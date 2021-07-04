using System.ComponentModel;
using TimeStudy.Features.Timer;
using Microsoft.EntityFrameworkCore;
using TimeStudy.Features.Categories;
using System;

namespace TimeStudy
{
    public class TimeStudyContext: DbContext
    {
        
        public TimeStudyContext(DbContextOptions<TimeStudyContext> options) : base(options) {}

        public DbSet<Category> Category { get; set; }
        public DbSet<TimeEntry> TimeEntry { get; set; }

    }
}