using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sendingEmails_.NetApi.DTO
{
    public class SendEmailDTO
    {
        [Required]
        public string ToEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
        public IList<IFormFile> Attachments{ get; set; }
    }
}
