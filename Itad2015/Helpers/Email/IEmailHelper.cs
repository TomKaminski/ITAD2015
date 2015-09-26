using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itad2015.ViewModels.Email;

namespace Itad2015.Helpers.Email
{
    public interface IEmailHelper<T>
        where T:EmailCommon
    {
        EmailHelper<T> AssignAll(string to, string from, string title);
        Task SendEmailAsync();
        void SendEmail();
        EmailHelper<T> AddAttachements(byte[][] datas, string[] names);
        EmailHelper<T> AddAttachement(byte[] data, string name);
        EmailHelper<T> AssignSender(string sender);
        EmailHelper<T> AssignRecipent(string recipent);
    }
}
