using CaffStore.Bll.Dtos;
using CaffStore.Bll.Interfaces;
using CaffStore.Bll.Json;
using CaffStore.Dal;
using CaffStore.Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CaffStore.Bll.Services
{
    public class StoreService : IStoreService
    {
        private readonly StoreDbContext context;
        private readonly IUserService userService;

        public StoreService(StoreDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<IEnumerable<UploadedImagesResponseDto>> GetUploadedImages(string filter)
        {
            var id = userService.GetCurrentUserId();
            var files = await context.CaffFiles.Select(caff => new UploadedImagesResponseDto
            {
                FileName = caff.CaffRoute.Split('\\', StringSplitOptions.RemoveEmptyEntries).Last(),
                CaffRoute = caff.CaffRoute,
                GifRoute = caff.GifRoute,
                Ciffs = caff.CiffFiles.Select(ciff => new CiffDto
                {
                    Caption = ciff.Caption,
                    Tags = ciff.Tags.Select(tag => tag.Text)
                }).ToList()
            }).ToListAsync();

            if (filter != null)
            {
                files = files
                    .Where(f => f.FileName.Contains(filter))
                    .ToList();
            }

            return files;
        }

        public async Task Upload(IFormFile file)
        {
            Directory.CreateDirectory(Path.Combine("Resources", "Images"));

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                var fullPath = Path.Combine(pathToSave, fileName);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Path.Combine(fullPath, fileName));

                // elmentjük a caff-ot és a gif-et is
                var caffPath = Path.Combine(folderName, fileNameWithoutExtension + ".caff");
                var gifPath = Path.Combine(folderName, fileNameWithoutExtension + ".gif");

                // nem használt erőforrások felszabadítása a végén
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    // felküldött kép mentése
                    file.CopyTo(stream);

                    // meghívjuk a C++ parsert
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "parser.exe",
                            Arguments = caffPath,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                    process.Start();

                    // kiolvassuk a konvertálás eredményét
                    string output = process.StandardOutput.ReadToEnd();

                    await SaveCaffIfSuccesful(output, gifPath, caffPath);
                }
            }
        }

        private async Task SaveCaffIfSuccesful(string output, string gifPath, string caffPath)
        {
            if (output == "CONVERSION SUCCESFUL")
            {
                File.Copy("outgif.gif", gifPath, true);

                Console.WriteLine("Conversion successful");

                // metaadatokat beolvassuk a json-ből
                var caffJson = LoadCaffJson();

                var uploadedFile = new CaffFile
                {
                    CaffRoute = caffPath,
                    GifRoute = gifPath,
                    Creator = DecodeBase64(caffJson.CreatorB64),

                    // átalakítjuk a json-t adatbázisbeli entitásokká
                    CiffFiles = caffJson.Ciffs.Select(ciff => new CiffFile
                    {
                        Caption = DecodeBase64(ciff.CaptionB64),
                        Tags = ciff.TagB64s
                            .Select(tag => new Tag(DecodeBase64(tag)))
                            .ToList()
                    }).ToList(),
                    Date = $"{caffJson.Year}. {caffJson.Month}. {caffJson.Day}. {caffJson.Hour} {caffJson.Minute}"
                };

                // mentés db-be
                context.CaffFiles.Add(uploadedFile);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("Error in the native parser");
        }

        private static CaffJson LoadCaffJson()
        {
            using (StreamReader r = new("out.json"))
            {
                string json = r.ReadToEnd();
                var caff = JsonConvert.DeserializeObject<CaffJson>(json);
                return caff;
            }
        }

        private static string DecodeBase64(string encodedString)
        {
            byte[] data = Convert.FromBase64String(encodedString);
            return Encoding.UTF8.GetString(data);
        }
    }
}
