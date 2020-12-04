using Hangfire.HttpJob.Client;
using System;
using System.Collections.Generic;

namespace HangfireClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverUrl = "http://localhost:5001/job";
            var result = HangfireJobClient.AddBackgroundJob(serverUrl, new BackgroundJob
            {
                JobName = "测试api",
                Method = "Get",
                Url = "http://localhost:5000/testaaa",
                Mail = new List<string> { "1877682825@qq.com" },
                SendSuccess = true,
                DelayFromMinutes = 1
            }, new HangfireServerPostOption
            {
                BasicUserName = "admin",
                BasicPassword = "test"
            });

            //var result = HangfireJobClient.AddRecurringJob(serverUrl, new RecurringJob()
            //{
            //    JobName = "测试5点40执行",
            //    Method = "Post",
            //    Data = new { name = "aaa", age = 10 },
            //    Url = "http://localhost:5001/testpost",
            //    Mail = new List<string> { "1877682825@qq.com" },
            //    SendSucMail = true,
            //    Cron = "40 17 * * *"
            //}, new HangfireServerPostOption
            //{
            //    BasicUserName = "admin",
            //    BasicPassword = "test"
            //});
        }
    }
}
