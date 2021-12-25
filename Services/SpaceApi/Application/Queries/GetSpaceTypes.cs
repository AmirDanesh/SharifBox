using MediatR;
using Shared;
using SpaceApi.Domain.Enums;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Queries
{
    public class GetSpaceTypes
    {
        public class Query : IRequest<List<SpaceTypeDTO>>
        {
            public Query()
            {

            }
        }

        public class Handler : IRequestHandler<Query, List<SpaceTypeDTO>>
        {
            public async Task<List<SpaceTypeDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return EnumToKeyValue<SpaceType>();
            }

            private List<SpaceTypeDTO> EnumToKeyValue<T>() where T : Enum
            {
                var result = new List<SpaceTypeDTO>();
                foreach (var item in Enum.GetValues(typeof(T)).Cast<T>())
                {
                    var name = EnumUtility.GetDisplayValue(item);
                    result.Add(new SpaceTypeDTO() { Name = name ?? item.ToString(), Id = Convert.ToByte(item) });
                }
                return result;
            }
        }
    }
}
