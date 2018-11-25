using Kent.Business.Core.Models.Pages;
using Kent.Business.Services;
using Kent.Libary.Enums;
using Kent.Libary.Utilities;
using Kent.Web.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Controllers
{
    public class PageController : BaseController
    {
        private readonly IPageServices _pageServices;
        private IHeaderTemplateServices _headerTemplateServices;
        private IFooterTemplateServices _footerTemplateServices;

        public PageController(IPageServices pageServices, IHeaderTemplateServices headerTemplateServices, IFooterTemplateServices footerTemplateServices)
        {
            _pageServices = pageServices;
            _headerTemplateServices = headerTemplateServices;
            _footerTemplateServices = footerTemplateServices;
        }
        // GET: Pages
        public ActionResult Index(string friendlyUrl)
        {
            friendlyUrl = SecureUtilities.RemoveSqlInjection(friendlyUrl);
            PageLanguages lang = PageLanguages.Vietnamese;
            if (Language == "vn")
            {
                lang = PageLanguages.Vietnamese;
            }
            else if (Language == "en")
            {
                lang = PageLanguages.English;
            }

            var page = new PageViewModel();
            if (friendlyUrl == "dangkynhaphoc" || friendlyUrl == "lienhe")
            {
                var headerDefault = _headerTemplateServices.GetHeaderTemplateDefault();
                var footerDefault = _footerTemplateServices.GetFooterTemplateDefault();
                if (Language == "vn")
                {
                    page.HeaderContent = headerDefault.Content;
                    page.FooterContent = footerDefault.Content;
                }
                else if (Language == "en")
                {
                    page.HeaderContent = headerDefault.ContentEnglish;
                    page.FooterContent = footerDefault.ContentEnglish;
                }

                ViewBag.IsSpecialPage = true;
                if (friendlyUrl == "dangkynhaphoc")
                {
                    ViewBag.Title = "Đăng kí nhập học";
                }
                else if (friendlyUrl == "lienhe")
                {
                    ViewBag.Title = "Liên hệ";
                }
            }
            else
            {
                page = _pageServices.GetPageByFiendlyUrl(friendlyUrl, lang);
                ViewBag.Title = page.Title;
            }

            return View(page);
        }

        public ActionResult ChangeLanguage(string lang)
        {
            string url = Request.UrlReferrer.ToString();
            Response.Cookies["culture"].Expires = DateTime.Now.AddDays(-1);
            SetLanguage(lang);
            return Redirect(url);
        }
    }
}