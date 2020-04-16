using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Services
{
    public interface IShipmentManagerServices
    {
        Task<List<Shipment>> QueryShipmentsAsync(ShipmentManagerIndexModel form);

        Task<List<Shipment>> GetShipmentsAsync(int page);

        int TotalNumberOfShipments();

        Task<bool> ShipmentExistsAsync(int id);

        Task<Shipment> GetShipmentAsync(int id);

        Task<string> AddActivityToShipmentAsync(int shipmentId, ActivityViewModel form, string username);

        Task EditShipmentAsync(int shipmentId,ShipmentViewModel form);

        Task EditActivityAsync(ActivityViewModel form);
    }
}
