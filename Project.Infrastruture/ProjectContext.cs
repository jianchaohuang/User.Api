using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Project.Domain.Seedwork;
using Project.Infrastruture.EntityCofiguration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Infrastruture
{
    public class ProjectContext : DbContext, IUnitOfWork
    {
        private IMediator _mediator;
        public ProjectContext(DbContextOptions<ProjectContext> dbContext, IMediator mediator) :base(dbContext)
        {
            _mediator = mediator;
        }
        public DbSet<Domain.AggregatesModel.Project> Projects { get; set; }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            await base.SaveChangesAsync();
            return true;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectPropertyEntityConfiguration());
           
            //modelBuilder.Entity<Domain.AggregatesModel.Project>()
            //    .ToTable("Projects")
            //    .HasKey(p=>p.Id);

        }
    }

}
