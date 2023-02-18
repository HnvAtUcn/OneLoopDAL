using OneLoopDAL.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Security.Cryptography;

namespace OneLoopDAL.DAL
{

    public class DALBase
    {
        private static bool MustRead = true;

        protected static JsonNode TopNode = new JsonObject();
        protected static JsonNode MovieNode = new JsonObject();
        protected static JsonNode BookNode = new JsonObject();
        protected static JsonNode CharacterNode = new JsonObject();
        protected static JsonNode QuoteNode = new JsonObject();
        protected static JsonNode ChapterNode = new JsonObject();

        protected static JsonArray empty = new JsonArray();


        private static void ReadData()
        {
            string jsonPath = Directory.GetCurrentDirectory() + @"\Data\OneApi.json";

            if (File.Exists(jsonPath))
            {
                // Read all data from our "database", OneApi.json
                string dbContent = File.ReadAllText(jsonPath);

                //
                //https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-7-0
                //

                TopNode = JsonNode.Parse(dbContent)!;
                if (TopNode != null)
                {
                    if (TopNode["Movies"] != null)
                        MovieNode = TopNode["Movies"]!;
                    if (TopNode["Books"] != null)
                        BookNode = TopNode["Books"]!;
                    if (TopNode["Characters"] != null)
                        CharacterNode = TopNode["Characters"]!;
                    if (TopNode["Quotes"] != null)
                        QuoteNode = TopNode["Quotes"]!;
                    if (TopNode["Chapters"] != null)
                        ChapterNode = TopNode["Chapters"]!;
                }

                MustRead = false;
            }
        }

        public static void ResetDatabase()
        {
            string originalDataPath = Directory.GetCurrentDirectory() + @"\Data\OneApi.bak";
            if (File.Exists(originalDataPath))
            {
                string jsonPath = Directory.GetCurrentDirectory() + @"\Data\OneApi.json";
                if (File.Exists(jsonPath))
                {
                    File.Delete(jsonPath);
                }
                File.Copy(originalDataPath, jsonPath);
            }
        }

        public static void SaveDatabase()
        {
            string jsonPath = Directory.GetCurrentDirectory() + @"\Data\OneApi.json";
            if (File.Exists(jsonPath))
            {
                string backupPath = Directory.GetCurrentDirectory() + @"\Data\OneApiBackup.json";
                if (File.Exists(backupPath))
                {
                    File.Delete(backupPath);
                }
                File.Copy(jsonPath, backupPath);
            }
            string dbContent = TopNode!.ToString();
            File.WriteAllText(jsonPath, dbContent);
        }

        public static JsonNode? GetEntities<T>()
        {
            return GetEntityNode<T>();
        }

        public static JsonNode? GetEntity<T>(string Id)
        {
            JsonNode? jsonNode = GetEntityNode<T>();
            if (jsonNode == null)
                return null;
            JsonArray? source = (JsonArray)jsonNode["docs"]!;

            foreach (var res in source)
            {
                string _id = (string)res!["_id"]!;
                if (_id.Equals(Id))
                    return res;
            }

            return empty;
        }

        public static bool UpdateEntity<T>(object object2Update)
        {
            bool founditem = false;

            JsonNode? jsonNode = GetEntityNode<T>();
            if (jsonNode == null)
                return founditem;
            JsonArray? source = (JsonArray)jsonNode["docs"]!;

            string ent2updateId;
            string ent2updateAsString;

            switch (typeof(T))
            {
                case Type type when type == typeof(Movie):
                    Movie Movie2update = (Movie)object2Update;
                    ent2updateId = Movie2update._id;
                    ent2updateAsString = JsonSerializer.Serialize(Movie2update, typeof(Movie));
                    break;

                case Type type when type == typeof(Book):
                    Book Book2update = (Book)object2Update;
                    ent2updateId = Book2update._id;
                    ent2updateAsString = JsonSerializer.Serialize(Book2update, typeof(Book));
                    break;

                case Type type when type == typeof(Character):
                    Character Character2update = (Character)object2Update;
                    ent2updateId = Character2update._id;
                    ent2updateAsString = JsonSerializer.Serialize(Character2update, typeof(Character));
                    break;

                case Type type when type == typeof(Quote):
                    Quote Quote2update = (Quote)object2Update;
                    ent2updateId = Quote2update._id;
                    ent2updateAsString = JsonSerializer.Serialize(Quote2update, typeof(Quote));
                    break;

                case Type type when type == typeof(Chapter):
                    Chapter Chapter2update = (Chapter)object2Update;
                    ent2updateId = Chapter2update._id;
                    ent2updateAsString = JsonSerializer.Serialize(Chapter2update, typeof(Chapter));
                    break;

                default:
                    return founditem;
            }

            foreach (var res in source)
            {
                string _id = (string)res!["_id"]!;
                if (_id.Equals(ent2updateId))
                {
                    int index = source.IndexOf(res);
                    JsonNode updatedNode = JsonNode.Parse(ent2updateAsString)!;
                    source.RemoveAt(index);
                    source.Insert(index, updatedNode);
                    founditem = true;
                    break;
                }
            }

            return founditem;
        }

        public static void InsertEntity<T>(object object2Insert)
        {
            JsonNode? jsonNode = GetEntityNode<T>();
            if (jsonNode == null)
                return;
            JsonArray? source = (JsonArray)jsonNode["docs"]!;
            JsonNode insertNode;
            int end = source.Count;
            string newEntity;

            byte[] theBytes = RandomNumberGenerator.GetBytes(24);
            string NewID = Convert.ToBase64String(theBytes);

            switch (typeof(T))
            {
                case Type type when type == typeof(Movie):
                    Movie movie2Insert = (Movie)object2Insert;
                    movie2Insert._id = NewID;
                    newEntity = JsonSerializer.Serialize(movie2Insert, typeof(Movie));
                    break;

                case Type type when type == typeof(Book):
                    Book book2Insert = (Book)object2Insert;
                    book2Insert._id = NewID;
                    newEntity = JsonSerializer.Serialize(book2Insert, typeof(Book));
                    break;

                case Type type when type == typeof(Character):
                    Character character2Insert = (Character)object2Insert;
                    character2Insert._id = NewID;
                    newEntity = JsonSerializer.Serialize(character2Insert, typeof(Character));
                    break;

                case Type type when type == typeof(Quote):
                    Quote quote2Insert = (Quote)object2Insert;
                    quote2Insert._id = NewID;
                    newEntity = JsonSerializer.Serialize(quote2Insert, typeof(Quote));
                    break;

                case Type type when type == typeof(Chapter):
                    Chapter chapter2Insert = (Chapter)object2Insert;
                    chapter2Insert._id = NewID;
                    newEntity = JsonSerializer.Serialize(chapter2Insert, typeof(Chapter));
                    break;

                default:
                    return;
            }

            insertNode = JsonNode.Parse(newEntity)!;
            source.Insert(end, insertNode);

            return;
        }

        public static bool DeleteEntity<T>(object object2Delete)
        {
            bool founditem = false;

            JsonNode? jsonNode = GetEntityNode<T>();
            if (jsonNode == null)
                return founditem;
            JsonArray? source = (JsonArray)jsonNode["docs"]!;

            string ent2deleteId;

            switch (typeof(T))
            {
                case Type type when type == typeof(Movie):
                    Movie movie2Delete = (Movie)object2Delete;
                    ent2deleteId = movie2Delete._id;
                    break;

                case Type type when type == typeof(Book):
                    Book book2Delete = (Book)object2Delete;
                    ent2deleteId = book2Delete._id;
                    break;

                case Type type when type == typeof(Character):
                    Character character2Delete = (Character)object2Delete;
                    ent2deleteId = character2Delete._id;
                    break;

                case Type type when type == typeof(Quote):
                    Quote quote2Delete = (Quote)object2Delete;
                    ent2deleteId = quote2Delete._id;
                    break;

                case Type type when type == typeof(Chapter):
                    Chapter chapter2Delete = (Chapter)object2Delete;
                    ent2deleteId = chapter2Delete._id;
                    break;

                default:
                    return founditem;
            }

            foreach (var res in source)
            {
                string _id = (string)res!["_id"]!;
                if (_id.Equals(ent2deleteId))
                {
                    int index = source.IndexOf(res);
                    source.RemoveAt(index);
                    founditem = true;
                    break;
                }
            }

            return founditem;
        }

        private static void CheckData()
        {
            if (MustRead)
                ReadData();
        }

        public static JsonNode? GetEntityNode<T>()
        {
            CheckData();

            switch (typeof(T))
            {
                case Type type when type == typeof(Movie):
                    return MovieNode;

                case Type type when type == typeof(Book):
                    return BookNode;

                case Type type when type == typeof(Character):
                    return CharacterNode;

                case Type type when type == typeof(Quote):
                    return QuoteNode;

                case Type type when type == typeof(Chapter):
                    return ChapterNode;

                default:
                    return null;
            }
        }


    }

}


