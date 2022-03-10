using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sendingEmails_.NetApi.Services
{
    public interface IMailServices
    {
       Task SendEmailAsync(string mailTo, string subject, string body,IList<IFormFile>attachments=null);
    }
}
