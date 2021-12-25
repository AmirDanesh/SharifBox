using SharifBox.Repository;
using SpaceApi.Domain.Context;

namespace SpaceApi.Domain.Repositories
{
    public interface ISpaceUnitOfWork : IUnitOfWork
    {
        public ISpaceRepository SpaceRepository { get; }
        public IReservationRepository ReservationRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IConferenceRoomRepository ConferenceRoomRepository { get; }
        public IRoomRepository RoomRepository { get; }
        public IChairRepository ChairRepository { get; }
    }

    public class SpaceUnitOfWork : UnitOfWork<SpaceDomainContext>, ISpaceUnitOfWork
    {
        public SpaceUnitOfWork(SpaceDomainContext contex) : base(contex)
        {
        }

        private ISpaceRepository _spaceRepository;
        private IReservationRepository _reservationRepository;
        private IPaymentRepository _paymentRepository;
        private IConferenceRoomRepository _conferenceRoomRepository;
        private IRoomRepository _roomRepository;
        private IChairRepository _chairRepository;

        public ISpaceRepository SpaceRepository => _spaceRepository ??= new SpaceRepository(Context);
        public IReservationRepository ReservationRepository => _reservationRepository ??= new ReservationRepository(Context);
        public IPaymentRepository PaymentRepository => _paymentRepository ??= new PaymentRepository(Context);
        public IConferenceRoomRepository ConferenceRoomRepository => _conferenceRoomRepository ??= new ConferenceRoomRepository(Context);
        public IRoomRepository RoomRepository => _roomRepository ??= new RoomRepository(Context);
        public IChairRepository ChairRepository => _chairRepository ??= new ChairRepository(Context);
    }
}