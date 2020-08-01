using Recommend.API.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommend.API.IntegrationEventHandlers
{
    public interface IProjectCreatedIntegrationEventHandler
    {
        void CreateRecommendFromProject(ProjectCreatedIntegrationEvent @event);
    }
}
