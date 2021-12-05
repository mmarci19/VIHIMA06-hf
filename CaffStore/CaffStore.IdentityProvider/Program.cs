using CaffStore.IdentityProvider.Data;
using CaffStore.IdentityProvider.Models;
using CaffStore.IdentityProvider.Services;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.Authority = "https://localhost:5101";
        opt.Audience = "CaffStore.IdentityProviderAPI";

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,

            ValidIssuer = "https://localhost:44464",
            ValidAudience = "CaffStore.IdentityProviderAPI"
        };
    })
    .AddIdentityServerJwt();

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("https://localhost:44464");
}));

builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

UpdateDatabase(app);
CreateRoles(app.Services.CreateScope().ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();


static void UpdateDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope())
    {
        using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
        {
            context.Database.Migrate();
        }
    }
}

async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    IdentityResult roleResult;

    var roleExist = await roleManager.RoleExistsAsync("Admin");
    if (!roleExist)
    {
        roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var getUser = await userManager.FindByEmailAsync("admin@admin.hu");

    if (getUser == null)
    {
        var user = new ApplicationUser("admin@admin.hu");
        string adminPassword = "Asdf1234.";

        var createUser = await userManager.CreateAsync(user, adminPassword);
        if (createUser.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}