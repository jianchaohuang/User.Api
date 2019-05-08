using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Project.API.Applications.IntegrationEvents;
using Project.Domain.Events;
using MediatR;

namespace Project.API.Applications.DomainEventHandlers
{
    public class ProjectCreatedDomainEventHandler:INotificationHandler<ProjectCreatedEvent>
    {
        private ICapPublisher _capPublisher;
        public ProjectCreatedDomainEventHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }
        public Task Handle(ProjectCreatedEvent notification,CancellationToken cancellationToken)
        {
            var @event = new ProjectCreatedIntegrationEvent() {
                UserId=notification.Project.UserId,
                CreatedTime=DateTime.Now,
                ProjectId=notification.Project.Id
            };
            _capPublisher.Publish("finbook.projectapi.projectcreated",@event);
            return Task.CompletedTask;
        }
    }
}
