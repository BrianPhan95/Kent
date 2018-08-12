using Kent.Business.Core.Models.Pages;
using Kent.Business.Services;
using Kent.Libary.Utilities;
using Kent.Web.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Kent.Libary.Enums.PageEnums;

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
                var headerContent = _headerTemplateServices.GetHeaderTemplateDefault().Content;
                var footerContent = _footerTemplateServices.GetFooterTemplateDefault().Content;

                page.HeaderContent = headerContent;
                page.FooterContent = footerContent;
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
            SetLanguage(lang);
            return Redirect(url);
        }
    }
}