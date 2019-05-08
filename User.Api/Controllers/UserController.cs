using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.Api.data;
using User.Api.Dtos;
using User.Api.IntegrationEvent.Events;
using Microsoft.AspNetCore.JsonPatch;

namespace User.Api.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private UserContext _userContext;
        private ICapPublisher _capPublisher;
        private ILogger<UserController> _logger;
        public UserController(UserContext userContext, ICapPublisher capPublisher, ILogger<UserController> logger)
        {
            _userContext = userContext;
            _capPublisher = capPublisher;
            _logger = logger;
        }
        private void RaiseUserprofileChangeEvent(model.AppUser user)
        {
            if (_userContext.Entry(user).Property(nameof(user.Name)).IsModified
                || _userContext.Entry(user).Property(nameof(user.Title)).IsModified
                || _userContext.Entry(user).Property(nameof(user.Company)).IsModified
                || _userContext.Entry(user).Property(nameof(user.Avatar)).IsModified)
            {
                _capPublisher.Publish("finbook.userapi.userprofilechanged",
                    new UserIdentity()
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Avatar = user.Avatar,
                        Title = user.Title,
                        Company = user.Company
                    });
            }
        }
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = _userContext.Users
               // .AsNoTracking()
               .Include(u=>u.Properties)
                .SingleOrDefaultAsync(a => a.Id == UserIdentity.UserId);
            if (user.Result == null)
            {
                throw new UserOperationException($"错误的用户上下文Id{UserIdentity.UserId}");
            }  
            return Json(user.Result);
        }
        [Route("check-or-create")]
        [HttpPost]
        public async Task<IActionResult> CheckOrCreate(string phone)
        {
            var user = _userContext.Users.SingleOrDefault(u => u.Phone == phone);
            if (user == null)
            {
                user = new model.AppUser { Phone = phone };
                _userContext.Users.Add(user);
                await _userContext.SaveChangesAsync();
            }
            return Ok(new
            {
                user.Id,
                user.Name,
                user.Company,
                user.Title,
                user.Avatar
            });
        }

        [Route("publish-message")]
        [HttpPost]
        public async Task<IActionResult> PublishMessage()
        {
            using (var trans = _userContext.Database.BeginTransaction())
            {
                //指定发送的消息标题（供订阅）和内容

                _userContext.Users.Add(new model.AppUser() { Name = "Foo", Avatar = "caige", Company = "huawei", Title = "saodi" });
                _userContext.SaveChanges();

                _capPublisher.Publish("cap.test.queue",
                   new UserProfileChangeEvent { Name = "Foo", Avatar = "caige", Company = "huawei", Title = "saodi", UserId = 1 });
                // 你的业务代码。
                trans.Commit();
            }
            return Ok();
        }
        [Route("")]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody]JsonPatchDocument<model.AppUser> patch)
        {
            var user =await _userContext.Users.Include(u=>u.Properties).SingleOrDefaultAsync(a => a.Id == UserIdentity.UserId);
            patch.ApplyTo(user);
            _userContext.SaveChanges();
            return Json(user);
        }
    }
}
