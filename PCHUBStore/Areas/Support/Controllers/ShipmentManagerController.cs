using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.View.Models.Pagination;

namespace PCHUBStore.Areas.Support.Controllers
{
    public class ShipmentManagerController : SupportController
    {
        private readonly IShipmentManagerServices service;
        private readonly IMapper mapper;

        public ShipmentManagerController(IShipmentManagerServices service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(int? pageId)
        {
            int page = 1;
            if(pageId != null)
            {
                page = pageId.Value;
            }

            var shipments = await this.service.GetShipmentsAsync(page);
     
            var model = new ShipmentManagerIndexModel();
            model.AllShipments = new List<AllShipmentsViewModel>();

           
            shipments.ForEach(x =>
            {

                model.AllShipments.Add(this.mapper.Map<AllShipmentsViewModel>(x));

            });

            model.Pager = new Pager(this.service.TotalNumberOfShipments(), page, 20);

            return this.View(model);
        }

        [HttpGet("/api/SearchSipments")]
        public async Task<ActionResult<List<QueryShipmentsViewModel>>> SearchShipments(ShipmentManagerIndexModel form)
        {
           

            var result = await this.service.QueryShipmentsAsync(form);

            var model = new List<QueryShipmentsViewModel>();

            Console.WriteLine();

            result.ForEach(x =>
            {

                model.Add(new QueryShipmentsViewModel
                {
                    Address = x.User.Address,
                    ConfirmationStatus = x.ConfirmationStatus.ToString(),
                    PurchaseDate = x.PurchaseDate.Value,
                    ShipmentDetails = x.ShipmentDetails.ToString(),
                    FirstName = x.User.FirstName,
                    Id = x.Id,
                    LastName = x.User.LastName,
                    ProductsCount = x.ShipmentProducts.Sum(x => x.Quantity),
                    ShipmentTotalPrice = x.ShipmentProducts.Sum(x => x.Quantity * x.Product.Price),
                    ShippingCompany = x.ShippingCompany.ToString(),
                });

            });

            return model;
        }

        [HttpGet]
        public async Task<IActionResult> Ticket(int id)
        {

            if(await this.service.ShipmentExistsAsync(id))
            {

                var model = new ShipmentViewModel();

                var shipment = await this.service.GetShipmentAsync(id);

                var shipmentProduct = new List<ShipmentProductViewModel>();

                foreach (var sp in shipment.ShipmentProducts)
                {
                    shipmentProduct.Add(new ShipmentProductViewModel {

                        Id = sp.Product.Id,
                        Price = sp.Product.Price,
                        Quantity = sp.Quantity,
                        PictureUrl = sp.Product.MainPicture.Url,
                        Title = sp.Product.Title,                       
                        
                    });
                 
                }
                var activities = new List<ActivityViewModel>();
                foreach (var activity in shipment.Activities)
                {
                    activities.Add(new ActivityViewModel
                    {

                        Description = activity.Description,
                        CreationDate = activity.CreatedOn,
                        ActivityClosed = activity.ActivityClosed,
                        ActivityType = activity.ActivityType,
                        Id = activity.Id,
                        OwnerName = activity.Owner.UserName
                    });
                }

                model.Address = shipment.User.Address;
                model.FirstName = shipment.User.FirstName;
                model.LastName = shipment.User.LastName;
                model.Phone = shipment.User.PhoneNumber;
                model.City = shipment.User.City;
                model.PurchaseDate = shipment.PurchaseDate;
                model.ReceivedOn = shipment.ReceivedOn;
                model.ShipmentCoveredBy = shipment.ShipmentCoveredBy;
                model.ShipmentDetails = shipment.ShipmentDetails;
                model.ShipmentPrice = shipment.ShipmentProducts.Sum(x => x.Product.Price * x.Quantity);
                model.ShipmentProducts = shipmentProduct;
                model.Activities = activities;
                model.ShipmentId = shipment.Id;
                model.ShipmentImportancy = shipment.ShipmentImportancy;
                model.ShipmentStatus = shipment.ShipmentStatus;
                model.ShippingCompany = shipment.ShippingCompany;
                model.ShippingCompanyDetails = shipment.ShippingCompanyDetails;
                model.ConfirmationStatus = shipment.ConfirmationStatus;
                model.ClientResponse = shipment.ClientResponse;
                model.DeliveryConfirmationDate = shipment.DeliveryConfirmationDate;
                model.ShipmentProducts = shipmentProduct;
                model.Activities = activities;
                return this.View(model);
            }

            return this.Redirect("/Home/Error");

        }

        [HttpPost("/api/CreateActivity/{shipmentId}")]
        public async Task<ActionResult<ActivityViewModel>> CreateActivity(int shipmentId, ActivityViewModel form)
        {

            if(await this.service.ShipmentExistsAsync(shipmentId))
            {
                form.Id = await this.service.AddActivityToShipmentAsync(shipmentId, form, this.User.Identity.Name);

                if (form.Id == null)
                {
                    this.ModelState.AddModelError("Activity", "Invalid Activity Details");
                    return this.NotFound();
                }
            }

            return form;
        }

        ///api/EditActivity/@activity.Id
        [HttpPost("/api/EditActivity/{id}")]
        public async  Task<ActionResult<ActivityViewModel>> EditActivity(string id, ActivityViewModel form)
        {

            await this.service.EditActivityAsync(form);
            
            return form;
        }

        [HttpPost]
        public async Task<IActionResult> EditShipment(int shipmentId, ShipmentViewModel form)
        {

            await this.service.EditShipmentAsync(shipmentId, form);

            return this.RedirectToAction("Ticket", new { id = shipmentId });
        }
    }
}