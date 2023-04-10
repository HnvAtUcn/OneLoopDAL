using OneLoopDAL.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace OneLoopDAL.DAL
{
    public class MovieService
    {
        public IEnumerable<Movie> GetMovies()
        {
            IEnumerable<Movie> result= new List<Movie>();   
            JsonNode? movies = DALBase.GetEntities<Movie>();
            if (movies != null)
            {
                JsonNode? mov = movies["docs"];
                result = JsonSerializer.Deserialize<List<Movie>>(mov!.AsArray().ToString())!;
            }

            return result; 
        }

        public Movie GetMovie(string Id)
        {
            JsonNode movie = DALBase.GetEntity<Movie>(Id)!;
            Movie retval = JsonSerializer.Deserialize<Movie>(movie.ToJsonString())!;
            return retval;
        }

        public void UpdateMovie(Movie movie2Update)
        {
            DALBase.UpdateEntity<Movie>(movie2Update);
        }

        public void InsertMovie(Movie movie2Insert)
        {
            DALBase.InsertEntity<Movie>(movie2Insert);
        }

        public void DeleteMovie(Movie movie2Delete)
        {
            DALBase.DeleteEntity<Movie>(movie2Delete);
        }
    }
}
