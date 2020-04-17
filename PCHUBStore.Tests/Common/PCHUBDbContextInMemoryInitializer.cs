using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using Microsoft.EntityFrameworkCore.InMemory;

namespace PCHUBStore.Tests.Common
{
    public class PCHUBDbContextInMemoryInitializer
    {
        public static PCHUBDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<PCHUBDbContext>()
               .UseLazyLoadingProxies()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())                   
                    .Options;


            return new PCHUBDbContext(options);
        }
    }
}
