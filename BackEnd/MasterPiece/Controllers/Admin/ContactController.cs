using MasterPiece.DTO;
using MasterPiece.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterPiece.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts()
        {
            try
            {
                var contacts = await _contactService.GetAllContactsAsync();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<ActionResult<ContactDto>> CreateContact(ContactDto contactDto)
        {
            try
            {
                if (contactDto == null)
                {
                    return BadRequest("Contact information is required");
                }

                if (string.IsNullOrEmpty(contactDto.Email))
                {
                    return BadRequest("Email is required");
                }

                var result = await _contactService.CreateContactAsync(contactDto);
                return CreatedAtAction(nameof(GetContacts), new { id = result.Email }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}