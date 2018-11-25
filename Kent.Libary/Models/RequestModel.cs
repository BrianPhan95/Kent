using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Libary.Models
{
    public class RequestModel
    {
        public string Keyword { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
