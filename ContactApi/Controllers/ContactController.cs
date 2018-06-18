using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.DataAccessLayer;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {


        #region WithRepository
        private IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public ActionResult<List<Contact>> GetAll()
        {
            return _contactRepository.GetContacts().ToList();
        }

        [HttpGet("{id}", Name = "GetContact")]
        public ActionResult<Contact> GetById(long id)
        {
            var item = _contactRepository.GetContactByID(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }


        [HttpPost]
        public IActionResult Create(Contact item)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.InsertContact(item);
                _contactRepository.Save();
                return CreatedAtRoute("GetContact", new { id = item.Id }, item);
            }
            else
                return BadRequest(item);


        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Contact item)
        {
            if (ModelState.IsValid)
            {
                var contact = _contactRepository.GetContactByID(id);
                if (contact == null)
                {
                    return NotFound();
                }
                _contactRepository.UpdateContact(item);
                return NoContent();
            }
            else
                return BadRequest(item);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var contact = _contactRepository.GetContactByID(id);
            if (contact == null)
            {
                return NotFound();
            }
            _contactRepository.DeleteContact(id);
            return NoContent();
        }

        #endregion



        #region Without Repository
        // private readonly ContactContext _context;

        // public ContactController(ContactContext context)
        // {
        //     _context = context;

        //     if (_context.Contacts.Count() == 0)
        //     {
        //         _context.Contacts.Add(new Contact { FirstName = "Brijesh", LastName = "Ghutugade", Email = "brijesha_ghutugade@yahoo.com", PhoneNumber = 9960269169, Status = Status.Active });
        //         _context.SaveChanges();
        //     }
        // }

        // [HttpGet]
        // public ActionResult<List<Contact>> GetAll()
        // {
        //     return _context.Contacts.ToList();
        // }

        // [HttpGet("{id}", Name = "GetContact")]
        // public ActionResult<Contact> GetById(long id)
        // {
        //     var item = _context.Contacts.Find(id);
        //     if (item == null)
        //     {
        //         return NotFound();
        //     }
        //     return item;
        // }


        // [HttpPost]
        // public IActionResult Create(Contact item)
        // {
        //     _context.Contacts.Add(item);
        //     _context.SaveChanges();

        //     return CreatedAtRoute("GetContact", new { id = item.Id }, item);
        // }


        // [HttpPut("{id}")]
        // public IActionResult Update(long id, Contact item)
        // {
        //     var contact = _context.Contacts.Find(id);
        //     if (contact == null)
        //     {
        //         return NotFound();
        //     }

        //     contact.FirstName = item.FirstName;
        //     contact.LastName = item.LastName;
        //     contact.Email = item.Email;
        //     contact.PhoneNumber = item.PhoneNumber;
        //     contact.Status = item.Status;
        //     _context.Contacts.Update(contact);
        //     _context.SaveChanges();
        //     return NoContent();
        // }


        // [HttpDelete("{id}")]
        // public IActionResult Delete(long id)
        // {
        //     var contact = _context.Contacts.Find(id);
        //     if (contact == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Contacts.Remove(contact);
        //     _context.SaveChanges();
        //     return NoContent();
        // }
        #endregion

    }
}