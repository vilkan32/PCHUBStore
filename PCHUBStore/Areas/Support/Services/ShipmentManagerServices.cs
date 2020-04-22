using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Services
{
    public class ShipmentManagerServices : IShipmentManagerServices
    {
        private readonly PCHUBDbContext context;

        public ShipmentManagerServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        public async Task<string> AddActivityToShipmentAsync(int shipmentId, ActivityViewModel form, string username)
        {
            var shipment = await this.context.Shipments.FirstOrDefaultAsync(x => x.Id == shipmentId);
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if(form.OwnerName != null && form.Description != null && form.CreationDate != null && form.ActivityType != null)
            {
                var activity = new Activity { Description = form.Description, ActivityClosed = form.ActivityClosed, Owner = user, ActivityType = (ActivityType)form.ActivityType, CreatedOn = DateTime.UtcNow };
                shipment.Activities.Add(activity);
                await this.context.SaveChangesAsync();

                return activity.Id;
            }
            else
            {
                return null;
            }
  
        }

        public async Task EditActivityAsync(ActivityViewModel form)
        {
            var activity = await this.context.Activities.FirstOrDefaultAsync(x => x.Id == form.Id);

            activity.ModificationDate = DateTime.UtcNow;

            activity.Description = form.Description;
            activity.ActivityType = (ActivityType)form.ActivityType;
            activity.ActivityClosed = form.ActivityClosed;
            activity.CreatedOn = form.CreationDate.Value;

            await this.context.SaveChangesAsync();
        }

        public async Task EditShipmentAsync(int shipmentId, ShipmentViewModel form)
        {
            var shipment = await this.context.Shipments.FirstOrDefaultAsync(x => x.Id == shipmentId);
 
            shipment.ReceivedOn = form.ReceivedOn;
            shipment.ShipmentCoveredBy = form.ShipmentCoveredBy;
            shipment.ShipmentDetails = form.ShipmentDetails;
            shipment.ShipmentImportancy = form.ShipmentImportancy;
            shipment.ShipmentStatus = form.ShipmentStatus;
            shipment.ShippingCompanyDetails = form.ShippingCompanyDetails;
            shipment.ConfirmationStatus = form.ConfirmationStatus;
            shipment.ClientResponse = form.ClientResponse;
            shipment.DeliveryConfirmationDate = form.DeliveryConfirmationDate;

            await this.context.SaveChangesAsync();
        }


        public Task<Shipment> GetShipmentAsync(int id)
        {
            return this.context.Shipments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Shipment>> GetShipmentsAsync(int page)
        {
           return await this.context.Shipments.OrderByDescending(x => x.PurchaseDate).Skip((page - 1) * 20).Take(20).ToListAsync();

        }
        
        public async Task<List<Shipment>> QueryShipmentsAsync(ShipmentManagerIndexModel form)
        {
            var result = new List<Shipment>();

            if(form.Address != null)
            {
                var shipments = await this.context.Shipments.Where(x => x.User.Address == form.Address).ToListAsync();

                foreach (var shipment in shipments)
                {
                    if(!result.Any(x => x.Id == shipment.Id))
                    {
                        result.Add(shipment);
                    }
                }

            }

            if (form.ConfirmationStatus != null)
            {
                var shipments = await this.context.Shipments.Where(x => x.ConfirmationStatus == form.ConfirmationStatus).ToListAsync();

                foreach (var shipment in shipments)
                {
                    if (!result.Any(x => x.Id == shipment.Id))
                    {
                        result.Add(shipment);
                    }
                }
            }

            if (form.FirstName != null)
            {
                var shipments = await this.context.Shipments.Where(x => form.FirstName == x.User.FirstName).ToListAsync();

                foreach (var shipment in shipments)
                {
                    if (!result.Any(x => x.Id == shipment.Id))
                    {
                        result.Add(shipment);
                    }
                }

            }

            if (form.Id != null)
            {

                var shipments = await this.context.Shipments.Where(x => form.Id == x.Id).ToListAsync();

                foreach (var shipment in shipments)
                {
                    if (!result.Any(x => x.Id == shipment.Id))
                    {
                        result.Add(shipment);
                    }
                }

            }

            if (form.LastName != null)
            {

                var shipments = await this.context.Shipments.Where(x => form.LastName == x.User.LastName).ToListAsync();

                foreach (var shipment in shipments)
                {
                    if (!result.Any(x => x.Id == shipment.Id))
                    {
                        result.Add(shipment);
                    }
                }
            }

            if (form.PurchaseDate != null)
            {
                var shipments = await this.context.Shipments.Where(x => form.PurchaseDate.Value.Month == x.PurchaseDate.Value.Month &&

                form.PurchaseDate.Value.Day == x.PurchaseDate.Value.Day &&

                form.PurchaseDate.Value.Year == x.PurchaseDate.Value.Year

                ).ToListAsync();

                foreach (var shipment in shipments)
                {
                    if (!result.Any(x => x.Id == shipment.Id))
                    {
                        result.Add(shipment);
                    }
                }            
            }

            if (form.ShippingCompany != null)
            {
                var shipments = await this.context.Shipments.Where(x => form.ShippingCompany == x.ShippingCompany).ToListAsync();

                foreach (var shipment in shipments)
                {
                    if (!result.Any(x => x.Id == shipment.Id))
                    {
                        result.Add(shipment);
                    }
                }
            }

            return result;
        }

        public async Task<bool> ShipmentExistsAsync(int id)
        {
            return await this.context.Shipments.AnyAsync(x => x.Id == id);
        }

        public int TotalNumberOfShipments()
        {
            return  this.context.Shipments.Count();
        }
    }
}
