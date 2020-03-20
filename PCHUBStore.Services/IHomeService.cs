using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface IHomeService
    {
        Task<IndexPage> LoadIndexPageComponentsAsync();
    }
}
