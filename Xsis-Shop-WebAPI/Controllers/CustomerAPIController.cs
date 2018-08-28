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
    public class CustomerAPIController : ApiController
    {
        private CustomerRepository service = new CustomerRepository();

        [HttpGet]
        public List<CustomerViewModel> Get()
        {
            var result = service.GetAllCustomer();
            return result;
        }

        [HttpGet]
        public CustomerViewModel Get(int id)
        {
            var result = service.GetCustomerById(id);
            return result;
        }

        [HttpGet]
        public List<CustomerViewModel> Search(string id)
        {
            string[] ID = id.Split('|');

            string FullName = ID[0];
            string Place = ID[1];
            string Email = ID[2];

            var result = service.GetAllCustomer(FullName, Place, Email);
            return result;
        }

        [HttpGet]
        public bool CekNama(string id, string id2)
        {
            return service.CekNama(id, id2);
        }

        [HttpGet]
        public bool CekEmail(string id)
        {
            return service.CekEmail(id);
        }

        [HttpPost]
        public bool Post(CustomerViewModel customer)
        {
            try
            {
                service.CreateNewCustomer(customer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(CustomerViewModel customer)
        {
            try
            {
                service.UpdateCustomer(customer);
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
                service.DeleteCustomer(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
