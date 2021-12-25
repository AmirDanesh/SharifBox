using AutoMapper;
using Dto.Payment;
using MediatR;
using SpaceApi.Domain.Models;
using SpaceApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZarinPal.Class;

namespace SpaceApi.Application.Commands
{
    public class FinalizeReserve
    {
        public class Command : IRequest
        {
            public Command(Guid paymentId, string status, string authority)
            {
                PaymentId = paymentId;
                this.Status = status;
                Authority = authority;
            }

            public Guid PaymentId { get; }

            public string Status { get; }

            public string Authority { get; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IReservationRepository _reservationRepository;
            private readonly IPaymentRepository _paymentRepository;
            private readonly IMapper _mapper;

            public Handler(ISpaceUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _reservationRepository = unitOfWork.ReservationRepository;
                _paymentRepository = unitOfWork.PaymentRepository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var payment = await _paymentRepository.GetAsync(request.PaymentId);
                var reservation = (await _reservationRepository.FindByAsync(x => x.PaymentId == request.PaymentId)).FirstOrDefault();

                if (request.Status != "" && request.Status.ToLower() == "ok" && request.Authority != "")
                {
                    var expose = new Expose();
                    var _payment = expose.CreatePayment();
                    var verification = await _payment.Verification(new DtoVerification
                    {
                        Amount = payment.Amount,
                        MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                        Authority = request.Authority
                    }, ZarinPal.Class.Payment.Mode.sandbox);

                    if (verification.Status == 100 || verification.Status == 101)
                    {
                        payment.Status = Domain.Enums.PaymentStatus.Paid;
                        payment.PaymentDate = DateTime.UtcNow;

                        reservation.IsFinalized = true;
                    }
                }
                else
                {
                    payment.Status = Domain.Enums.PaymentStatus.Cancel;
                    payment.PaymentDate = DateTime.UtcNow;
                }

                await _paymentRepository.UpdateAsync(payment, payment.Id);
                await _reservationRepository.UpdateAsync(reservation, reservation.Id);
                await _unitOfWork.CommitAsync();

                return Unit.Value;
            }
        }
    }
}
