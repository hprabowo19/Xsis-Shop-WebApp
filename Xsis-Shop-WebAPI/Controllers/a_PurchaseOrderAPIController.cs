using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xsis_Shop_Repository;
using Xsis_Shop_ViewModels;
using Xsis_Shop_Models;

namespace Xsis_Shop_WebAPI.Controllers
{
    public class a_PurchaseOrderAPIController : ApiController
    {
        private a_PurchaseOrderRepository service = new a_PurchaseOrderRepository();

        [HttpGet]
        public List<a_PurchaseOrderViewModel> Get()
        {
            var result = service.GetAlla_PurchaseOrder();
            return result;
        }

        [HttpGet]
        public a_PurchaseOrderViewModel Get(int id)
        {
            var result = service.Geta_PurchaseOrderById(id);
            return result;
        }

        [HttpGet]
        public List<a_SupplierViewModel> Geta_SupplierList(string id)
        {
            var a_Suppliers = service.Geta_SupplierList();
            var ListSupplier = new List<a_SupplierViewModel>();
            foreach (var a_Supplier in a_Suppliers)
            {
                var result = new a_SupplierViewModel();
                result.ID = a_Supplier.ID;
                result.Name = a_Supplier.Name;
                ListSupplier.Add(result);
            }
            return ListSupplier;
        }

        [HttpPost]
        public bool Post(a_PurchaseOrderViewModel a_PurchaseOrder)
        {
            try
            {
                service.CreateNewa_PurchaseOrder(a_PurchaseOrder);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(a_PurchaseOrderViewModel a_PurchaseOrder)
        {
            try
            {
                service.Updatea_PurchaseOrder(a_PurchaseOrder);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                service.Deletea_PurchaseOrder(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
