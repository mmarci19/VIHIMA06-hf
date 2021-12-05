using System.Collections.Generic;

namespace CaffStore.Bll.Dtos
{
    public class CiffDto
    {
        public string Caption { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
