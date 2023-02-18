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
            JsonNode? jsonResult = DALBase.GetEntities<Chapter>();
            if (jsonResult != null)
            {
                JsonNode? docs = jsonResult["docs"];
                result = JsonSerializer.Deserialize<List<Chapter>>(docs!.AsArray().ToString())!;
            }

            return result;
        }
    }
}
