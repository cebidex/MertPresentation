using MertPresentation.Database;
using MertPresentation.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MertPresentation.Services
{
    public interface IPeopleService
    {
        public Task<IList<PeopleGetDto>> Get();
    }

    public class PeopleService : IPeopleService
    {
        private readonly MertPresentationDBContext _context;

        public PeopleService(MertPresentationDBContext context)
        {
            _context = context;
        }
        public async Task<IList<PeopleGetDto>> Get()
        {
            var result = new List<PeopleGetDto>();

            result = await _context
                .People
                .Where(x => !x.IsDeleted)
                .Select(s => new PeopleGetDto
                {
                    Id = s.Id,
                    CreatedAt = s.CreatedAt,
                    Name = s.Name,
                })
                .ToListAsync();

            return result;
        }
    }
}