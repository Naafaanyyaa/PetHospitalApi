using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Business.Models.Request
{
    public class ContactsRequest
    {
        public string? Telegram { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? Viber { get; set; }
        public string? Phone { get; set; }
    }
}
