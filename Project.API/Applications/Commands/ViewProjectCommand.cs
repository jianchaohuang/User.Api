using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.API.Applications.Commands
{
    public class ViewProjectCommand : IRequest<Domain.AggregatesModel.Project>
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public int ProjectId { get; set; }
    }
}
