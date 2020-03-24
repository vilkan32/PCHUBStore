using PCHUBStore.Areas.Administration.Models.CharacteristicsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public interface IAdminCharacteristicsServices
    {
        Task CreateCharacteristicsAsync(InsertCharacteristicsViewModel form);

        Task CreateCharacteristicsCategoryAsync(InsertCharacteristicsCategoryViewModel form);

        Task<bool> CategoryExistsAsync(string name);

        Task<bool> CharacteristicsExistsAsync(string name);

        Task<List<string>> GetAvailableCharacteristicsAsync();
    }
}
