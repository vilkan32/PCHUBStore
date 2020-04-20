using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.CharacteristicsViewModels;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public class AdminCharacteristicsServices : IAdminCharacteristicsServices
    {
        private readonly PCHUBDbContext context;

        public AdminCharacteristicsServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCharacteristicsAsync(InsertCharacteristicsViewModel form)
        {
            var category = await this.context.AdminCharacteristicsCategories.FirstOrDefaultAsync(x => x.CategoryName == form.Category);

            foreach (var bc in form.BasicCharacteristics.Where(x => !string.IsNullOrEmpty(x)))
            {
                category.BasicCharacteristics.Add(new AdminCharacteristic
                {

                    Name = bc

                });
            }

            foreach (var fc in form.FullCharacteristics.Where(x => !string.IsNullOrEmpty(x)))
            {
                category.FullCharacteristics.Add(new AdminCharacteristic
                {

                    Name = fc

                });
            }

            await this.context.SaveChangesAsync();
        }

        public async Task CreateCharacteristicsCategoryAsync(InsertCharacteristicsCategoryViewModel form)
        {
            await this.context.AdminCharacteristicsCategories.AddAsync(new AdminCharacteristicsCategory
            {
                CategoryName = form.CategoryName,
                BasicCharacteristics = new List<AdminCharacteristic>(),
                FullCharacteristics = new List<AdminCharacteristic>(),

            });

            await context.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsAsync(string name)
        {
            return await this.context.AdminCharacteristicsCategories.AnyAsync(x => x.CategoryName == name);
        }

        public async Task<bool> CharacteristicsExistsAsync(string name)
        {
            return await this.context.AdminCharacteristicsCategories.AnyAsync(x => x.CategoryName == name &&
            x.BasicCharacteristics.Count > 0 &&
            x.FullCharacteristics.Count > 0);
        }

        public async Task<List<string>> GetAvailableCharacteristicsAsync()
        {
            return await this.context.AdminCharacteristicsCategories.Select(x => x.CategoryName).ToListAsync();
        }
    }
}
