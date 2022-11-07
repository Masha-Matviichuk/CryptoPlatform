using System.Threading.Tasks;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using DAL.Pagination;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;


        public EmailController(IMapper mapper, IEmailService emailService)
        {
            _mapper = mapper;
            _emailService = emailService;
        }

        // POST api/Email/send_message
        [HttpPost("send_message")]
        public async Task<IActionResult> Send([FromBody] MessageModel messageModel)
        {
            if (messageModel == null)
               return BadRequest(ModelState);
            
            var message = _mapper.Map<CustomerMailRequest>(messageModel);
            await _emailService.SendMessageToManagerAsync(message);
            return Ok(message);
        }
    }
}