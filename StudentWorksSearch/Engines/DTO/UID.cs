using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWorksSearch.Engines.DTO
{
    class UID
    {
        [JsonProperty("text_uid")]
        public string uid { get; set; }
    }
}
