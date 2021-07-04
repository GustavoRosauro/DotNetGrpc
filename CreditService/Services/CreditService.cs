using CreditService.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditService.Services
{
    public class CreditService : CreditServiceCheck.CreditServiceCheckBase
    {
        private readonly ILogger<CreditService> _logger;
        private static readonly Dictionary<string, Int32> customerTrustedCredit = new Dictionary<string, int>()
        {
            {"id0201", 10000},
            {"id0417", 5000},
            {"id0306", 15000}
        };
        public CreditService(ILogger<CreditService> logger)
        {
            _logger = logger;
        }
        public override Task<CreditReply> CheckCreditRequest(CreditRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CreditReply
            {
                IsAccepted = IsEligibleForCredit(request.CustomerId, request.Credit)
            });
        }
        private bool IsEligibleForCredit(string customerId, Int32 credit)
        {
            bool isEligible = false;

            if (customerTrustedCredit.TryGetValue(customerId, out Int32 maxCredit))
            {
                isEligible = credit <= maxCredit;
            }
            return isEligible;
        }
    }
}
