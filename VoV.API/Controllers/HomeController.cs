using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VoV.Core.Helpers;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Helpers;
using VoV.Services.Interface;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;

namespace VoV.API.Controllers
{
    [Route("Home")]
    public class HomeController : BaseApiController
    {
        #region Properties
        VoVDbContext _dbContext;
        //Aes256Cipher _aes256Cipher = new Aes256Cipher();

        private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        private readonly IHelperService _helperService;
        #endregion

        #region Constructor
        public HomeController(IHttpContextAccessor contextAccessor,
                       VoVDbContext dbContext,
                       IHelperService helperService
                                              ) : base(contextAccessor)
        {
            _dbContext = dbContext;
            _helperService = helperService;
        }
        #endregion

        #region Methods
        [AllowAnonymous]
        [HttpGet]
        public string Get()
        {
            ErrorHelper e = new ErrorHelper();
            //int x = 10;
            //x = x / (x - x);
            try
            {
                e.getCurrentProjectPath();
                List<string> names = _dbContext.Roles.Select(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                string actionName = ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

                return (ex.Message);
            }
            Log.Error("Welcome to VoV API (DB Connected)!!!");
            return "Welcome to VoV API (DB Connected)!!!";
        }

        [Route("Encrypt")]
        [HttpPost]
        [AllowAnonymous]
        public string Encrypt(EncryptDecryptDTO model)
        {
            return AesOperation.EncryptString(model.Text);
        }

        [Route("Decrypt")]
        [HttpPost]
        [AllowAnonymous]
        public string DecryptWebConfig(EncryptDecryptDTO model)
        {
            return AesOperation.DecryptString(model.Text);
        }

        [Route("GetWeatherForecast")]
        [HttpGet]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Email")]
        public string Email()
        {
            //https://isidorebennett.azurewebsites.net/Articles/Send_Email_Invitation_From_MVC
            //string body = getBody();
            // _helperService.GetInvitationBody(eventModel)
            EmailEventModel eventModel = new EmailEventModel()
            {
                EventName = "VoV : Discussion about the project",
                EventDescription = "BoardMeeting",
                Location = "B2B Center Co-operative Premises Pvt Ltd.",
                StartDateTime = DateTime.Now.AddDays(1).AddHours(1),
                EndDateTime = DateTime.Now.AddDays(1).AddHours(5),
                Addess = "Goregaon(west)",
                Summary = "Sentient Systems Pvt. Lmtd. Summary",
                MailFrom = "Test PerfectSociety",
                MailTo = "sujashree.murugesh@sentientsystems.net"
            };
            string subject = "Vov-Meeting calendar event";
            string str = _helperService.GetMeetingEvent(eventModel);
            ContentType ct = new ContentType("text/calendar"); // MIME Type
            ct.Parameters.Add("method", "REQUEST");
            //ct.Parameters.Add("name", "invitation.ics");
            //AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), ct);
            //avCal.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
            byte[] bytes = Encoding.UTF8.GetBytes(str.ToString());
            MemoryStream stream = new MemoryStream(bytes);
            List<Attachment> attachments = new List<Attachment>();
            Attachment icsAttachment = new Attachment(stream, "invitation.ics", "text/calendar");
            attachments.Add(icsAttachment);
            var tuple = _helperService.SendMailWithAttachments(eventModel.MailTo, string.Empty, subject, _helperService.GetInvitationBody(eventModel), attachments, null);
            //AlternateView alternateView = AlternateView.CreateAlternateViewFromString(str.ToString(), ct);
            //var tuple = _helperService.SendMailWithAttachments(eventModel.MailTo, string.Empty, subject, null, attachments, alternateView);
            return "Email send successfully.";
        }

        //private string getBody()
        //{
        //    string s = $"<div class=\"calendar-invitebox\"><table border = \"0\" class=\"calendar-eventdetails\"><tbody>" +
        //        $"<tr><td class=\"ititle\">Invitation to</td>\r\n<td class=\"title\">this is title</td>\r\n</tr>\r\n" +
        //        $"<tr><td class=\"label\">Date</td>\r\n<td class=\"date\">2023-10-18 3:30 pm - 4:30 pm</td>\r\n</tr>\r\n" +
        //        $"<tr><td class=\"label\">Location</td>\r\n<td class=\"location\">locatin - goregaon</td>\r\n</tr>\r\n</tbody>\r\n</table>\r\n</div>";
        //    return s;
        //}

        #endregion
    }
}
