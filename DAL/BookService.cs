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
    internal class BookService
    {
        public IEnumerable<Book> GetBooks()
        {
            IEnumerable<Book> result = new List<Book>();
            JsonNode jsonResult = DALBase.GetEntities<Book>();
            if (jsonResult != null)
            {
                JsonNode docs = jsonResult["docs"]!;
                result = JsonSerializer.Deserialize<List<Book>>(docs.AsArray().ToString())!;
            }

            return result;
        }

        public Book GetMovie(string Id)
        {
            JsonNode movie = DALBase.GetEntity<Book>(Id)!;
            Book retval = JsonSerializer.Deserialize<Book>(movie.ToJsonString())!;
            return retval;
        }

        public void UpdateMovie(Book book2Update)
        {
            DALBase.UpdateEntity<Book>(book2Update);
        }

        public void InsertMovie(Book book2Insert)
        {
            DALBase.InsertEntity<Book>(book2Insert);
        }

        public void DeleteMovie(Book book2Delete)
        {
            DALBase.DeleteEntity<Book>(book2Delete);
        }
    }
}
