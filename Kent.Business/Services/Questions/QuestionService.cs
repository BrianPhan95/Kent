using Kent.Entities.Model;
using Kent.Entities.Repositories.Questions;
using Kent.Libary.Logger;
using Kent.Libary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Kent.Business.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        public readonly IQuestionRepository _questionRepository;
        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        #region base
        public ResponseModel Delete(int id)
        {
            return _questionRepository.Delete(id);
        }

        public Question GetById(int id)
        {
            return _questionRepository.GetById(id);
        }

        public List<Question> GetList(RequestModel request)
        {
            return _questionRepository.GetList(request);
        }

        public ResponseModel Insert(Question model)
        {
            return _questionRepository.Insert(model);
        }

        public ResponseModel Update(Question model)
        {
            return _questionRepository.Update(model);
        }

        #endregion
    }
}
