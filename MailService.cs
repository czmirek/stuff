// Example mail service showcasing how in ASP.NET Core 2 can links be generated without HttpContext
// and using strongly typed routing referencing MVC Controller actions directly

namespace MailSericeTest
{
    using AspNet.Mvc.TypedRouting.LinkGeneration;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System;
    using System.Net;
    using System.Net.Mail;
    
    public class MailService
    {
        private readonly SmtpClient smtp;
        private readonly Uri publicUrl;
        private readonly LinkGenerator linkGenerator;
        private readonly IExpressionRouteHelper expressionRouteHelper;

        public MailService(Uri publicUrl, LinkGenerator linkGenerator, IExpressionRouteHelper expressionRouteHelper)
        {
            this.smtp = new SmtpClient("smtp host");
            this.publicUrl = publicUrl;
            this.linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
            this.expressionRouteHelper = expressionRouteHelper ?? throw new ArgumentNullException(nameof(expressionRouteHelper));
        }

        public void SendEmailConfirmation(string email, string confirmationKey)
        {
            var resolved = expressionRouteHelper.Resolve<ConfirmEmailPageController>(a => a.ConfirmEmail(confirmationKey));
            string link = GetLink(resolved);

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("example@example.com"),
                Subject = "Your email has been confirmed",
                BodyEncoding = System.Text.Encoding.UTF8,
                Body = $"Click on link to confirm your email: <a href=\"{link}\">{link}</a>"
            };

            mail.To.Add(email);
            smtp.Send(mail);
        }

        public void SendPasswordResetEmail(string email, string passwordKey)
        {
            var resolved = expressionRouteHelper.Resolve<SetNewPasswordPageController>(a => a.SetNewPassword(passwordKey));
            string link = GetLink(resolved);

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("example@example.com"),
                Subject = "You requested password reset"
                BodyEncoding = System.Text.Encoding.UTF8,
                Body =  $"Click on link to reset your password: <a href=\"{link}\">{link}</a>"
            };

            mail.To.Add(email);
            smtp.Send(mail);
        }

        private string GetLink(ExpressionRouteValues expressionRouteValues)
        {
            return linkGenerator.GetUriByAction
            (
                action: expressionRouteValues.Action,
                controller: expressionRouteValues.Controller, values: expressionRouteValues.RouteValues as object,
                scheme: publicUrl.Scheme,
                host: new HostString(publicUrl.Host, publicUrl.Port),
                pathBase: publicUrl.AbsolutePath
            );
        }
    }
}
