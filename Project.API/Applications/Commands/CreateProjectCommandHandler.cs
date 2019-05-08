using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Domain.AggregatesModel;

namespace Project.API.Applications.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Domain.AggregatesModel.Project>
    {
        private IProjectRepository _repository;
        public CreateProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.AggregatesModel.Project> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Project.Domain.AggregatesModel.Project project = new Domain.AggregatesModel.Project(
                request.Project.UserId, 
                request.Project.Id, 
                request.Project.Company, 
                request.Project.Introduction
                );
             _repository.Add(project);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            return request.Project;
        }
    }
}
