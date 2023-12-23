using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.DTOs
{
    [DataContract]
    public class SelectListDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public string Value { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Text { get; set; }
    }

    [DataContract]
    public class MultiSelectDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public string value { get; set; } = null!;
        [DataMember(EmitDefaultValue = false)]
        public string label { get; set; } = null!;
    }
    public class FailureModel
    {
        public FailureModel()
        {
            this.isSuccess = false;
        }

        //[DataMember(EmitDefaultValue = false)]
        //public string errorId { get; set; }
        //[DataMember(EmitDefaultValue = false)]
        //public string source { get; set; }
        //[DataMember(EmitDefaultValue = false)]
        //public string status { get; set; }
        //[DataMember(EmitDefaultValue = false)]
        //public int errorCode { get; set; }
        [DataMember]
        public bool isSuccess { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    [DataContract]
    public class EmailModel
    {
        public string From { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public string Recepients { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Attachment Attachments { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Isssl { get; set; }
    }

    public class EmailEventModel
    {
        public EmailEventModel()
        {
            this.Attendees = new List<string>();
        }
        public string EventName { get; set; }
        public string Addess { get; set; }
        public string EventDescription { get; set; }
        public string Location { get; set; }
        public string Summary { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<string> Attendees { get; set; }
    }


    [DataContract]
    public class EncryptDecryptDTO
    {
        [DataMember]
        public string Text { get; set; } = null!;
    }
}
