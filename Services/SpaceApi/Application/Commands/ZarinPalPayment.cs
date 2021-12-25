using Dto.Payment;
using Dto.Response.Payment;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using ZarinPal.Class;

namespace SpaceApi.Application.Commands
{
    public class ZarinPalPayment
    {
        public class Command : IRequest<string>
        {
            public Command(Guid paymentId, int amount, string userName, string description)
            {
                PaymentId = paymentId;
                Amount = amount;
                UserName = userName;
                Description = description;
            }

            public Guid PaymentId { get; }

            public int Amount { get; }

            public string UserName { get; }

            public string Description { get; }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly IConfiguration _configuration;

            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var expose = new Expose();
                var _payment = expose.CreatePayment();

                //var _authority = expose.CreateAuthority();
                //var _transactions = expose.CreateTransactions();

                Request result = await _payment.Request(new DtoRequest()
                {
                    Mobile = request.UserName,
                    CallbackUrl = _configuration["zarinCallBack"] + request.PaymentId,
                    Description = $"پرداخت فاکتور {request.Description}",

                    //Email = "farazmaan@outlook.com",
                    Amount = request.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);

                if (result.Status == 100)
                {
                    return $"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}";
                }

                throw new OperationCanceledException("خطای ارتباط با درگاه بانکی");
            }
        }
    }
}
