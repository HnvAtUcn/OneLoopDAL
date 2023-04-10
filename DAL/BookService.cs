using OneLoopDAL.Models;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace OneLoopDAL.DAL
{
    public class BookService
    {
        public IEnumerable<Book> GetBooks()
        {
            IEnumerable<Book> result = new List<Book>();
            JsonNode? jsonResult = DALBase.GetEntities<Book>();
            if (jsonResult != null)
            {
                JsonNode? docs = jsonResult["docs"];
                result = JsonSerializer.Deserialize<List<Book>>(docs!.AsArray().ToString())!;
            }

            return result;
        }
    }
}
