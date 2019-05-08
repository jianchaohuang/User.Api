using Project.Domain.Events;
using Project.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Domain.AggregatesModel
{
    public class Project :Entity, IAggregateRoot
    {
        public int  UserId { get; set; }
        public int Id { get; set; }
        public string Company { get; set; }
        public string Introduction { get; set; }
        public string Avatar { get; set; }
        public string FinStage { get; set; }
        public string FinMoney { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Revenue { get; set; }
        public string Valution { get; set; }
        public string BrokerageOption { get; set; }
        public int SourceId { get; set; }
        public int ReferenceId { get; set; }
        public List<ProjectProperty> Properties { get; set; }
        public List<ProjectContributor> Contributors { get; set; }
        public List<ProjectViewer> Viewers { get; set; }

        public Project(int userId, int id, string company, string introduction)
        {
            Viewers = new List<ProjectViewer>();
            Contributors = new List<ProjectContributor>();
            var @event = new ProjectCreatedEvent(userId, id, company, introduction);
            AddDomainEvent(@event);
            Handle(@event);
        }
        public void AddViewer(int userId,string userName,string avatar)
        {
            var viewer = new ProjectViewer
            {
                UserId = userId,
                UserName = userName,
                Avatar = avatar,
                CreateTime = DateTime.Now
            };
            if (!Viewers.Any(v => v.UserId == userId))
            {
                Viewers.Add(viewer);
                AddDomainEvent(new ProjectViewedEvent() { Viewer = viewer });
            }
        }
        public void AddContributor(ProjectContributor contributor)
        {
            if (!Contributors.Any(c => c.UserId == contributor.UserId))
            {
                Contributors.Add(contributor);
                AddDomainEvent(new ProjectJoinedEvent() { Contributor = contributor });
            }

        }

        public void Handle(ProjectCreatedEvent e)
        {
            UserId = e.UserId;
            Id = e.Id;
            Company = e.Company;
            Introduction = e.Introduction;
        }
    }
}
