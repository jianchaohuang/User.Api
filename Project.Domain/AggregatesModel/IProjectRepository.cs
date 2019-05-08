using Project.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.AggregatesModel
{
    public interface IProjectRepository:IRepository<Project>
    {
        Task<Project> GetAsync(int id);
        Project Add(Project project);
        void Update(Project project);
    }
}
