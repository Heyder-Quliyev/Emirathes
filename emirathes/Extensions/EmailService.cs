using MailKit.Net.Smtp;
using MimeKit;

namespace emirathes.Extensions
{
    public interface IEmailService
    {
        //Task SendEmailAsync(string headTxt, string to, Order model);
        Task SendEmailAsync(string to, string subject, string message);
    }
    public class EmailService : IEmailService
    {
        public readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(emailSettings["FromName"], emailSettings["FromEmail"]));
            mimeMessage.To.Add(MailboxAddress.Parse(to));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain") { Text = message };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), true);
            var x = emailSettings["SmtpUsername"];
            await client.AuthenticateAsync(emailSettings["FromEmail"], emailSettings["SmtpPassword"]);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }

        //public async Task SendEmailAsync(string headTxt, string to, Order model)
        //{
        //    var emailSettings = _configuration.GetSection("EmailSettings");

        //    var mimeMessage = new MimeMessage();
        //    mimeMessage.From.Add(new MailboxAddress(emailSettings["FromName"], emailSettings["FromEmail"]));
        //    mimeMessage.To.Add(MailboxAddress.Parse(to));
        //    mimeMessage.Subject = "Fasteny Service";
        //    var namesHtml = "";
        //    foreach (var item in model.OrderToUsers)
        //    {
        //        namesHtml += $"<p>{item.ApplicationUsers.FirstName} {item.ApplicationUsers.LastName}</p>";
        //    }

        //    var htmlContent = $@"
        //    <div>
        //    <h1 style=""text-align: center; font-size: 24px;"">{headTxt}</h1>
        //     <hr>
        //     <div style=""text-align: center; margin: auto; max-width: 400px; border: 1px solid #ccc; border-radius: 5px; overflow: hidden; font-family: Arial, 'Helvetica Neue', Helvetica, sans-serif; box-shadow: 0 2px 3px rgba(0,0,0,0.1);"">
        //         <div style=""background-color: #f7f7f7; padding: 10px; font-weight: bold;"">Ticket Number: #{model.Id}</div>
        //         <div style=""padding: 20px;"">
        //             <span style=""display: block; margin-bottom: 5px;""><strong>Date:</strong> {model.WorkDate.ToString("MM-dd-yyyy")} {model.TimeRange.Name}</span>
        //             <span style=""display: block; margin-bottom: 5px;"">
        //                 <strong>Status:</strong> 
        //                 <span style=""display: inline-block; background-color:#8C8C8C; color:white; padding: 5px 10px; border-radius: 5px; font-size: 0.875rem;"">{model.Status.Name}</span>
        //             </span>
        //             <span style=""display: block; margin-bottom: 5px;""><strong>Type:</strong> {model.OrderType.Name}</span>
        //             <span style=""display: block; margin-bottom: 5px;""><strong>Customer Name:</strong> {model.Fullname}</span>
        //             <span style=""display: block; margin-bottom: 5px;""><strong>Phone:</strong> <a href=""tel:{model.Phone}"" style=""color: #007bff; text-decoration: none;"">{model.Phone}</a></span>
        //             <span style=""display: block; margin-bottom: 5px;"">
        //                 <strong>Address:</strong> 
        //                 <a href=""{model.Address}"" target=""_blank"" style=""color: #007bff; text-decoration: none;"">{model.Address}</a>
        //             </span>
        //             <span style=""display: block; margin-bottom: 10px;""><strong>Problem description:</strong> {model.ProblemDesc}</span>
        //             <a href=""https://fastenyservices.com/Order/Show/{model.Id}"" style=""background-color: #007bff; color: white; padding: 10px 15px; text-decoration: none; border-radius: 5px; font-size: 0.875rem; margin-right: 5px;"">Show</a>
        //         </div>
        //         <div style=""background-color: #f7f7f7; padding: 10px;"">
        //             <p>{namesHtml}</p>
        //         </div>
        //     </div>
        //     </div>
        //     ";



        //    mimeMessage.Body = new TextPart("html") { Text = htmlContent };

        //    using var client = new SmtpClient();
        //    await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), true);
        //    var x = emailSettings["SmtpUsername"];
        //    await client.AuthenticateAsync(emailSettings["FromEmail"], emailSettings["SmtpPassword"]);
        //    await client.SendAsync(mimeMessage);
        //    await client.DisconnectAsync(true);
        //}

    }

}
