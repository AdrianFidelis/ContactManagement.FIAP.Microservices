// Contact.Persistence.API/Controllers/ContactController.cs
using ContactEntities = ContactManagement.Domain.Entities.Contact;
using ContactManagement.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Persistence.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContactEntities contact)
        {
            await _repository.AddAsync(contact);
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _repository.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null)
                return NotFound();
            return Ok(contact);
        }
    }
}
