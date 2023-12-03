using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }
    }
    
    public class RootObject
    {
        public List<UserViewModel> listEmployees { get; set; }
    }
}