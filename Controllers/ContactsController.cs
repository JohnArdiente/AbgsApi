using AbgsApi.Data;
using AbgsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbgsApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AbgsApiDbContext _context;
        public ContactsController(AbgsApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _context.Contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(Contact contact)
        {
            var contacts = new Contact()
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Address = contact.Address
            };

            await _context.Contacts.AddAsync(contacts);
            await _context.SaveChangesAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] int id, Contact contact)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            if(contacts != null)
            {
                contacts.Name = contact.Name;
                contacts.Email = contact.Email;
                contacts.Address = contact.Address;

                _context.Contacts.Update(contacts);
                await _context.SaveChangesAsync();
                return Ok(contacts);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(1);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
    }
}
