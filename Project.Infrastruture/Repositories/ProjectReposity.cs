using Project.Domain.AggregatesModel;
using Project.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectEntity=Project.Domain.AggregatesModel.Project;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project.Infrastruture.Repositories
{
    public class ProjectReposity : IProjectRepository
    {
        private readonly ProjectContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ProjectReposity(ProjectContext context)
        {
             _context = context;
        }
        public ProjectEntity Add(ProjectEntity project)
        {
            if(project.IsTransient())
            {
                return _context.Add(project).Entity;
            }
            else
            {
                return project;
            }
        }

        public async Task<ProjectEntity> GetAsync(int id)
        {
            var project = await _context.Projects
                .Include(p=>p.Properties)
                .SingleOrDefaultAsync();
            return project;
        }

        public void Update(ProjectEntity project)
        {
            _context.Update(project);   
        }
    }
}
