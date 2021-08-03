using System;
using System.Collections.Generic;
using System.Text;

namespace BookManager
{
    class Book
    { 
        public string ISbn { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public int Page { get; set; }
        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool isBorrowed { get; set; }
        public DateTime BorrowdAt  { get; set; }
    }
}
