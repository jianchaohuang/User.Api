﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.API.Applications.Services
{
    public interface IRecommendService
    {
        Task<bool> IsProjectInRecommend(int projectId,int userId);
    }
}
