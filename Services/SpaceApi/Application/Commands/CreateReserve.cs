using AutoMapper;
using MediatR;
using SpaceApi.Domain.Models;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Commands
{
    public class CreateReserve
    {
        public class Command : IRequest<Reservation>
        {
            public Command(Reservation reservation)
            {
                this.reservation = reservation;
            }

            public Reservation reservation { get; }
        }

        public class Handler : IRequestHandler<Command, Reservation>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(ISpaceUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Reservation> Handle(Command request, CancellationToken cancellationToken)
            {
                var reserve = _mapper.Map<Reservation>(request.reservation);

                reserve = await _unitOfWork.ReservationRepository.AddAsync(reserve);
                await _unitOfWork.CommitAsync();

                return reserve;
            }
        }
    }
}
