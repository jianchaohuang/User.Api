using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Api.Dtos;

namespace User.Api.Controllers
{
    public class BaseController : Controller
    {
        protected UserIdentity UserIdentity => new UserIdentity { UserId=1,Name="chaor"};
        //protected UserIdentity UserIdentity
        //{
        //    get
        //    {
        //        var identity=new UserIdentity();
        //        identity.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c=>c.Type=="UserId"));
        //        identity.Name = User.Claims.FirstOrDefault(c => c.Type == "Name").ToString();
        //        identity.Company = User.Claims.FirstOrDefault(c => c.Type == "Company").ToString();
        //        identity.Title = User.Claims.FirstOrDefault(c => c.Type == "Title").ToString();
        //        identity.Avatar = User.Claims.FirstOrDefault(c => c.Type == "Avatar").ToString();
        //        return identity;
        //    }
        //}
    }
}