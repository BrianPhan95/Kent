using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class EmailQueue : BaseModel
    {
        public string Name { get; set; }

        public string Subject { get; set; }

        public string From { get; set; }

        public string FromName { get; set; }

        public string To { get; set; }

        public string ToName { get; set; }

        public string CC { get; set; }

        public string BCC { get; set; }

        public string Body { get; set; }
    }
}
