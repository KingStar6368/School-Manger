using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient.Models.Results;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using School_Manager.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMS.Base
{
    public class SMSRunService : BackgroundService
    {
        private readonly ILogger<SMSRunService> logger;
        private readonly IServiceProvider provider;
        private readonly IParentService ParentService;
        private readonly IChildService ChildService;
        public SMSRunService(ILogger<SMSRunService> _logger,IServiceProvider _provider,IParentService _Pservice,IChildService _Cservice)
        {
            logger = _logger;
            provider = _provider;
            ParentService = _Pservice;
            ChildService = _Cservice;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("SMS Start!");
            //while (!stoppingToken.IsCancellationRequested)
            //{

            //}
            //throw new NotImplementedException();
            logger.LogInformation("SMS STOPED!");
            return null;
        }
    }
    public class SMSBase
    {
        private readonly SmsIr SMS;
        public SMSBase(string _APIKEY)
        {
            SMS = new SmsIr(_APIKEY);
        }
        public bool SendMessage(string[] Phone, string Message)
        {
            SmsIrResult result = SMS.BulkSend(300000000000, Message, Phone);
            return result.Status == 1 ? true : false;
        }
        public bool SendOTP(string Phone,string Message)
        {
            SmsIrResult result = SMS.VerifySend(Phone, 0, new VerifySendParameter[]
            {
                new VerifySendParameter("Code", Message)
            });
            return result.Status == 1 ? true : false;
        }
    }
}
