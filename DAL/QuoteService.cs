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
    internal class QuoteService
    {
        public IEnumerable<Quote> GetQuotes()
        {
            IEnumerable<Quote> result = new List<Quote>();
            JsonNode quotes = DALBase.GetEntities<Quote>();
            if (quotes != null)
            {
                JsonNode docs = quotes["docs"]!;
                result = JsonSerializer.Deserialize<List<Quote>>(docs.AsArray().ToString())!;
            }

            return result;
        }

        public Quote GetMovie(string Id)
        {
            JsonNode quote = DALBase.GetEntity<Quote>(Id)!;
            Quote retval = JsonSerializer.Deserialize<Quote>(quote.ToJsonString())!;
            return retval;
        }

        public void UpdateMovie(Quote quote2Update)
        {
            DALBase.UpdateEntity<Quote>(quote2Update);
        }

        public void InsertMovie(Quote quote2Insert)
        {
            DALBase.InsertEntity<Quote>(quote2Insert);
        }

        public void DeleteMovie(Quote quote2Delete)
        {
            DALBase.DeleteEntity<Quote>(quote2Delete);
        }
    }
}
