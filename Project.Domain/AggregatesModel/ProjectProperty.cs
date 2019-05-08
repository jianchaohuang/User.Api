using Project.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.AggregatesModel
{
    public class ProjectProperty: ValueObject
    {
        public int ProjectId { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public string  Value { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
        public ProjectProperty(string key,string text,string value)
        {
            Key = key;
            Text = text;
            Value = value;
        }
    }
}
