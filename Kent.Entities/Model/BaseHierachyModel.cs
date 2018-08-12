using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class BaseHierachyModel<T> : BaseModel where T : class
    {
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual T Parent { get; set; }

        public string Hierarchy { get; set; }
    }
}
