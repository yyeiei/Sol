﻿using CommonSpider.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonSpider.Jobs
{
    public class CommonJob : IJob
    {
        Task IJob.Execute(IJobExecutionContext context)
        {
            var targetUrl = GetJobDataMap(context, "TargetUrl").ToString();
            var type = Type.GetType(context.JobDetail.JobDataMap["Service"].ToString());
            var data = context.JobDetail.JobDataMap["Data"] as Dictionary<string, object>;
            IService service = (IService)Activator.CreateInstance(type);
            return Task.Factory.StartNew(() =>
            {
                service.Init(targetUrl, data);
            });
        }

        private object GetJobDataMap(IJobExecutionContext context, string key)
        {
            return context.JobDetail.JobDataMap[key];
        }
    }
}
