using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Itad2015.ViewModels.Email;

namespace Itad2015.Helpers.Email
{
    public class EmailHelper<T> : IEmailHelper<T> 
        where T : EmailCommon
    {
        private readonly T _email;

        public EmailHelper(T email)
        {
            _email = email;
        }

        public EmailHelper<T> AssignAll(string to, string from, string title)
        {
            _email.To = to;
            _email.From = from;
            _email.Title = title;
            return this;
        }

        public async Task SendEmailAsync()
        {
            await _email.SendAsync();
        }

        public void SendEmail()
        {
            _email.Send();
        }

        public EmailHelper<T> AddAttachements(byte[][] datas, string[] names)
        {
            for (var i = 0; i < names.Length; i++)
            {
                var ms = new MemoryStream(datas[i]);
                _email.Attachments.Add(new Attachment(ms, names[i]));
            }
            return this;
        }

        public EmailHelper<T> AddAttachement(byte[] data, string name)
        {
            var ms = new MemoryStream(data);
            _email.Attachments.Add(new Attachment(ms, name));
            return this;

        }

        public EmailHelper<T> AssignSender(string sender)
        {
            _email.From = sender;
            return this;
        }

        public EmailHelper<T> AssignRecipent(string recipent)
        {
            _email.To = recipent;
            return this;
        }
    }
}