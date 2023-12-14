using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{
    internal class BookResponse
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }
    }
}
