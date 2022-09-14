using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateBQ.Extensions
{
    public class QueryBuilder
    {
        public void QueryOne(string shootingId, IDictionary<string, string> dictionary)
        {
            string query = $"UPDATE `journey-event-process-dev.luis.journey_event_v2` " +
                        $"SET properties = array[STRUCT('Prod', 'Dev'), STRUCT('stg', 'amb')] " +
                        $"WHERE shootingId = '62f31f348c713af456042a2b'; ";
        }
        public void QueryOne(string shootingId, List<IDictionary<string, string>> dictionary)
        {
            string query = $"UPDATE `journey-event-process-dev.luis.journey_event_v2` " +
                        $"SET properties = array[STRUCT('Prod', 'Dev'), STRUCT('stg', 'amb')] " +
                        $"WHERE shootingId = '62f31f348c713af456042a2b'; ";
        }
    }
}
