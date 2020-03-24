using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PCHUBStore.DemoTestEnvironment.DTOs
{
    public class AdvancedCharacteristicsDTO
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public bool HasArray { get; set; }

    }
}
