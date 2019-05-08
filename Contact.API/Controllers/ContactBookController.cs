using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.API.Data;
using Contact.API.IntegrationEvent.Events;
using Contact.API.Models;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactBookController : BaseController
    {
        IContactRespository _respository;
        public ContactBookController(IContactRespository respository)
        {
            _respository = respository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = UserIdentity.UserId;
            //ContactBooks contact = new ContactBooks();
            //contact.UserId = Guid.NewGuid().ToString("N");
            //contact.Contacts = new List<ContactInfo>();
            //ContactInfo contactInfo = new ContactInfo();
            //contactInfo.UserId= Guid.NewGuid().ToString("N");
            //contactInfo.Name = "菜哥";
            //contactInfo.Title = "洗地";
            //contactInfo.Company = "阿里";
            //contactInfo.Avatar = "C";
            //contact.Contacts.Add(contactInfo);
            //await _respository.AddContactBook(contact);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var result=await _respository.UpdateAsync();
            return Ok(result);
        }

    }
}