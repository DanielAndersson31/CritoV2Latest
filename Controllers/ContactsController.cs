using CritoV2.Models;
using CritoV2.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace CritoV2.Controllers
{
    public class ContactsController : SurfaceController
    {
        public ContactsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        [HttpPost]
        public IActionResult Index(ContactForm contactForm)
        {
            if(!ModelState.IsValid)
                
                return CurrentUmbracoPage();
                

            using var mail = new MailService("no-reply@crito.com", "smtp.websupport.se", 465, "contact-form@crito.com", "CritoCMS123!");

            // To Sender
            mail.SendAsync(contactForm.Email, "Your contact request was received", "Hi, your request was received and we will be in contact with your as soon as possible.").ConfigureAwait(false);

            // To us
            mail.SendAsync("umbraco@crito.com", $"{contactForm.Name} sent a contact request.", contactForm.Message).ConfigureAwait(false);



            return LocalRedirect(contactForm.RedirectUrl ?? "/"); 



          /*  if (!string.IsNullOrWhiteSpace(contactForm.RedirectUrl))
                return LocalRedirect(contactForm.RedirectUrl);

            return LocalRedirect("/");*/
        }
    }
}
