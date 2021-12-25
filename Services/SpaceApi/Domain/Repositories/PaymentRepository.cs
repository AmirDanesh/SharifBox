using SharifBox.Repository;
using SpaceApi.Domain.Context;
using SpaceApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Repositories
{
    public interface IPaymentRepository : IGenericRepository<SpaceDomainContext, Payment>
    {

    }
    public class PaymentRepository : GenericRepository<SpaceDomainContext, Payment>, IPaymentRepository
    {
        public PaymentRepository(SpaceDomainContext dbContext) : base(dbContext)
        {
        }
    }
}
