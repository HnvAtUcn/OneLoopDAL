using OneLoopDAL.Models;
using System.Text.Json.Nodes;
using System.Text.Json;

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
