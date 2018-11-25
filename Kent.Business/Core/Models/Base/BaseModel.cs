using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.Base
{
    public class BaseModel
    {
        public int ID { get; set; }
        public int RecordOrder { get; set; }
        public bool RecordActive { get; set; }
        public bool RecordDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
