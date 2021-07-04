using System;
using TimeStudy.Features.Categories;

namespace TimeStudy.Features.Timer
{
    public class TimeEntry
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public Category Category { get; set; }

    }
}