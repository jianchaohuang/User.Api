using DotNetCore.CAP;
using Recommend.API.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommend.API.IntegrationEventHandlers
{
    public class ProjectCreatedIntegrationEventHandler: ICapSubscribe
    {
        [CapSubscribe("cap.test.queue")]
        public void CreateRecommendFromProject(ProjectCreatedIntegrationEvent @event)
        {

        }
    }
}
