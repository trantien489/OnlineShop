using OnlineShop.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class InvoiceController : AdminController
    {
        // GET: Admin/Invoice
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetInvoices()
        {
            try
            {

                var invoice = DbContext.Invoices.Where(p => p.Status == true).OrderByDescending(p => p.CreatedDate).Include(p => p.ApplicationUser).ToList();
                if (invoice.Count() == 0)
                {
                    return Json(invoice, JsonRequestBehavior.AllowGet);
                }
                var resultInvoices = invoice.Select(i => new
                {
                    Id = i.Id,
                    Total = i.Total.ToString("N0") + " VND",
                    CreateDate = i.CreatedDate.Value.ToString("d/M/yyyy HH:mm"),
                    ReceiveData = i.InvoiceStatus == 2 ? i.ModifiedDate.Value.ToString("d/M/yyyy") : "",
                    InvoiceStatus = StringHelper.GetstringInvoiceStatus(i.InvoiceStatus.Value),
                    CustomerName = i.ApplicationUser.FirstName + " " + i.ApplicationUser.LastName,
                    NameReceive = i.NameReceive,
                    AddressReceive = i.AddressReceive,
                    PhoneReceive = i.PhoneReceive,
                    EmailReceive = i.EmailReceive,
                    CustomerId = i.ApplicationUser.CustomerId,
                    InvoiceStatusInt = i.InvoiceStatus.Value
                });
                return Json(resultInvoices, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    result = "Fail",
                    error = e.ToString()
                },JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetInvoiceDetailbyId(int invoiceId)
        {
            try
            {

                var invoice = DbContext.Invoices.FirstOrDefault(p => p.Status == true && p.Id == invoiceId);
                if (invoice != null)
                {
                    var invoiceDetails = DbContext.InvoiceDetails.Where(i => i.InvoiceId == invoiceId).Include(i=>i.Product).ToList()
                    .Select(i => new {
                        ProductName = i.Product.ProductName,
                        ProductImage = i.Product.Image,
                        Price = i.Product.Price.Value.ToString("N0")+" VND",
                        Quantity = i.Quantity,
                        Money = i.Money.Value.ToString("N0") + " VND"
                    });
                    return Json(invoiceDetails, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(null, JsonRequestBehavior.AllowGet);

                }
               
            }
            catch (Exception e)
            {
                return Json(new
                {
                    result = "Fail",
                    error = e.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateStatusInvoice(int invoiceId, int invoiceStatus)
        {
            var invoice = DbContext.Invoices.FirstOrDefault(p => p.Id == invoiceId);
            if (invoice != null)
            {
                invoice.InvoiceStatus = invoiceStatus;
                DbContext.SaveChanges();
                return Json(new
                {
                    result = "Success",
                    data = invoice
                });
            }
            return null;
        }


        [HttpPost]
        public JsonResult Delete(int invoiceId)
        {
            var invoice = DbContext.Invoices.FirstOrDefault(p => p.Id == invoiceId);
            if (invoice != null)
            {
                invoice.Status = false;
                DbContext.SaveChanges();
                return Json(new
                {
                    result = "Success",
                    data = invoice
                });
            }
            return null;
        }
    }
}