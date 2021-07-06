using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TimeStudy.Features.Timer
{
    public class TimeEntriesService
    {

        private readonly TimeStudyContext _timeStudyContext;

        public TimeEntriesService(TimeStudyContext timeStudyContext)
        {
            _timeStudyContext = timeStudyContext;
        }

        public async Task CreateTimeEntry(TimeEntry timeEntry)
        {
            _timeStudyContext.TimeEntry.Add(timeEntry);
            await _timeStudyContext.SaveChangesAsync();
        }
        
        public async Task<List<TimeEntry>> GetTimeEntries()
        {
            return await _timeStudyContext.TimeEntry
                .Include(entry => entry.Category)
                .ToListAsync();
        }

    }
}