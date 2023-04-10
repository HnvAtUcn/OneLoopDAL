using OneLoopDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneLoopDAL.DAL
{
    public class QuoteService
    {
        public IEnumerable<Quote> GetQuotes()
        {
            IEnumerable<Quote> result = new List<Quote>();
            JsonNode? jsonResult = DALBase.GetEntities<Quote>();
            if (jsonResult != null)
            {
                JsonNode? docs = jsonResult["docs"];
                result = JsonSerializer.Deserialize<List<Quote>>(docs!.AsArray().ToString())!;
            }

            return result;
        }
    }
}
