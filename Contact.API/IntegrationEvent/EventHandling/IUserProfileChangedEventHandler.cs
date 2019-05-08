using Contact.API.IntegrationEvent.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.IntegrationEvent.EventHandling
{
    public interface IUserProfileChangedEventHandler
    {
        void CheckReceivedMessage(UserProfileChangeEvent user);
    }
}
