using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Tests.AdminServicesTests.JSONModel
{
    public class JsonProductModel
    {
        public string[] BasicChars { get; set; }

        public AdvancedCharacteristicsModel[] AdvancedChars { get; set; }
    }
}
