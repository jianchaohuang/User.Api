using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{

    public class BaseController : Controller
    {
        protected UserIdentity UserIdentity
        {
            get
            {
                var identity = new UserIdentity();
                identity.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                identity.Name = User.Claims.FirstOrDefault(c => c.Type == "Name").Value;
                identity.Company = User.Claims.FirstOrDefault(c => c.Type == "Company").Value;
                identity.Title = User.Claims.FirstOrDefault(c => c.Type == "Title").Value;
                identity.Avatar = User.Claims.FirstOrDefault(c => c.Type == "Avatar").Value;
                return identity;
            }
        }
    }
}