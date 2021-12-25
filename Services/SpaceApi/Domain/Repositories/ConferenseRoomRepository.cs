using SharifBox.Repository;
using SpaceApi.Domain.Context;
using SpaceApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Repositories
{
    public interface IConferenceRoomRepository : IGenericRepository<SpaceDomainContext, RentAbleConferenceRoom>
    {

    }
    public class ConferenceRoomRepository : GenericRepository<SpaceDomainContext, RentAbleConferenceRoom>, IConferenceRoomRepository
    {
        public ConferenceRoomRepository(SpaceDomainContext dbContext) : base(dbContext)
        {
        }
    }
}
