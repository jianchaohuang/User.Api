using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Domain.AggregatesModel;

namespace Project.API.Applications.Commands
{
    public class ViewProjectCommandHandler : IRequestHandler<ViewProjectCommand,Domain.AggregatesModel.Project>
    {
        private IProjectRepository _repository;
        public ViewProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<Domain.AggregatesModel.Project> Handle(ViewProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetAsync(request.ProjectId);
            if(project==null)
            {
                throw new Domain.Exceptions.ProjectDomainException("not find projectId" + request.ProjectId);
            }
            project.AddViewer(request.UserID,request.UserName,request.Avatar);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            return project;
        }


    }
}
