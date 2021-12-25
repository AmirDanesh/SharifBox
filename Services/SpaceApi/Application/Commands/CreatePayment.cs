using AutoMapper;
using MediatR;
using SpaceApi.Domain.Models;
using SpaceApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Commands
{
    public class CreatePayment
    {
        public class Command : IRequest<Payment>
        {
            public Command(double amount, string userName, string userFullName, string type)
            {
                Amount = amount;
                UserName = userName;
                UserFullName = userFullName;
                Type = type;
            }

            public double Amount { get; }
            public string UserName { get; }
            public string UserFullName { get; }
            public string Type { get; }
        }

        public class Handler : IRequestHandler<Command, Payment>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(ISpaceUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Payment> Handle(Command request, CancellationToken cancellationToken)
            {
                var payment = _mapper.Map<Payment>(request);
                payment = await _unitOfWork.PaymentRepository.AddAsync(payment);
                await _unitOfWork.CommitAsync();

                return payment;
            }
        }
    }
}
