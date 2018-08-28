using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xsis_Shop_Models;
using Xsis_Shop_ViewModels;

namespace Xsis_Shop_Repository
{
    public class CustomerRepository
    {
        // SELECT * FROM Customer
        public List<CustomerViewModel> GetAllCustomer()
        {
            using(ShopDBEntities db = new ShopDBEntities())
            {
                var ListCustomer = db.Customer.ToList();
                List<CustomerViewModel> ListView = new List<CustomerViewModel>();
                foreach (var customer in ListCustomer)
                {
                    CustomerViewModel Model = new CustomerViewModel();
                    Model.Id = customer.Id;
                    Model.FirstName = customer.FirstName;
                    Model.LastName = customer.LastName;
                    Model.City = customer.City;
                    Model.Country = customer.Country;
                    Model.Phone = customer.Phone;
                    Model.Email = customer.Email;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        public List<CustomerViewModel> GetAllCustomer(string FullName, string Place, string Email)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<Customer> ListCustomer;

                bool NullFullName = string.IsNullOrWhiteSpace(FullName);
                FullName = NullFullName ? " " : FullName.ToLower();
                string FirstName = FullName.Split().Take(2).First();
                string LastName = FullName.Split().Take(2).Last();
                bool NullLastName = string.IsNullOrWhiteSpace(LastName);

                bool NullPlace = string.IsNullOrWhiteSpace(Place);
                Place = NullPlace ? " " :Place.ToLower();

                bool NullEmail = string.IsNullOrWhiteSpace(Email);
                Email = NullEmail ? " ": Email.ToLower();

                if (NullFullName && NullPlace && NullEmail) // NULL
                    ListCustomer = db.Customer.ToList();

                else if (NullPlace && NullEmail) // Hanya Fullname
                    ListCustomer = db.Customer.Where(p => (
                    p.FirstName.Contains(FullName) || 
                    p.LastName.Contains(FullName) ||
                    (p.FirstName.Contains(FirstName) && p.LastName.Contains(LastName))
                    )).ToList();

                else if (NullFullName && NullEmail)  // Hanya Kota/Negara
                    ListCustomer = db.Customer.Where(p => (
                        p.City == Place || p.Country == Place
                    )).ToList();

                else if (NullFullName && NullPlace)  // Hanya Email
                    ListCustomer = db.Customer.Where(p => (
                        p.Email == Email
                    )).ToList();

                else if (NullEmail)  // Fullname dan Kota/Negara
                    ListCustomer = db.Customer.Where(p => (
                        (p.FirstName.Contains(FullName) || p.LastName.Contains(FullName) || (p.FirstName.Contains(FirstName)) && p.LastName.Contains(LastName)) &&
                        (p.City == Place || p.Country == Place)
                    )).ToList();

                else if (NullPlace) // Fullname dan Email
                    ListCustomer = db.Customer.Where(p => (
                        (p.FirstName.Contains(FullName) || p.LastName.Contains(FullName) || (p.FirstName.Contains(FirstName) && p.LastName.Contains(LastName))) &&
                        (p.Email == Email)
                    )).ToList();

                else if (NullFullName) // Kota/Negara dan Email
                    ListCustomer = db.Customer.Where(p => (
                        (p.City == Place || p.Country == Place) &&
                        (p.Email == Email)
                    )).ToList();

                else // Semua Terisi
                {
                    //ListCustomer = db.Customer.Where(p => (p.FirstName.Contains(FullName) || p.LastName.Contains(FullName) || (p.FirstName.Contains(FirstName) && p.LastName.Contains(LastName))) && (p.City == Place || p.Country == Place) && (p.Email == Email)).ToList();
                    ListCustomer = (
                        from p in db.Customer where (
                            (p.FirstName.Contains(FullName) || p.LastName.Contains(FullName) || (p.FirstName.Contains(FirstName) && p.LastName.Contains(LastName))) &&
                            (p.City == Place || p.Country == Place) &&
                            (p.Email == Email)
                        ) select p
                    ).ToList();
                }
                List<CustomerViewModel> ListView = new List<CustomerViewModel>();
                
                foreach (var customer in ListCustomer)
                {
                    CustomerViewModel Model = new CustomerViewModel();
                    Model.Id = customer.Id;
                    Model.FirstName = customer.FirstName;
                    Model.LastName = customer.LastName;
                    Model.City = customer.City;
                    Model.Country = customer.Country;
                    Model.Phone = customer.Phone;
                    Model.Email = customer.Email;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        // SELECT * FROM Customer WHERE Id = Id
        public CustomerViewModel GetCustomerById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer customer = db.Customer.Find(id);

                CustomerViewModel Model = new CustomerViewModel();
                Model.Id = customer.Id;
                Model.FirstName = customer.FirstName;
                Model.LastName = customer.LastName;
                Model.City = customer.City;
                Model.Country = customer.Country;
                Model.Phone = customer.Phone;
                Model.Email = customer.Email;

                return Model;
            }
        }

        // INSERT INTO Customer VALUE customer
        public void CreateNewCustomer(CustomerViewModel customer)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer Model = new Customer();
                Model.FirstName = customer.FirstName;
                Model.LastName = customer.LastName;
                Model.City = customer.City;
                Model.Country = customer.Country;
                Model.Phone = customer.Phone;
                Model.Email = customer.Email;

                db.Customer.Add(Model);
                db.SaveChanges();
            }
        }

        // UPDATE
        public void UpdateCustomer(CustomerViewModel customer)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer Model = new Customer();
                Model.Id = customer.Id;
                Model.FirstName = customer.FirstName;
                Model.LastName = customer.LastName;
                Model.City = customer.City;
                Model.Country = customer.Country;
                Model.Phone = customer.Phone;
                Model.Email = customer.Email;

                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE
        public void DeleteCustomer(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer customer = db.Customer.Find(id);
                db.Customer.Remove(customer);
                db.SaveChanges();
            }
        }

        public bool CekNama(string FirstName, string LastName)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = (from p in db.Customer where (p.FirstName == FirstName && p.LastName == LastName) select p).SingleOrDefault();
                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }

        public bool CekEmail(string Email)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Email = Email ?? " ";
                var result = (from p in db.Customer where (p.Email == Email) select p).ToList();
                return !IsNullOrEmpty(result);
            }
        }

        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            switch (enumerable)
            {
                case null:
                    return true;
                case ICollection<T> collection:
                    return collection.Count < 1;
            }
            return !enumerable.Any();
        }
    }
}
