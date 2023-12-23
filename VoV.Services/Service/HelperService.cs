using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VoV.Services.Interface;
using VoV.Data.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using VoV.Core.Enum;
using VoV.Core.Helpers;
using System.Net.Mime;

namespace VoV.Services.Service
{
    public class HelperService : IHelperService
    {
        #region Properties

        IConfiguration _configuration;
        //ILoggerManager _logger;
        private readonly ApplicationSettings _appSettings;
        private readonly IWebHostEnvironment _hostingEnvironment;

        #endregion

        #region Constructor        

        public HelperService(IConfiguration configuration
            // , ILoggerManager logger
            , IOptions<ApplicationSettings> appSettings,
            IWebHostEnvironment hostingEnvironment
           )
        {
            this._configuration = configuration;
            //this._logger = logger;
            this._appSettings = appSettings.Value;
            this._hostingEnvironment = hostingEnvironment;
            //this._httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        public Tuple<bool, string> SendMailWithAttachments(string MailTo, string MailCc, string Subject, string body, List<System.Net.Mail.Attachment> files = null, AlternateView alternateView = null)
        {
            try
            {
                if (string.IsNullOrEmpty(MailTo))
                {
                    return Tuple.Create(false, "MailTo address is required.");
                }

                EmailModel model = new EmailModel();
                model.Recepients = MailTo;
                model.Body = body;
                model.Subject = Subject;
                model.CC = MailCc;
                model.Host = _appSettings.SMTPHost.ToString();
                model.Port = _appSettings.SMTPPort;
                model.Sender = _appSettings.SMTPUserName;
                model.From = _appSettings.MailFrom.ToString();
                ///model.UseDefaultCredentials = _appSettings.SMTPUseDefaultCredentials;
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(model.From, model.Sender);
                    if (!string.IsNullOrEmpty(model.Recepients))
                    {
                        model.Recepients.Split(',').ToList().ForEach(x => mail.To.Add(new MailAddress(x)));
                    }
                    if (!string.IsNullOrEmpty(model.CC))
                    {
                        model.CC.Split(',').ToList().ForEach(x => mail.CC.Add(new MailAddress(x)));
                    }
                    if (!string.IsNullOrEmpty(model.BCC))
                    {
                        model.BCC.Split(',').ToList().ForEach(x => mail.Bcc.Add(new MailAddress(x)));
                    }

                    mail.Subject = model.Subject;
                    mail.Body = model.Body;
                    if (files != null)
                    {
                        foreach (Attachment attachment in files)
                        {
                            if (attachment != null)
                            {
                                string fileName = Path.GetFileName(attachment.Name);
                                mail.Attachments.Add(new Attachment(attachment.ContentStream, fileName));
                            }
                        }
                    }
                    mail.IsBodyHtml = true;
                    if (alternateView != null)
                        mail.AlternateViews.Add(alternateView);
                    //string Host = _configuration["Smtp:host"];
                    //int Port = Convert.ToInt16(_configuration["Smtp:Port"]);

                    using (SmtpClient smtp = new SmtpClient(_appSettings.SMTPHost, _appSettings.SMTPPort))
                    {
                        smtp.Credentials = new NetworkCredential(_appSettings.SMTPUserName, _appSettings.SMTPPassword);
                        smtp.EnableSsl = false;
                        smtp.Send(mail);

                        smtp.Dispose();
                    }

                    // SmtpClient smtp = new SmtpClient(Host, Port);
                    // smtp.Send(mail);
                    return Tuple.Create(true, "Email Sent Successfully");
                }
            }
            catch (SmtpException e)
            {
                Log.Error(e.Message + e.StackTrace);
                return Tuple.Create(false, e.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                return Tuple.Create(false, ex.Message);
            }
        }

        public Tuple<bool, string> SendInvitationMailWithAttachments(EmailEventModel eventModel, List<System.Net.Mail.Attachment> files = null)
        {
            string subject = eventModel.EventName;
            string str = this.GetMeetingEvent(eventModel);
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
            if (files != null)
            {
                attachments.AddRange(files);
            }
            var tuple = this.SendMailWithAttachments(eventModel.MailTo, string.Empty, subject, this.GetInvitationBody(eventModel), attachments, null);
            return tuple;
        }

        public string GetInvitationBody(EmailEventModel eventModel)
        {
            string body = $"<div class=\"calendar-invitebox\"><table border = \"0\" class=\"calendar-eventdetails\"><tbody>" +
                   $"<tr><td class=\"ititle\">Invitation to</td>\r\n<td class=\"title\">" + eventModel.EventName + "</td>\r\n</tr>\r\n";

            if (eventModel.StartDateTime.Date == eventModel.EndDateTime.Date)
            {
                body = body + $"<tr><td class=\"label\">Start Date</td>\r\n<td class=\"date\">" + eventModel.StartDateTime.ToString("yyyy-MMM-dd hh:mm tt") + " - " + eventModel.EndDateTime.ToString("hh:mm tt") + "</td>\r\n</tr>\r\n";
            }
            else
            {
                body = body + $"<tr><td class=\"label\">Start Date</td>\r\n<td class=\"date\">" + eventModel.StartDateTime.ToString("yyyy-MMM-dd hh:mm tt") + "</td>\r\n</tr>\r\n" +
                 $"<tr><td class=\"label\">End Date</td>\r\n<td class=\"date\">" + eventModel.EndDateTime.ToString("yyyy-MMM-dd hh:mm tt") + "</td>\r\n</tr>\r\n";
            }
            body = body + $"<tr><td class=\"label\">Location</td>\r\n<td class=\"location\">" + eventModel.Location + "</td>\r\n</tr>\r\n" +
             $"<tr><td class=\"label\">Description</td>\r\n<td class=\"description\">" + eventModel.EventDescription + "</td>\r\n</tr>\r\n" +
             $"<tr><td class=\"label\">Address</td>\r\n<td class=\"address\">" + eventModel.Addess + "</td>\r\n</tr>\r\n" +
             $"</tbody>\r\n</table>\r\n" +
            // $"<div class=\"itip-buttons\" id=\"itip-buttons-ec06f595-0545-4552-bdb6-227683c0b6f8\"><div id=\"loading-ec06f595-0545-4552-bdb6-227683c0b6f8\" class=\"rsvp-status loading\" style=\"display: none;\">Loading...</div><div class=\"rsvp-status\">Do you accept this invitation?</div>\r\n<div id=\"rsvp-ec06f595-0545-4552-bdb6-227683c0b6f8\" class=\"rsvp-buttons\" style=\"\"><input type=\"button\" class=\"button accepted\" onclick=\"rcube_libcalendaring.add_from_itip_mail('1.2:0', 'calendar', 'accepted', 'ec06f595-0545-4552-bdb6-227683c0b6f8')\" value=\"Accept\"><input type=\"button\" class=\"button tentative\" onclick=\"rcube_libcalendaring.add_from_itip_mail('1.2:0', 'calendar', 'tentative', 'ec06f595-0545-4552-bdb6-227683c0b6f8')\" value=\"Maybe\"><input type=\"button\" class=\"button declined\" onclick=\"rcube_libcalendaring.add_from_itip_mail('1.2:0', 'calendar', 'declined', 'ec06f595-0545-4552-bdb6-227683c0b6f8')\" value=\"Decline\"><input type=\"button\" class=\"button delegated\" onclick=\"rcube_libcalendaring.add_from_itip_mail('1.2:0', 'calendar', 'delegated', 'ec06f595-0545-4552-bdb6-227683c0b6f8')\" value=\"Delegate\"><input type=\"button\" class=\"button preview\" onclick=\"rcube_libcalendaring.open_itip_preview('./?_task=calendar&amp;view=agendaDay&amp;date=1697623200', 'INBOX/1843#1.2:0')\" value=\"Check Calendar\"><span class=\"folder-select\">save in&nbsp;<select name=\"calendar\" id=\"itip-saveto\">\r\n<option value=\"\">--</option>\r\n<option value=\"2514222\" selected=\"selected\">Default</option>\r\n</select>\r\n</span><div class=\"itip-reply-controls\"><label class=\"noreply-toggle\"><input type=\"checkbox\" id=\"noreply-ec06f595-0545-4552-bdb6-227683c0b6f8\" value=\"1\" class=\"material-input\"><label class=\"material checkbox\" for=\"noreply-ec06f595-0545-4552-bdb6-227683c0b6f8\" style=\"margin: 3px 3px 3px 4px;\"></label> Do not send a response</label>\r\n<a href=\"#toggle\" class=\"reply-comment-toggle\" onclick=\"$(this).hide().parent().find('textarea').show().focus()\">Enter a response text</a><div class=\"itip-reply-comment\"><textarea id=\"reply-comment-ec06f595-0545-4552-bdb6-227683c0b6f8\" name=\"_comment\" cols=\"40\" rows=\"6\" style=\"display:none\" placeholder=\"Invitation/notification comment\"></textarea></div>\r\n</div>\r\n</div>\r\n<div id=\"update-ec06f595-0545-4552-bdb6-227683c0b6f8\" style=\"display:none\"><input type=\"button\" class=\"button\" onclick=\"rcube_libcalendaring.add_from_itip_mail('1.2:0', 'calendar')\" value=\"Update in my calendar\"></div>\r\n<div id=\"import-ec06f595-0545-4552-bdb6-227683c0b6f8\" style=\"display:none\"><input type=\"button\" class=\"button\" onclick=\"rcube_libcalendaring.add_from_itip_mail('1.2:0', 'calendar')\" value=\"Save to my calendar\"></div>\r\n</div>\r\n<div class=\"calendar-agenda-preview\" style=\"display: block;\"><h3 class=\"preview-title\">Agenda <span class=\"date\">2023-10-18</span></h3>\r\n<div class=\"event-row\"><span class=\"event-date\">12:30 pm - 1:30 pm</span><span class=\"event-title\">This is Title</span></div>\r\n<div class=\"event-row current\"><span class=\"event-date\">3:30 pm - 4:30 pm</span><span class=\"event-title\">this is title</span></div>\r\n<div class=\"event-row no-event\">No later events</div>\r\n</div>"+
            $"</div>";
            return body;
        }

        //https://stackoverflow.com/questions/49358161/create-ics-file-and-send-email-with-attachment-using-c-sharp
        public string GetMeetingEvent(EmailEventModel eventModel, bool isCancel = false)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR"); // Init Calendar
            str.AppendLine("PRODID:-//test//EN"); // Identifier for the product that created the Calendar object
            str.AppendLine("VERSION:2.0"); // Indicates that the data is in iCalendar format
            str.AppendLine("METHOD:REQUEST"); // Request, Cancel
            str.AppendLine("CALSCALE:GREGORIAN");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", eventModel.StartDateTime)); // Start time            
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", eventModel.EndDateTime)); // End time
            str.AppendLine(string.Format("LOCATION: {0}", eventModel.Location)); // Location
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid())); // ID
            str.AppendLine(string.Format("DESCRIPTION:{0}", eventModel.EventDescription)); // Body
            str.AppendLine(string.Format("SUMMARY:{0}", eventModel.EventName)); // Subject    
            str.AppendLine(string.Format("STATUS:{0}", isCancel ? "CANCELLED" : "CONFIRMED")); // Confirmed, Cancelled
            str.AppendLine(string.Format("ORGANIZER;CN=\"{0}\":MAILTO:{1}", eventModel.MailFrom, eventModel.MailTo));

            foreach (string recipient in eventModel.Attendees)
                str.AppendLine(string.Format($@"ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN={recipient};X-NUM-GUESTS=0:MAILTO:{recipient}"));
            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR"); // End Calendar

            //str.AppendLine("BEGIN:VCALENDAR");
            //str.AppendLine("PRODID:-//Microsoft Corporation//Outlook 12.0 MIMEDIR//EN");
            //str.AppendLine("VERSION:2.0");
            //str.AppendLine(string.Format("METHOD:{0}", (isCancel ? "CANCEL" : "REQUEST")));
            //str.AppendLine("METHOD:REQUEST");
            //str.AppendLine("BEGIN:VEVENT");

            //str.AppendLine(string.Format("CREATED:{0:yyyyMMddTHHmmssZ}", DateTime.Now.ToUniversalTime()));

            //// Date format - yyyyMMddTHHmmssZ
            //str.AppendLine(string.Format("DTSTART:{0}", eventModel.StartDateTime));
            //str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmss}", DateTime.Now.ToUniversalTime()));
            //str.AppendLine(string.Format("DTEND:{0:}", eventModel.EndDateTime));

            //str.AppendLine(string.Format("LAST-MODIFIED:{0:yyyyMMddTHHmmssZ}", DateTime.Now.ToUniversalTime()));

            //// web meeting link
            //str.AppendLine(string.Format("LOCATION: {0}", eventModel.Location));
            ////or
            ////str.AppendLine(string.Format("LOCATION: {0}", mailTemplate.EventLocation));

            //str.AppendLine("PRIORITY: 5");
            //str.AppendLine("SEQUENCE: 0");

            //str.AppendLine(string.Format("UID:{0}", Guid.NewGuid().ToString()));
            //str.AppendLine(string.Format("DESCRIPTION:{0}", eventModel.EventDescription.Replace("\n", "<br>")));
            //str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", eventModel.EventDescription.Replace("\n", "<br>")));
            //str.AppendLine(string.Format("SUMMARY:{0}", eventModel.Summary));
            //str.AppendLine("STATUS:CONFIRMED");
            //str.AppendLine(string.Format("ORGANIZER;CN={0}:MAILTO:{1}", eventModel.MailFrom, eventModel.MailTo));
            //str.AppendLine(string.Format("ATTENDEE;CN={0};RSVP=TRUE:mailto:{1}", string.Join(",", eventModel.Attendees), string.Join(",", eventModel.Attendees)));

            //str.AppendLine("BEGIN:VALARM");
            //str.AppendLine("TRIGGER:-PT15M");
            //str.AppendLine("ACTION:DISPLAY");
            //str.AppendLine("DESCRIPTION:Reminder");
            //str.AppendLine("END:VALARM");
            //str.AppendLine("END:VEVENT");
            //str.AppendLine("END:VCALENDAR");

            return str.ToString();
        }

        public string GetPhysicalFolderPath(Guid? id, string fileUploadType)
        {
            string folderPath = string.Empty;
            string webRootPath = _hostingEnvironment.WebRootPath;
            //string contentRootPath = _hostingEnvironment.ContentRootPath;

            if (fileUploadType == FileUploadEnum.ProfileImage.ToString())
            {
                folderPath = webRootPath + _appSettings.DocStoreFolderPath + "Profile/" + id + "/";
            }
            else if (fileUploadType == FileUploadEnum.CompanyLogo.ToString())
            {
                folderPath = webRootPath + _appSettings.DocStoreFolderPath + "Logo/";
            }
            return folderPath;
        }

        public string GetVirtualFilePath(string baseURL, Guid id, string type)
        {
            WebHelper helper = new WebHelper();
            string virtualPath = string.Empty, folderPath = string.Empty;
            //string contentRootPath = _hostingEnvironment.ContentRootPath;
            string webRootPath = _hostingEnvironment.WebRootPath;

            if (type == FileUploadEnum.ProfileImage.ToString())
            {
                folderPath = webRootPath + "/" + _appSettings.DocStoreFolderPath + "Profile/" + id + "/";
                var tuple = helper.GetLatestFileDetailsFromFolder(folderPath);
                virtualPath = tuple.Item1.Replace(webRootPath, baseURL);
            }
            return virtualPath;
        }

        /// <summary>
        /// All FileNames list , isSuccess
        /// </summary>
        /// <param name="id"></param>
        /// <param name="httpRequestFiles"></param>
        /// <param name="fileUploadType"></param>
        /// <returns></returns>
        public async Task<Tuple<List<DocumentDTO>, bool>> uploadHttpRequestFiles(Guid id, IFormFileCollection httpRequestFiles, string fileUploadType)
        {
            List<DocumentDTO> docs = new List<DocumentDTO>();
            //https://stackoverflow.com/questions/47368044/mvc-core-1-1-image-will-not-show-on-view-cannot-find-path-or-something
            #region Upload Files           
            //var httpRequest = HttpContext.Request;
            //var httpRequestFiles = HttpContext.Request.Form.Files;
            if (httpRequestFiles.Count > 0)
            {
                //profile Image
                IFormFile formFile = null;
                if (fileUploadType == FileUploadEnum.ProfileImage.ToString())
                {
                    formFile = httpRequestFiles["profileImage"];
                }
                else if (fileUploadType == FileUploadEnum.CompanyLogo.ToString())
                {
                    formFile = httpRequestFiles["logoFile"];
                }
                var folderPath = GetPhysicalFolderPath(id, fileUploadType);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string extension = string.Empty, fileName = string.Empty, filePath = string.Empty;
                System.IO.DirectoryInfo directoryInfo = null;
                DocumentDTO doc = null;
                foreach (var file in httpRequestFiles)
                {
                    filePath = string.Empty;

                    #region file

                    if (formFile != null)
                    {
                        if (formFile.ContentType != null && !string.IsNullOrEmpty(formFile.FileName))
                        {
                            extension = System.IO.Path.GetExtension(file.FileName);
                            //Delete Images from folder if already exists                                
                            directoryInfo = new DirectoryInfo(folderPath);
                            //foreach (FileInfo imgFile in directoryInfo.GetFiles())
                            //{
                            //    imgFile.Delete();
                            //}
                            int fileCount = directoryInfo.GetFiles().Count() + 1;
                            //fileName = filePrefix + "_" + fileCount + extension;
                            if (fileUploadType == FileUploadEnum.CompanyLogo.ToString())
                            {
                                fileName = id.ToString() + extension;
                            }
                            else
                            {
                                fileName = file.FileName.Replace(extension, "") + "_" + fileCount + extension;
                            }
                            fileName = fileName.Replace("/", "_").Replace(" ", "");
                            filePath = folderPath + fileName;
                            doc = new DocumentDTO()
                            {
                                FileSize = file.Length,
                                FileName = fileName
                            };
                            docs.Add(doc);
                        }
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }

            }
            #endregion
            return Tuple.Create(docs, true);
        }

        public string ConvertFileToBase64Sring(string filepath)
        {
            Byte[] bytes = File.ReadAllBytes(filepath);
            string extension = System.IO.Path.GetExtension(filepath);
            String base64File = Convert.ToBase64String(bytes);
            return base64File;
        }

        public void ConvertBase64SringToFile(string base64File, string path)
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            //string folderPath = webRootPath + _appSettings.DocStoreFolderPath;

            Byte[] bytes = Convert.FromBase64String(base64File);
            File.WriteAllBytes(path, bytes);
        }


        #endregion
    }
}
