using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffStore.Bll.Json
{
    public class CiffJson
    {
        public string CaptionB64 { get; set; }
        public List<string> TagB64s { get; set; }
    }

    public class CaffJson
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string CreatorB64 { get; set; }
        public List<CiffJson> Ciffs { get; set; }
    }
}
