using DomainContact = ContactManagement.Domain.Entities.Contact;
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
        public async Task<IActionResult> Post(DomainContact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(contact);
            return CreatedAtAction(nameof(Post), new { id = contact.Id }, contact);
        }
    }
}
