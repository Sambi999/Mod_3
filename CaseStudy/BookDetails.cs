using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{
    public class BookDetails
    {
        [JsonProperty("firstname")]
        public string? FirstName { get; set; }
        [JsonProperty("token")]
        public string? Token { get; set; }
    }
}
