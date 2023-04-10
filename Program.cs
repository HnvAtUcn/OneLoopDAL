
using OneLoopDAL.DAL;
using OneLoopDAL.Models;
using System.Linq;

DALBase.SetDataDir(@"\DataSource\");
DALBase.ResetDatabase();

MovieService movieService = new();
IEnumerable<Movie> theMovies = movieService.GetMovies();
Console.WriteLine("Movies: " + theMovies.Count());

BookService bookService = new();
IEnumerable<Book> theBooks = bookService.GetBooks();
Console.WriteLine("Books: " + theBooks.Count());

CharacterService characterService = new();
IEnumerable<Character> theCharacters = characterService.GetCharacters();
Console.WriteLine("Characters: " + theCharacters.Count());

QuoteService quoteService = new();
IEnumerable<Quote> theQuotes = quoteService.GetQuotes();
Console.WriteLine("Quotes: " + theQuotes.Count());

ChapterService chapterService = new();
IEnumerable<Chapter> theChapters = chapterService.GetChapters();
Console.WriteLine("Chapters: " + theChapters.Count());

Movie m = movieService.GetMovie(theMovies.ElementAt<Movie>(0)._id);

m.name = "Test";

movieService.UpdateMovie(m);

m.name = "Blabla";
m = movieService.GetMovie(theMovies.ElementAt(0)._id);

Movie nm = new();
nm.name = "New one";
movieService.InsertMovie(nm);
DALBase.SaveDatabase();
movieService.DeleteMovie(nm);

DALBase.SaveDatabase();

Console.ReadLine();