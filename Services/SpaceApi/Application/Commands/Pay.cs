using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersianDate;
using SpaceApi.Application.Queries;
using SpaceApi.Domain.Enums;
using SpaceApi.Domain.Models;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Commands
{
    public class Pay
    {
        public class Command : IRequest<string>
        {
            public Command(string userName, Guid spaceId, string startDate, string startTime, string endTime, string endDate, int amount, SpaceType type)
            {
                UserName = userName;
                SpaceId = spaceId;
                StartDate = startDate;
                StartTime = startTime;
                EndTime = endTime;
                EndDate = endDate;
                Amount = amount;
                Type = type;
            }

            [Required]
            public string UserName { get; }
            [Required]
            public Guid SpaceId { get; }
            [Required]
            public string StartDate { get; }
            public string StartTime { get; }
            public string EndTime { get; }
            public string EndDate { get; }
            [Required]
            public int Amount { get; }
            public SpaceType Type { get; }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly ISpaceRepository _spaceRepository;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(ISpaceUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            {
                _unitOfWork = unitOfWork;
                _spaceRepository = unitOfWork.SpaceRepository;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                if ((await _spaceRepository.FindByAsync(x => x.Id == request.SpaceId)) == null)
                    throw new ArgumentException("ورودی نامعتبر است");

                #region dates

                var startTime = Convert.ToDateTime(request.StartTime);
                var endTime = Convert.ToDateTime(request.EndTime);

                var startDate = PersianDateTime.ConvertToDateTime(request.StartDate).Add(new TimeSpan(startTime.Hour, startTime.Minute, startTime.Second));
                var endDate = (request.Type == SpaceType.ConferenceRoom || request.Type == SpaceType.FlexChair) ?
                    PersianDateTime.ConvertToDateTime(request.StartDate).Add(new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second)) :
                    PersianDateTime.ConvertToDateTime(request.EndDate).Add(new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second));


                #endregion

                var reserve = _mapper.Map<Reservation>(request);
                reserve.StartDate = startDate;
                reserve.EndDate = endDate;

                var spacesForReserve =  await _mediator.Send(new GetAvailabeSpacesList.Query(
                        request.UserName,
                        request.Type,
                        startDate,
                        endDate));

                if (spacesForReserve.FirstOrDefault(x => x.SpaceId == request.SpaceId) != null)
                {
                    var exist_Reservation = await _unitOfWork
                        .ReservationRepository
                        .GetAll()
                        .Where(x => x.SpaceId == reserve.SpaceId && (x.ValidUntil > DateTime.UtcNow || !x.IsFinalized)).FirstOrDefaultAsync();

                    //TODO send UserFullName to Payment
                    var space = await _mediator.Send(new GetSpaceById.Query(request.SpaceId));
                    var payment = await _mediator.Send(new CreatePayment.Command(request.Amount, request.UserName, "", space.Type));

                    if (exist_Reservation == null)
                    {
                        reserve.PaymentId = payment.Id;
                        reserve = await _mediator.Send(new CreateReserve.Command(reserve));
                    }
                    else
                    {
                        exist_Reservation.PaymentId = payment.Id;
                        await _unitOfWork.ReservationRepository.UpdateAsync(exist_Reservation, exist_Reservation.Id);
                        await _unitOfWork.CommitAsync();
                    }

                    return await _mediator.Send(new ZarinPalPayment.Command(payment.Id, payment.Amount, payment.UserName, payment.Description));

                }
                else
                {
                    throw new InvalidOperationException("گزینه انتخاب شده قابل رزرو نمی‌باشد");
                }
            }
        }
    }
}
