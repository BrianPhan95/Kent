using Kent.Business.Core.Models.TestKits;
using Kent.Business.Services.Base;
using Kent.Entities.Model;
using Kent.Libary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services.QuestionKits
{
    public interface IQuestionKitService : IBaseService<QuestionKit>
    {
        List<QuestionKitModel> GetListQuestionKit(RequestModel request);
        ResponseModel ImportTestKit(ImportQuestionKitModel model, DataTable data);
    }
}
