using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Models
{
    public class ContactBooks
    {
        public string UserId { get; set; }
        public List<ContactInfo> Contacts { get; set; }
        
    }
}
