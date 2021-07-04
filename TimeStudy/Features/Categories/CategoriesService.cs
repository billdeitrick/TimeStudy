using System;
using TimeStudy;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TimeStudy.Features.Categories
{
 
    public class CategoriesService
    {
        private readonly TimeStudyContext _timeStudyContext;

        public CategoriesService(TimeStudyContext timeStudyContext)
        {
            _timeStudyContext = timeStudyContext;
        }

        public async Task CreateCategory(Category category)
        {
            _timeStudyContext.Add(category);
            await _timeStudyContext.SaveChangesAsync();
        }

        public async Task Update()
        {
            await _timeStudyContext.SaveChangesAsync();
        }

        public async Task<Category[]> GetCategories()
        {
            return await _timeStudyContext.Category
                .OrderBy(e => e.Name)
                .ToArrayAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _timeStudyContext.Category.SingleAsync(cat => cat.Id == id);
        }
    }
}