using Microsoft.AspNetCore.Mvc;
using sendingEmails_.NetApi.DTO;
using sendingEmails_.NetApi.Services;
using System.Threading.Tasks;

namespace sendingEmails_.NetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailServices _ImailServices;
        public MailController(IMailServices imailService)
        {
            _ImailServices = imailService;
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailDTO dto)
        {
           await _ImailServices.SendEmailAsync(dto.ToEmail, dto.Subject, dto.Body, dto.Attachments);

            return Ok();
        }
    }
}
