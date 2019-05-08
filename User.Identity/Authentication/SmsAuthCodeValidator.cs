using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using User.Identity.Services;

namespace User.Identity.Authentication
{
    public class SmsAuthCodeValidator : IExtensionGrantValidator
    {
        private readonly IAuthCodeService _authCodeService;
        private readonly IUserService _userService;
        public string GrantType => "sms_auth_code";
        
        public SmsAuthCodeValidator(IAuthCodeService authCodeService,IUserService userService)
        {
            _authCodeService = authCodeService;
            _userService = userService;
        }
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var phone = context.Request.Raw["phone"];
            var code = context.Request.Raw["auth_code"];

            var errorValidationResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            if(string.IsNullOrWhiteSpace(phone)||string.IsNullOrWhiteSpace(code))
            {
                context.Result = errorValidationResult;
            }
            if(!_authCodeService.Validate(phone,code))
            {
                context.Result = errorValidationResult;
                return;
            }
            var userInfo = await _userService.CheckOrCreate(phone);
            if (userInfo==null)
            {
                context.Result = errorValidationResult;
                return;
            }
            var claim = new Claim[]
            {
                new Claim("UserId",userInfo.Id.ToString()??string.Empty),
                new Claim("Name",userInfo.Name??string.Empty),
                new Claim("Title",userInfo.Title??string.Empty),
                new Claim("Company",userInfo.Company??string.Empty),
                new Claim("Avatar",userInfo.Avatar??string.Empty)
            };
            context.Result = new GrantValidationResult(userInfo.Id.ToString(), GrantType, claim);
        }
    }
}
