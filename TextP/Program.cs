using System;
using System.IO;
using System.Text;
using TextP.Models;
using TextP.Controllers;
using TextP.Logic;
using TextP.UtfCheck;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TextP
{
    class Program
    {
        public static void AddOrUpdateDB(DBWords database)
        {
            if(database==null)
            {
                Console.WriteLine("Database does not exist, entering input mode...");
                return;
            }
            while (true)
            {
                Console.Write("Enter file name: ");
                var fileName = Console.ReadLine();
                var fullFileName = Directory.GetCurrentDirectory() + "\\" + fileName;
                if (!Directory.EnumerateFiles(Directory.GetCurrentDirectory()).Contains(fullFileName))
                {
                    Console.WriteLine("Invalid file name, try again...");
                    continue;
                }    

                var EncodeChecker = new Utf8Checker();
                if (EncodeChecker.Check(fileName))
                {
                    var a = FileController.ParseFile(File.ReadAllLines(fileName));
                    var e = FileController.GenerateWordList(a);
                    var result = Requests.GenerateUpdateRequestWithGlobalOptions(e);
                    DBController.AddOrUpdateEntity(database, result);
                    return;
                }
                else
                    Console.WriteLine("File is not in UTF-8 encoding");
            }
        }

        public static DbContextOptions<DBWords> ConfigureConnectionToDB()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<DBWords>();
            return optionsBuilder
                   .UseSqlServer(connectionString)
                   .Options;
        }

        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "\\Files");
            DBWords db = null;

            if (args.Length>GlobalSettings.MAX_ARGS_COUNT)
            {
                Console.WriteLine("More than one argument has been entered to run");
                return;
            }

            if (args.Length!=0)
            {
                db = new DBWords(ConfigureConnectionToDB());
                switch (args[0])
                {
                    case "-create": 
                        db.CreateNewBD();
                        AddOrUpdateDB(db);
                        Console.WriteLine("New database was created");
                        break;
                    case "-update":
                        if (!db.Database.CanConnect())
                        {
                            db = null;
                            Console.WriteLine("Database does not exist, entering input mode...");
                        }
                        else
                        {
                            AddOrUpdateDB(db);
                            Console.WriteLine("Database was updated");
                        }
                        break;
                    case "-delete":
                        if (!db.DeleteDB())
                            Console.WriteLine("Database does not exist, entering input mode...");
                        else
                            Console.WriteLine("Database was deleted");
                        db = null;
                        break;
                    default:
                        Console.WriteLine("Invalid input command, available start commands:");
                        Console.WriteLine("-create -> to create new database dictionary");
                        Console.WriteLine("-update -> to update database dictionary");
                        Console.WriteLine("-delete -> to delete database dictionary");
                        break;
                }
            }
            var sb = new StringBuilder();
            Console.WriteLine("You are in input mode");
            while (true)
            {
                var symbol = Console.ReadKey(false);
                if (symbol.Key == ConsoleKey.Backspace && sb.Length != 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                    Console.Write("\r");
                    Console.Write(sb.ToString()+" ");
                    Console.Write("\b");
                }
                else
                if (symbol.Key == ConsoleKey.Enter && sb.Length == 0 || symbol.Key == ConsoleKey.Escape)
                    return;
                else
                if (symbol.Key == ConsoleKey.Enter && sb.Length != 0)
                {
                    Console.WriteLine();
                    var resultString = sb.ToString();
                    sb = new StringBuilder();
                    var res = Requests.TakeRequest(db, resultString);
                    Requests.PrintRequest(res);
                    Console.WriteLine();
                }
                else if (symbol.Key != ConsoleKey.Backspace)
                    sb.Append(symbol.KeyChar);
            }
        }
    }
}
