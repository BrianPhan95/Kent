using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class NewsHot : BaseModel
    {


        public virtual Page Page { get; set; }
    }
}
