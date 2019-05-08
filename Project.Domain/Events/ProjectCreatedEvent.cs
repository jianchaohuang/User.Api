using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Project.Domain.Events
{
    public class ProjectCreatedEvent :INotification
    {
        public int UserId { get; internal set; }
        public int Id { get; internal set; }
        public string Company { get; internal set; }
        public string Introduction { get; internal set; }
        public ProjectCreatedEvent(int userId,int id,string company,string introduction)
        {
            UserId = userId;
            Id = id;
            Company = company;
            Introduction = introduction;
        }
    }
}
