
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.IntegrationEvent.Events;

namespace User.Api.IntegrationEvent.EventHandling
{
    public interface IUserProfileChangedEventHandler
    {
        void CheckReceivedMessage(UserProfileChangeEvent user);
    }
}
