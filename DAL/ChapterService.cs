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
    internal class ChapterService
    {
        public IEnumerable<Chapter> GetChapters()
        {
            IEnumerable<Chapter> result = new List<Chapter>();
            JsonNode jsonResult = DALBase.GetEntities<Chapter>();
            if (jsonResult != null)
            {
                JsonNode docs = jsonResult["docs"]!;
                result = JsonSerializer.Deserialize<List<Chapter>>(docs.AsArray().ToString())!;
            }

            return result;
        }

        public Chapter GetMovie(string Id)
        {
            JsonNode movie = DALBase.GetEntity<Chapter>(Id)!;
            Chapter retval = JsonSerializer.Deserialize<Chapter>(movie.ToJsonString())!;
            return retval;
        }

        public void UpdateMovie(Chapter chapter2Update)
        {
            DALBase.UpdateEntity<Chapter>(chapter2Update);
        }

        public void InsertMovie(Chapter chapter2Insert)
        {
            DALBase.InsertEntity<Chapter>(chapter2Insert);
        }

        public void DeleteMovie(Chapter chapter2Delete)
        {
            DALBase.DeleteEntity<Chapter>(chapter2Delete);
        }
    }
}
