using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TimeStudy.Features.Timer;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System;

namespace TimeStudy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DownloadController : Controller
    {
        private readonly TimeStudyContext _timeStudyContext;
        private readonly TimeEntriesService _timeEntriesService;
        
        public DownloadController(TimeStudyContext timeStudyContext, TimeEntriesService timeEntriesService)
        {
            _timeStudyContext = timeStudyContext;
            _timeEntriesService = timeEntriesService;
        }
        
        public async Task<IActionResult> Index()
        {

            var entries = await _timeEntriesService.GetTimeEntries();

            byte[] output;

            await using (var memoryStream = new MemoryStream())
            {
                await using (var streamWriter = new StreamWriter(memoryStream))
                {
                    await using (var csvWriter =
                        new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentUICulture))
                    {
                        csvWriter.Context.RegisterClassMap<TimeEntryRowMap>();
                        csvWriter.WriteHeader<TimeEntryRow>();
                        await csvWriter.NextRecordAsync();
                        foreach (var record in entries)
                        {
                            csvWriter.WriteRecord(new
                            {
                                Email = record.Email,
                                CreateTime = record.CreatedDate,
                                Duration = record.Duration,
                                Category = record.Category.Name,
                                Description = record.Description
                            });
                            await csvWriter.NextRecordAsync();
                        }
                    }
                }

                output = memoryStream.ToArray();
            }

            return File(output, "text/csv", "report.csv");
        }
    }

    public class TimeEntryRow
    {
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }

    public class TimeEntryRowMap : ClassMap<TimeEntryRow>
    {
        public TimeEntryRowMap()
        {
            Map(m => m.Email).Index(0);
            Map(m => m.CreateTime).Index(1);
            Map(m => m.Duration).Index(2);
            Map(m => m.Category).Index(3);
            Map(m => m.Description).Index(4);
        }
    }
}