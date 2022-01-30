using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinLanguage.Services
{
    //add permision for editing dictionary, 
    public class Admin
    {
        public int userID { get; set; }
        public string email { get; set; }
        public string psswrd { get; set; }
        public string username { get; set; }
    }
}
