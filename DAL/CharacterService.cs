using OneLoopDAL.DAL;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace OneLoopDAL.Models
{
    public class CharacterService
    {
        public IEnumerable<Character> GetCharacters()
        {
            IEnumerable<Character> result = new List<Character>();
            JsonNode? jsonResult = DALBase.GetEntities<Character>();
            if (jsonResult != null)
            {
                JsonNode? docs = jsonResult["docs"];
                result = JsonSerializer.Deserialize<List<Character>>(docs!.AsArray().ToString())!;
            }

            return result;
        }
    }
}
