using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class UserType : BaseModel
    {
        public string UserTypeName { get; set; }
        public int Level { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
