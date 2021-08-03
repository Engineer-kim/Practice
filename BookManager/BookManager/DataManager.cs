using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;


namespace BookManager
{
    class DataManager
    {
        public static List<Book> Books = new List<Book>();
        public static List<User> Users = new List<User>();

        static DataManager()
        {
            Load();
        }
        public static void Load()
        {
          try 
            { 
          string booksOutput =  File.ReadAllText(@"./Books.xml");
          XElement booksElement = XElement.Parse(booksOutput);
          Books =  (from item in booksElement.Descendants("book")
                    select new Book()
                    {
                           ISbn = item.Element("isbn").Value,
                           Name = item.Element("name").Value,
                           Publisher = item.Element("publisher").Value,
                           Page = int.Parse(item.Element("page").Value),
                           BorrowdAt = DateTime.Parse(item.Element("borrowdAt").Value),
                           isBorrowed = item.Element("isBorrowd").Value != "0" ? true : false,
                           UserId = int.Parse(item.Element("userId").Value),
                           UserName = item.Element("userName").Value
                    }).ToList<Book>();

           string usersOutput = File.ReadAllText(@"./Users.xml");
           XElement usersElement = XElement.Parse(usersOutput);
           Users = (from item in usersElement.Descendants("user")
                    select new User()
                     {
                         Id = int.Parse(item.Element("id").Value),
                         Name = item.Element("name").Value
                     }).ToList<User>();
            }catch(FileNotFoundException exception)
        {
                Save();
        }
      }

   
        public static void Save()
        {
        string booksOutput = "";
        string usersOutput = "";
        booksOutput += "<books>\n";
        foreach (var item in Books)
        {
            booksOutput += "<book>\n";
            booksOutput += "   <isbn>" + item.ISbn + "</isbn>\n";
            booksOutput += "   <name>" + item.Name + "</name>\n";
            booksOutput += "   <publisher>" + item.Publisher + "</publisher>\n";
            booksOutput += "   <page>" + item.Page + "</page>\n";
            booksOutput += "   <borrowdAt>" + item.BorrowdAt.ToLongDateString() + "</borrowdAt>\n";
            booksOutput += "   <isBorrowd>" + (item.isBorrowed ? 1 : 0) + "</isBorrowd>\n";
            booksOutput += "   <userId>" + item.UserId + "</userId>\n";
            booksOutput += "   <userName>" + item.UserName + "</userName>\n";
            booksOutput += "</book>\n";
        }
        booksOutput += "</books>";

        File.WriteAllText(@"./Books.xml", booksOutput);

        usersOutput += "<users>\n";
        foreach (var item in Users)
        {
            usersOutput += "<user>\n";
            usersOutput += "   <id>" + item.Id + "</id>\n";
            usersOutput += "   <name>" + item.Name + "</name>\n";
            usersOutput += "</user>\n";
        }
        usersOutput += "</users>";

       File.WriteAllText(@"./Users.xml", usersOutput);
    }
    }
}
