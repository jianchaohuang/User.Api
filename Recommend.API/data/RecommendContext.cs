using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommend.API.Data
{
    public class RecommendContext:DbContext
    {
        public RecommendContext(DbContextOptions options) :base(options)
        {

        }
    }
}
