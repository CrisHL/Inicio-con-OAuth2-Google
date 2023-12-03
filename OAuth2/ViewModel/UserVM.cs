using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.ViewModel
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public int IdAddress { get; set; }

        public AddressVM AddressVM { get; set; }
    }
}