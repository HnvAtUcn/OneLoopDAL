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
            JsonNode jsonResult = DALBase.GetEntities<Character>();
            if (jsonResult != null)
            {
                JsonNode docs = jsonResult["docs"]!;
                result = JsonSerializer.Deserialize<List<Character>>(docs.AsArray().ToString())!;
            }

            return result;
        }

        public Character GetMovie(string Id)
        {
            JsonNode movie = DALBase.GetEntity<Character>(Id)!;
            Character retval = JsonSerializer.Deserialize<Character>(movie.ToJsonString())!;
            return retval;
        }

        public void UpdateMovie(Character character2Update)
        {
            DALBase.UpdateEntity<Character>(character2Update);
        }

        public void InsertMovie(Character character2Insert)
        {
            DALBase.InsertEntity<Character>(character2Insert);
        }

        public void DeleteMovie(Character character2Delete)
        {
            DALBase.DeleteEntity<Character>(character2Delete);
        }
    }
}
