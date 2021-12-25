using SharifBox.Repository;
using SpaceApi.Domain.Context;
using SpaceApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Repositories
{
    public interface IRoomRepository : IGenericRepository<SpaceDomainContext, RentAbleRoom>
    {

    }
    public class RoomRepository : GenericRepository<SpaceDomainContext, RentAbleRoom>, IRoomRepository
    {
        public RoomRepository(SpaceDomainContext dbContext) : base(dbContext)
        {
        }
    }
}
