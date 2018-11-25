using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.Base
{
    public class BaseHierachyModel<T> : BaseModel where T : class
    {
        public int? ParentId { get; set; }

        public string Hierarchy { get; set; }
    }
}
