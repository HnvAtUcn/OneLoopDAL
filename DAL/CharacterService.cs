using OneLoopDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneLoopDAL.Models
{
    internal class CharacterService
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
