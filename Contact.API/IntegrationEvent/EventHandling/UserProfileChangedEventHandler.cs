using Contact.API.IntegrationEvent.Events;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.IntegrationEvent.EventHandling
{
    public class UserProfileChangedEventHandler: IUserProfileChangedEventHandler,ICapSubscribe
    {
        [CapSubscribe("cap.test.queue")]
        public void CheckReceivedMessage(UserProfileChangeEvent user)
        {

        }
    }
}
