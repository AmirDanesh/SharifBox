using SharifBox.Repository;
using SpaceApi.Domain.Context;
using SpaceApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Repositories
{
    public interface IChairRepository : IGenericRepository<SpaceDomainContext, RentAbleChair>
    {

    }
    public class ChairRepository : GenericRepository<SpaceDomainContext, RentAbleChair>, IChairRepository
    {
        public ChairRepository(SpaceDomainContext dbContext) : base(dbContext)
        {
        }
    }
}
