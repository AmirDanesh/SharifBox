using Microsoft.EntityFrameworkCore;
using SharifBox.Repository;
using SpaceApi.Domain.Context;
using SpaceApi.Domain.Enums;
using SpaceApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Repositories
{
    public interface IReservationRepository : IGenericRepository<SpaceDomainContext, Reservation>
    {
        Task<List<Space>> SpacesForReserve(string userName, SpaceType type, DateTime startDate, DateTime endDate);
    }

    public class ReservationRepository : GenericRepository<SpaceDomainContext, Reservation>, IReservationRepository
    {
        public ReservationRepository(SpaceDomainContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Space>> SpacesForReserve(string userName, SpaceType type, DateTime startDate, DateTime endDate)
        {
            var spaces = Context.Spaces
                .Include(x => x.Parent)
                .Include(x => x.RentAbleChair)
                .Include(x => x.RentAbleConferenceRoom)
                .Include(x => x.RentAbleRoom)
                .Where(x => x.Type == type && !x.IsDeleted && !x.IsDisabled).ToList().Where(x => x.IsRentable());

            var reservations = await Context.Reservations
                .Where(x => x.Type == type && (x.ValidUntil > DateTime.UtcNow || x.IsFinalized) &&
                       ((x.StartDate >= startDate && x.EndDate <= endDate) ||
                        (x.EndDate <= endDate && x.EndDate >= startDate) ||
                        (x.StartDate <= startDate && x.EndDate >= endDate))).ToListAsync();

            return spaces.Where(x => !reservations.Where(x => x.IsFinalized || x.UserName != userName).Select(s => s.SpaceId).Contains(x.Id)).ToList();
        }

    }
}
