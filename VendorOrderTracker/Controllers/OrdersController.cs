using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VendorOrderTracker.Models;

namespace VendorOrderTracker.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet("/vendors/{vendorId}/orders/new")]
        public ActionResult New(int vendorId)
        {
            Vendor vendor = Vendor.FindVendor(vendorId);
            return View(vendor);
        }

        [HttpGet("/vendors/{vendorId}/orders/{orderId}")]
        public ActionResult Show(int vendorId, int orderId)
        {
            Order order = Order.FindOrder(orderId);
            Vendor vendor = Vendor.FindVendor(vendorId);
            Dictionary<string, object> model = new()
            {
                { "vendor", vendor },
                { "order", order }
            };
            return View(model);
        }

        [HttpPost("/vendors/{vendorId}/orders/{orderId}")]
        public ActionResult Destroy(int vendorId, int orderId)
        {
            Vendor selectedVendor = Vendor.FindVendor(vendorId);
            Order selectedOrder = Order.FindOrder(orderId);
            selectedVendor.Orders.Remove(selectedOrder);
            Order.GetAll().Remove(selectedOrder);
            return RedirectToAction("Show", "Vendors", new { id = vendorId });
        }

        [HttpGet("vendors/{id}/orders/{orderId}/edit")]
        public ActionResult Edit(int vendorEditId, int editId)
        {
            Order order = Order.FindOrder(editId);
            Vendor vendor = Vendor.FindVendor(vendorEditId);
            Dictionary<string, object> model = new()
            {
                { "vendor", vendor },
                { "order", order }
            };
            return View(model);
        }

        [HttpPost("/vendors/{vendorId}/orders/{orderId}/update")]
        public ActionResult Update(int orderId, int vendorId, string newOrderTitle, string newOrderDescription, string newOrderPrice)
        {
            Order selectedOrder = Order.FindOrder(orderId);
            selectedOrder.Title = newOrderTitle;
            selectedOrder.Description = newOrderDescription;
            selectedOrder.Price = int.Parse(newOrderPrice);
            return RedirectToAction("Show", new { vendorId, orderId });
        }
    }
}
