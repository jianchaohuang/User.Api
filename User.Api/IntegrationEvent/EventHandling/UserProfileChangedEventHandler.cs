using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.IntegrationEvent.Events;

namespace User.Api.IntegrationEvent.EventHandling
{
    public class UserProfileChangedEventHandler: IUserProfileChangedEventHandler,ICapSubscribe
    {
        [CapSubscribe("cap.test.queue")]
        public void CheckReceivedMessage(UserProfileChangeEvent user)
        {

        }
    }
}
