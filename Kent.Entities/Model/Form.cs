using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class Form : BaseModel
    {
        public int FormTypeID { get; set; }
        [ForeignKey("FormTypeID")]
        public virtual FormType FormType { get; set; }

        public int EmailQueueID { get; set; }
        [ForeignKey("EmailQueueID")]
        public virtual EmailQueue EmailQueues { get; set; }

        public string Data { get; set; }
    }
}
