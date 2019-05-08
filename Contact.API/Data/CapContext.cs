using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Data
{
    public class CapContext:DbContext
    {
        public CapContext(DbContextOptions options) : base(options)
        {
        }
    }
}
