using Kent.Business.Core.Models.Base;
using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.TestKits
{
    public class QuestionKitModel
    {
        public QuestionKitModel()
        {

        }
        public QuestionKitModel(QuestionKit questionKit)
        {
            ID = questionKit.ID;
            Name = questionKit.Name;
            Description = questionKit.Description;

            RecordOrder = questionKit.RecordOrder;
            Created = questionKit.Created;
            CreatedBy = questionKit.CreatedBy;
            LastUpdate = questionKit.LastUpdate;
            LastUpdateBy = questionKit.LastUpdateBy;
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int RecordOrder { get; set; }
        public bool RecordActive { get; set; }
        public bool RecordDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
