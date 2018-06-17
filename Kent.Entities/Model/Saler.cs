using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class Saler : BaseModel
    {
        public string SalerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
