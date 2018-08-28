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
    public class SupplierRepository
    {
        // SELECT *
        public List<SupplierViewModel> GetAllSupplier()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var ListSupplier = db.Supplier.ToList();
                List<SupplierViewModel> ListView = new List<SupplierViewModel>();
                foreach (var supplier in ListSupplier)
                {
                    SupplierViewModel Model = new SupplierViewModel();
                    Model.Id = supplier.Id;
                    Model.CompanyName = supplier.CompanyName;
                    Model.ContactName = supplier.ContactName;
                    Model.ContactTitle = supplier.ContactTitle;
                    Model.City = supplier.City;
                    Model.Country = supplier.Country;
                    Model.Phone = supplier.Phone;
                    Model.Fax = supplier.Fax;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        // SELECT *
        public SupplierViewModel GetSupplierById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supplier = db.Supplier.Find(id);

                SupplierViewModel Model = new SupplierViewModel();
                Model.Id = supplier.Id;
                Model.CompanyName = supplier.CompanyName;
                Model.ContactName = supplier.ContactName;
                Model.ContactTitle = supplier.ContactTitle;
                Model.City = supplier.City;
                Model.Country = supplier.Country;
                Model.Phone = supplier.Phone;
                Model.Fax = supplier.Fax;

                return Model;
            }
        }

        public List<ProductViewModel> GetProduct(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var ListProduct = db.Product.Where(p => p.SupplierId.Equals(id)).ToList();
                List<ProductViewModel> ListView = new List<ProductViewModel>();
                foreach (var product in ListProduct)
                {
                    ProductViewModel Model = new ProductViewModel();
                    Model.Id = product.Id;
                    Model.ProductName = product.ProductName;
                    Model.SupplierId = product.SupplierId;
                    Model.UnitPrice = product.UnitPrice;
                    Model.Package = product.Package;
                    Model.IsDiscontinued = product.IsDiscontinued;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        // INSERT INTO
        public void CreateNewSupplier(SupplierViewModel supplier)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier Model = new Supplier();
                Model.Id = supplier.Id;
                Model.CompanyName = supplier.CompanyName;
                Model.ContactName = supplier.ContactName;
                Model.ContactTitle = supplier.ContactTitle;
                Model.City = supplier.City;
                Model.Country = supplier.Country;
                Model.Phone = supplier.Phone;
                Model.Fax = supplier.Fax;

                db.Supplier.Add(Model);
                db.SaveChanges();
            }
        }

        // UPDATE
        public void UpdateSupplier(SupplierViewModel supplier)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier Model = new Supplier();
                Model.Id = supplier.Id;
                Model.CompanyName = supplier.CompanyName;
                Model.ContactName = supplier.ContactName;
                Model.ContactTitle = supplier.ContactTitle;
                Model.City = supplier.City;
                Model.Country = supplier.Country;
                Model.Phone = supplier.Phone;
                Model.Fax = supplier.Fax;

                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE
        public void DeleteSupplier(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supplier = db.Supplier.Find(id);
                db.Supplier.Remove(supplier);
                db.SaveChanges();
            }
        }

    }
}
