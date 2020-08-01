using DotNetCore.CAP;
using Recommend.API.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommend.API.IntegrationEventHandlers
{
    public class ProjectCreatedIntegrationEventHandler: IProjectCreatedIntegrationEventHandler, ICapSubscribe
    {
        [CapSubscribe("cap.test.queue")]
        public void CreateRecommendFromProject(ProjectCreatedIntegrationEvent @event)
        {
            Console.WriteLine($"ProjectId:{@event.ProjectId},UserId:{@event.UserId}");
        }
    }
}
