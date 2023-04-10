using OneLoopDAL.Models;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace OneLoopDAL.DAL
{
    public class ChapterService
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
