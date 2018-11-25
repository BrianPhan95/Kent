using Kent.Entities.Model;
using Kent.Entities.Repositories.QuestionSections;
using Kent.Libary.Models;
using System.Collections.Generic;

namespace Kent.Business.Services.QuestionSections
{
    public class QuestionSectionService : IQuestionSectionService
    {
        public readonly IQuestionSectionRepository _questionSectionRepository;
        public QuestionSectionService(IQuestionSectionRepository questionSectionRepository)
        {
            _questionSectionRepository = questionSectionRepository;
        }

        #region base
        public ResponseModel Delete(int id)
        {
            return _questionSectionRepository.Delete(id);
        }

        public QuestionSection GetById(int id)
        {
            return _questionSectionRepository.GetById(id);
        }

        public List<QuestionSection> GetList(RequestModel request)
        {
            return _questionSectionRepository.GetList(request);
        }

        public ResponseModel Insert(QuestionSection model)
        {
            return _questionSectionRepository.Insert(model);
        }

        public ResponseModel Update(QuestionSection model)
        {
            return _questionSectionRepository.Update(model);
        }

        #endregion
    }
}
