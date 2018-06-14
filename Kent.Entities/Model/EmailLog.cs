using Kent.Libary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class EmailLog : BaseModel
    {
        public int EmailQueueID { get; set; }
        [ForeignKey("EmailQueueID")]
        public virtual EmailQueue EmailQueues { get; set; }

        public string Message { get; set; }

        public EmailEnums.EmailStatusEnums Status { get; set; }

    }
}
