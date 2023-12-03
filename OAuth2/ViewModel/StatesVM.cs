using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.ViewModel
{
    public class StatesVM
    {
        public StatesVM()
        {
            this.AddressVM = new HashSet<AddressVM>();
        }

        public int Id { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<AddressVM> AddressVM { get; set; }
    }
}