using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWorksSearch.Engines.DTO
{
    class Plagiarism
    {
        [JsonProperty("text_unique")]
        public double uniq { get; set; }

        [JsonProperty("error_code")]
        public int err { get; set; }
    }
}
