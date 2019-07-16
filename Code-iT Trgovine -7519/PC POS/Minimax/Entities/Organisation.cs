using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    class Organisation
    {
        [JsonProperty(PropertyName = "ID")]
        public int OrganisationId { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Title { get; set; }
    }
}
