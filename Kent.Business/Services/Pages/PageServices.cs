using Kent.Business.Core.Models.Pages;
using Kent.Entities.Model;
using Kent.Entities.Repositories;
using Kent.Libary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class PageServices : IPageServices
    {
        private IPageRepository _pageRepository;
        private IHeaderTemplateRepository _headerTemplateRepository;
        private IFooterTemplateRepository _footerTemplateRepository;
        public PageServices(IPageRepository pageRepository, IHeaderTemplateRepository headerTemplateRepository, IFooterTemplateRepository footerTemplateRepository)
        {
            _pageRepository = pageRepository;
            _headerTemplateRepository = headerTemplateRepository;
            _footerTemplateRepository = footerTemplateRepository;
        }

        public List<PageModel> GetPages(string keyword)
        {
            List<Page> data = _pageRepository.GetPages(keyword);
            if (data != null)
            {
                return data.Select(d => Mapping(d)).ToList();
            }
            return new List<PageModel>();
        }

        public PageViewModel GetPageByFiendlyUrl(string url, PageLanguages language)
        {
            Page data = _pageRepository.GetPageByFriendlyUrl(url);
            if (data != null)
            {
                var header = _headerTemplateRepository.GetHeaderTemplateById(data.HeaderTemplateId.Value);
                var footer = _footerTemplateRepository.GetFooterTemplateById(data.FooterTemplateId.Value);
                if(language == PageLanguages.Vietnamese)
                {
                    return new PageViewModel()
                    {
                        Title = data.Title,
                        HeaderContent = header.Content,
                        PageContent = data.Content,
                        FooterContent = footer.Content,
                    };
                }
                else if(language == PageLanguages.English)
                {
                    return new PageViewModel()
                    {
                        Title = data.TitleEnglish,
                        HeaderContent = header.ContentEnglish,
                        PageContent = data.ContentEnglish,
                        FooterContent = footer.ContentEnglish,
                    };
                }
            }
            return new PageViewModel();
        }

        public bool DeletePage(int id)
        {
            return _pageRepository.DeletePage(id);
        }

        public bool SavePage(PageManageModel model)
        {
            if (model.ID > 0)
            {
                var dataUpdate = _pageRepository.GetPageById(model.ID);
                dataUpdate.Title = model.Title;
                dataUpdate.FriendlyUrl = model.FriendlyUrl;
                dataUpdate.Status = PageStatus.Online;
                dataUpdate.Content = model.Content;
                dataUpdate.IsHomePage = model.IsHomePage;

                dataUpdate.TitleEnglish = model.TitleEnglish;
                dataUpdate.FriendlyUrlEnglish = model.FriendlyUrlEnglish;
                dataUpdate.ContentEnglish = model.ContentEnglish;

                dataUpdate.FooterTemplateId = _footerTemplateRepository.GetFooterTemplates(model.FooterTemplate).FirstOrDefault().ID;
                dataUpdate.HeaderTemplateId = _headerTemplateRepository.GetHeaderTemplates(model.HeaderTemplate).FirstOrDefault().ID;
                dataUpdate.LastUpdateBy = "Admin";
                dataUpdate.LastUpdate = DateTime.Now;
                dataUpdate.RecordOrder = 1;
                dataUpdate.RecordActive = true;
                dataUpdate.RecordDeleted = false;

                return _pageRepository.UpdatePage(dataUpdate);
            }
            else
            {
                Page data = new Page()
                {
                    Title = model.Title,
                    FriendlyUrl = model.FriendlyUrl,
                    Status = PageStatus.Online,
                    Content = model.Content,
                    IsHomePage = model.IsHomePage,

                    TitleEnglish = model.TitleEnglish,
                    FriendlyUrlEnglish = model.FriendlyUrlEnglish,
                    ContentEnglish = model.ContentEnglish,

                    FooterTemplateId = _footerTemplateRepository.GetFooterTemplates(model.FooterTemplate).FirstOrDefault().ID,
                    HeaderTemplateId = _headerTemplateRepository.GetHeaderTemplates(model.HeaderTemplate).FirstOrDefault().ID,
                    Created = DateTime.Now,
                    CreatedBy = "Admin",
                    LastUpdate = DateTime.Now,
                    RecordOrder = 1,
                    RecordActive = true,
                    RecordDeleted = false,
                };
                return _pageRepository.AddPage(data);
            }
        }

        public PageManageModel GetPageById(int id)
        {
            Page data = _pageRepository.GetPageById(id);
            if (data != null)
            {
                return MappingManageModel(data);
            }
            return new PageManageModel();
        }

        private PageModel Mapping(Page page)
        {
            return new PageModel()
            {
                ID = page.ID,
                Title = page.Title,
                FriendlyUrl = page.FriendlyUrl,
                Status = PageStatus.Online,
                Content = page.Content,
                IsHomePage = page.IsHomePage,

                TitleEnglish = page.TitleEnglish,
                FriendlyUrlEnglish = page.FriendlyUrlEnglish,
                ContentEnglish = page.ContentEnglish,

                FooterTemplateId = page.FooterTemplateId ?? 0,
                HeaderTemplateId = page.HeaderTemplateId ?? 0,
                Created = page.Created,
                CreatedBy = page.CreatedBy,
                LastUpdate = page.LastUpdate,
                LastUpdateBy = page.LastUpdateBy
            };
        }

        private PageManageModel MappingManageModel(Page page)
        {
            var headerName = _headerTemplateRepository.GetHeaderTemplateById(page.HeaderTemplateId.Value).Name;
            var footerName = _footerTemplateRepository.GetFooterTemplateById(page.FooterTemplateId.Value).Name;
            return new PageManageModel()
            {
                ID = page.ID,
                Title = page.Title,
                FriendlyUrl = page.FriendlyUrl,
                Status = PageStatus.Online,
                Content = page.Content,
                IsHomePage = page.IsHomePage,

                TitleEnglish = page.TitleEnglish,
                FriendlyUrlEnglish = page.FriendlyUrlEnglish,
                ContentEnglish = page.ContentEnglish,

                FooterTemplate = headerName,
                HeaderTemplate = footerName,
            };
        }
    }
}
