using Kent.Business.Core.Models.Base;
using Kent.Libary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services.Base
{
    public interface IBaseService<TModel> where TModel : class
    {
        List<TModel> GetList(RequestModel request);

        TModel GetById(int id);

        ResponseModel Insert(TModel model);

        ResponseModel Update(TModel model);

        ResponseModel Delete(int id);
    }
}
