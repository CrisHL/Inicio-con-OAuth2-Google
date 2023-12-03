using OAuth2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.ViewModel
{
    public class AddressVM
    {
        public AddressVM()
        {
            this.UserVM = new HashSet<UserVM>();
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public Nullable<int> State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public StatesVM StatesVM { get; set; }
        public ICollection<UserVM> UserVM { get; set; }
    }
}