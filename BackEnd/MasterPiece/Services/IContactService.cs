using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Services
{
        public interface IContactService
        {
            Task<IEnumerable<ContactDto>> GetAllContactsAsync();
            Task<ContactDto> CreateContactAsync(ContactDto contactDto);
        }

        public class ContactService : IContactService
        {
            private readonly MasterPieceContext _context;

            public ContactService(MasterPieceContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
            {
                var contacts = await _context.Contacts
                    .Select(c => new ContactDto
                    {
                        Name = c.Name,
                        Email = c.Email,
                        Message = c.Message
                    })
                    .ToListAsync();

                return contacts;
            }

            public async Task<ContactDto> CreateContactAsync(ContactDto contactDto)
            {
                var contact = new Contact
                {
                    Name = contactDto.Name,
                    Email = contactDto.Email,
                    Message = contactDto.Message
                };

                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return contactDto;
            }
        }
    }

