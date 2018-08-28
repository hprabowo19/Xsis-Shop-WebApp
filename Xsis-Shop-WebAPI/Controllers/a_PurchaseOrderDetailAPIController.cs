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
    public class a_PurchaseOrderDetailAPIController : ApiController
    {
        private a_PurchaseOrderDetailRepository service = new a_PurchaseOrderDetailRepository();

        [HttpGet]
        public List<a_PurchaseOrderDetailViewModel> Get()
        {
            var result = service.GetAlla_PurchaseOrderDetail();
            return result;
        }

        [HttpGet]
        public a_PurchaseOrderDetailViewModel Get(int id)
        {
            var result = service.Geta_PurchaseOrderDetailById(id);
            return result;
        }

        [HttpGet]
        public List<a_PurchaseOrderViewModel> Geta_PurchaseOrderList(string id)
        {
            var a_PurchaseOrders = service.Geta_PurchaseOrderList();
            var Lista_PurchaseOrder = new List<a_PurchaseOrderViewModel>();
            foreach (var a_PurchaseOrder in a_PurchaseOrders)
            {
                var result = new a_PurchaseOrderViewModel();
                result.ID = a_PurchaseOrder.ID;
                Lista_PurchaseOrder.Add(result);
            }
            return Lista_PurchaseOrder;
        }

        [HttpGet]
        public List<a_ProductViewModel> Geta_ProductList(string id)
        {
            var a_Products = service.Geta_ProductList();
            var Lista_Product = new List<a_ProductViewModel>();
            foreach (var a_Product in a_Products)
            {
                var result = new a_ProductViewModel();
                result.ID = a_Product.ID;
                result.Name = a_Product.Name;
                Lista_Product.Add(result);
            }
            return Lista_Product;
        }

        [HttpPost]
        public bool Post(a_PurchaseOrderDetailViewModel a_PurchaseOrderDetail)
        {
            try
            {
                service.CreateNewa_PurchaseOrderDetail(a_PurchaseOrderDetail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(a_PurchaseOrderDetailViewModel a_PurchaseOrderDetail)
        {
            try
            {
                service.Updatea_PurchaseOrderDetail(a_PurchaseOrderDetail);
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
                service.Deletea_PurchaseOrderDetail(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
