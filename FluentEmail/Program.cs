using System;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmail.Smtp;
using FluentEmail.Core;
using System.Text;
using FluentEmail.Razor;
namespace FluentEmail
{
    class Program
    {
        static async Task Main(string[] args)
        {
             
            /// "localhost" for sending email to computer, but could be used with other mail servers
            var sender = new SmtpSender(() => new SmtpClient(host: "localhost")
            {
                // Saves to directory as a file. 
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
               // DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
               // PickupDirectoryLocation = @"C:\Demos"
            }) ;
            // inherits from FluentEmail.Razor Template
            StringBuilder template = new StringBuilder();
            template.AppendLine("Dear @Model.FirstName");
            template.AppendLine("<p> We have changed our transaction polcy. Please review carefully below for further details");
            template.AppendLine("-Wild World Games");
            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();
            // Sends email to client
            var email = await Email // Does actual sending
                .From("ramimorrar97@gmail.com")
                .To("Test@test.com", "Hana")
                .Subject("Regarding Transaction Policy")
                .UsingTemplate(template.ToString(), new { FirstName = "Rami", ProductName = "TrialsofPower" })
                //.Body("Hello Dear Customer, we have changed the way we do process transactions.")
                .SendAsync();
        }      
    }
}
