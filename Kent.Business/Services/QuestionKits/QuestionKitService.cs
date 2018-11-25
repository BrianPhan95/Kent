using Kent.Business.Core.Models.TestKits;
using Kent.Entities.Model;
using Kent.Entities.Repositories.QuestionKits;
using Kent.Libary.Logger;
using Kent.Libary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Kent.Business.Services.QuestionKits
{
    public class QuestionKitService : IQuestionKitService
    {
        public readonly IQuestionKitRepository _questionKitRepository;
        public QuestionKitService(IQuestionKitRepository questionKitRepository)
        {
            _questionKitRepository = questionKitRepository;
        }

        #region base
        public ResponseModel Delete(int id)
        {
            return _questionKitRepository.Delete(id);
        }

        public QuestionKit GetById(int id)
        {
            return _questionKitRepository.GetById(id);
        }

        public List<QuestionKit> GetList(RequestModel request)
        {
            return _questionKitRepository.GetList(request);
        }

        public ResponseModel Insert(QuestionKit model)
        {
            return _questionKitRepository.Insert(model);
        }

        public ResponseModel Update(QuestionKit model)
        {
            return _questionKitRepository.Update(model);
        }

        #endregion
        
        public List<QuestionKitModel> GetListQuestionKit(RequestModel request)
        {
            var list = GetList(request);
            return list.Select(d => new QuestionKitModel(d)).ToList();
        }

        public ResponseModel ImportTestKit(ImportQuestionKitModel model, DataTable data)
        {
            var result = new ResponseModel();

            return result;
        }
    }
}
