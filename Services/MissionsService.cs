using MertPresentation.Database;
using MertPresentation.Dtos;
using MertPresentation.Helpers;
using MertPresentation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MertPresentation.Services
{
    public interface IMissionService
    {
        public Task<ApiResult> Add(MissionAddDto model);
        public Task<ApiResult> Update(MissionUpdateDto model);
        public Task<ApiResult> Delete(Guid Id);
        public Task<IList<MissionGetDto>> Get(String GetName = null);
    }
    public class MissionService : IMissionService
    {
        private readonly MertPresentationDBContext _context;

        public MissionService(MertPresentationDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResult> Add(MissionAddDto model)
        {
            var entity0 = await _context.People.Where(x => x.Name == model.Name).FirstOrDefaultAsync();
            if (entity0 == null)
            {
                var entity1 = new People
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow
                };

                entity1.Name = model.Name;
                await _context.People.AddAsync(entity1);
            }
            else
            {
                if (entity0.IsDeleted == true)
                    entity0.IsDeleted = false;
            }

            var entity2 = new Missions
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            entity2.Name = model.Name;
            entity2.Mission = model.Mission;

            await _context.Missions.AddAsync(entity2);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = model.Name, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Delete(Guid Id)
        {
            var entity = await _context.Missions.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return new ApiResult { Data = Id, Message = ApiResultMessages.MIE01 };
            }
            entity.IsDeleted = true;

            await _context.SaveChangesAsync();

            var entity1 = await _context.Missions.Where(x => x.Name == entity.Name).FirstOrDefaultAsync();
            if (entity1 == null)
            {
                var entity2 = await _context.People.Where(x => x.Name == entity.Name).FirstOrDefaultAsync();

                entity2.IsDeleted = true;

                await _context.SaveChangesAsync();
            }

            return new ApiResult { Data = entity.Name, Message = ApiResultMessages.Ok };
        }

        public async Task<IList<MissionGetDto>> Get(String GetName = null)
        {
            var result = new List<MissionGetDto>();

            if (GetName != null)
            {
                result = await _context
                .Missions
                .Where(x => !x.IsDeleted && x.Name == GetName)
                .Select(s => new MissionGetDto
                {
                    Id = s.Id,
                    CreatedAt = s.CreatedAt,
                    Name = s.Name,
                    Mission = s.Mission
                })
                .ToListAsync();
            }
            else
            {
                result = await _context
                    .Missions
                    .Where(x => !x.IsDeleted)
                    .Select(s => new MissionGetDto
                    {
                        Id = s.Id,
                        CreatedAt = s.CreatedAt,
                        Name = s.Name,
                        Mission = s.Mission
                    })
                    .ToListAsync();
            }

            return result;
        }

        public async Task<ApiResult> Update(MissionUpdateDto model)
        {
            var entity = await _context.Missions.Where(x => x.Id == model.Id && !x.IsDeleted).FirstOrDefaultAsync();
            if (entity == null)
            {
                return new ApiResult { Data = model.Id, Message = ApiResultMessages.MIE01 };
            }

            entity.Name = model.Name;
            entity.Mission = model.Mission;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }
    }
}