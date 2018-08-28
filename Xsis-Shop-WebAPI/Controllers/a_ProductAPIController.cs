using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xsis_Shop_Repository;
using Xsis_Shop_ViewModels;

namespace Xsis_Shop_WebAPI.Controllers
{
    public class a_ProductAPIController : ApiController
    {
        private a_ProductRepository service = new a_ProductRepository();

        [HttpGet]
        public List<a_ProductViewModel> Get()
        {
            var result = service.GetAlla_Product();
            return result;
        }

        [HttpGet]
        public a_ProductViewModel Get(int id)
        {
            var result = service.Geta_ProductById(id);
            return result;
        }

        [HttpGet]
        public List<a_ProductViewModel> Search(string id)
        {
            string[] ID = id.Split('|');

            string Name = ID[0];

            var result = service.GetAlla_Product(Name);
            return result;
        }

        [HttpPost]
        public bool Post(a_ProductViewModel a_Product)
        {
            try
            {
                service.CreateNewa_Product(a_Product);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(a_ProductViewModel a_Product)
        {
            try
            {
                service.Updatea_Product(a_Product);
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
                service.Deletea_Product(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}