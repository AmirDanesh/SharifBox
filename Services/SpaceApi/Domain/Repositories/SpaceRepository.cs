using Microsoft.EntityFrameworkCore;
using SharifBox.Repository;
using SpaceApi.Domain.Context;
using SpaceApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Repositories
{
    public interface ISpaceRepository : IGenericRepository<SpaceDomainContext, Space>
    {
        Task<Space> GetSpaceAsync(string svgId);

        Task<RentAbleChair> AddChairAsync(RentAbleChair e);

        Task<RentAbleConferenceRoom> AddConferenceRoomAsync(RentAbleConferenceRoom e);

        Task<RentAbleRoom> AddRoomAsync(RentAbleRoom e);

        Task<RentAbleConferenceRoom> GetConferenceRoomAsync(Guid spaceId);
        Task<RentAbleRoom> GetRoomAsync(Guid spaceId);
    }

    public class SpaceRepository : GenericRepository<SpaceDomainContext, Space>, ISpaceRepository
    {
        public SpaceRepository(SpaceDomainContext context) : base(context)
        {
        }

        public async Task<Space> GetSpaceAsync(string svgId) =>
            await Context.Spaces.FirstOrDefaultAsync(s => s.SvgId == svgId);

        public async Task<RentAbleChair> AddChairAsync(RentAbleChair e)
        {
            await Context.RentAblesChairs.AddAsync(e);
            return e;
        }

        public async Task<RentAbleConferenceRoom> AddConferenceRoomAsync(RentAbleConferenceRoom e)
        {
            await Context.RentAbleConferenceRooms.AddAsync(e);
            return e;
        }

        public async Task<RentAbleRoom> AddRoomAsync(RentAbleRoom e)
        {
            await Context.RentAbleRooms.AddAsync(e);
            return e;
        }

        public async Task<RentAbleConferenceRoom> GetConferenceRoomAsync(Guid spaceId) =>
            await Context.RentAbleConferenceRooms.FirstOrDefaultAsync(x => x.SpaceId == spaceId);


        public async Task<RentAbleRoom> GetRoomAsync(Guid spaceId) =>
            await Context.RentAbleRooms.FirstOrDefaultAsync(x => x.SpaceId == spaceId);
    }
}