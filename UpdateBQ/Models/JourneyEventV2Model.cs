using System.Collections;
using System.Collections.Generic;

namespace UpdateBQ.Models
{
    public class JourneyEventV2Model
    {
        public string shootingCorrelationId { get; set; }
        public List<IDictionary<string, string>> Properties { get; set; }
    }
}
