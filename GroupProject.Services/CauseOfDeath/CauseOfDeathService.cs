using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Data;

namespace GroupProject.Services.CauseOfDeath
{
    public class CauseOfDeathService : ICauseOfDeathService
    {
        private readonly ApplicationDbContext _dbContext;
        
        private readonly int _causeId;
        public CauseOfDeathService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    //todo Create

        public async Task<bool> CreateCauseOfDeathAsync(CauseCreate request)
        {

        }

    //todo GetAll

        public async Task<IEnumerable<CauseListItem>> GetAllCausesAsync(CauseListItem request)
        {

        }
    //todo GetByID

        public async Task<CauseModel> GetCauseById(int causeId)
        {

        }

    //todo Update

        public async Task<CauseModel> UpdateCause(int causeId)
        {

        }

    //todo Delete

        public async Task<bool> DeleteCause(int causeId)
        {
            
        }

    }
}