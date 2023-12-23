using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.DTOs;

namespace VoV.Services.Interface
{
    public interface IHelperService
    {
        string GetVirtualFilePath(string baseURL, Guid id, string type);
        Task<Tuple<List<DocumentDTO>, bool>> uploadHttpRequestFiles(Guid id, IFormFileCollection httpRequestFiles, string fileUploadType);
        Tuple<bool, string> SendMailWithAttachments(string MailTo, string MailCc, string Subject, string Message, List<System.Net.Mail.Attachment> files = null, AlternateView alternateView=null);
        string ConvertFileToBase64Sring(string filepath);
        void ConvertBase64SringToFile(string base64File, string path);
        string GetPhysicalFolderPath(Guid? id, string fileUploadType);
        Tuple<bool, string> SendInvitationMailWithAttachments(EmailEventModel eventModel, List<System.Net.Mail.Attachment> files = null);
        string GetInvitationBody(EmailEventModel eventModel);
        string GetMeetingEvent(EmailEventModel eventModel, bool isCancel = false);
    }
}
