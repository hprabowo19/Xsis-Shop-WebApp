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
    public class ProductAPIController : ApiController
    {
        private ProductRepository service = new ProductRepository();

        [HttpGet]
        public List<ProductViewModel> Get()
        {
            var result = service.GetAllProduct();
            return result;
        }

        [HttpGet]
        public ProductViewModel Get(int id)
        {
            var result = service.GetProductById(id);
            return result;
        }

        [HttpGet]
        public List<SupplierViewModel> GetSupplierList(string id)
        {
            var suppliers = service.GetSupplierList();
            var ListSupplier = new List<SupplierViewModel>();
            foreach(var supplier in suppliers)
            {
                var result = new SupplierViewModel();
                result.Id = supplier.Id;
                result.CompanyName = supplier.CompanyName;
                ListSupplier.Add(result);
            }
            return ListSupplier;
        }

        [HttpPost]
        public bool Post(ProductViewModel product)
        {
            try
            {
                service.CreateNewProduct(product);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(ProductViewModel product)
        {
            try
            {
                service.UpdateProduct(product);
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
                service.DeleteProduct(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
