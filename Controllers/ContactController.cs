using DenKhangThinh.Models;
using DenKhangThinh.Services;
using DenKhangThinh.TemplateEngine;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Website.Controllers;

namespace DenKhangThinh.Controllers;

public class ContactController : SurfaceController
{
    private readonly IPublishedContentQuery _contentQuery;
    private readonly IViewRenderService _viewRenderService;
    private readonly UmbracoHelper _umbracoHelper;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;
    public ContactController(
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        IPublishedContentQuery contentQuery,
        IViewRenderService viewRenderService,
        UmbracoHelper umbracoHelper,
        IConfiguration configuration,
        IWebHostEnvironment environment
        ) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _contentQuery = contentQuery;
        _viewRenderService = viewRenderService;
        _umbracoHelper = umbracoHelper;
        _configuration = configuration;
        _environment = environment;
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult HandleContactForm(ContactModel model)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(model.CultureLcid);

        if (!ModelState.IsValid)
        {
            return Json(new
            {
                success = false,
                returnView = _viewRenderService.RenderToStringAsync("Contact/_Form", model),
                responseMessage = _umbracoHelper.GetDictionaryValue("Message.SendError") ?? "Đã có lỗi xảy ra trong quá trình gửi mail",
                responseType = _umbracoHelper.GetDictionaryValue("Title.Error") ?? "Thất bại"
            });
        }
        var textTemplate = new DotLiquidTemplate(Path.Combine(_environment.WebRootPath, "templates"));
        var sendTo = _configuration.GetValue<string>("EmailContactReceive") ?? "nhat05thud@gmail.com";
        var html = textTemplate.Render("EmailContact", new { name = model.Name, phone = model.Phone, service = model.Service });

        try
        {
            using (var message = new MailMessage("noreply@khangthinh.com", sendTo))
            {
                message.Subject = "Contact";
                message.Body = html;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    var netCre = new NetworkCredential("mastersender002@gmail.com", "iyueomvxqokclgtp");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = netCre;
                    smtp.Port = 587;
                    smtp.Send(message);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        model.Name = "";
        model.Phone = "";
        ModelState.Clear();

        return Json(new
        {
            success = true,
            returnView = _viewRenderService.RenderToStringAsync("Contact/_Form", model),
            responseMessage = _umbracoHelper.GetDictionaryValue("Message.SendSuccess") ?? "Gửi mail thành công",
            responseType = _umbracoHelper.GetDictionaryValue("Title.Success") ?? "Thành công"
        });
    }
}
