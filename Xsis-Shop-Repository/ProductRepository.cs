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
    public class ProductRepository
    {
        // SELECT *
        public List<ProductViewModel> GetAllProduct()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var products = db.Product.Include(p => p.Supplier).ToList();
                //var list = db.Product.ToList();
                List<ProductViewModel> ListView = new List<ProductViewModel>();
                foreach (var product in products)
                {
                    ProductViewModel Model = new ProductViewModel();
                    Model.Id = product.Id;
                    Model.ProductName = product.ProductName;
                    Model.SupplierId = product.SupplierId;
                    Model.UnitPrice = product.UnitPrice;
                    Model.Package = product.Package;
                    Model.IsDiscontinued = product.IsDiscontinued;
                    Model.SupplierName = product.Supplier.CompanyName;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        // SELECT *
        public ProductViewModel GetProductById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product product = db.Product.Find(id);

                ProductViewModel Model = new ProductViewModel();
                Model.Id = product.Id;
                Model.ProductName = product.ProductName;
                Model.SupplierId = product.SupplierId;
                Model.UnitPrice = product.UnitPrice;
                Model.Package = product.Package;
                Model.IsDiscontinued = product.IsDiscontinued;
                Model.SupplierName = product.Supplier.CompanyName;

                return Model;
            }
        }

        // INSERT INTO
        public void CreateNewProduct(ProductViewModel product)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product Model = new Product();
                Model.Id = product.Id;
                Model.ProductName = product.ProductName;
                Model.SupplierId = product.SupplierId;
                Model.UnitPrice = product.UnitPrice;
                Model.Package = product.Package;
                Model.IsDiscontinued = product.IsDiscontinued;

                db.Product.Add(Model);
                db.SaveChanges();
            }
        }

        // UPDATE
        public void UpdateProduct(ProductViewModel product)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product Model = new Product();
                Model.Id = product.Id;
                Model.ProductName = product.ProductName;
                Model.SupplierId = product.SupplierId;
                Model.UnitPrice = product.UnitPrice;
                Model.Package = product.Package;
                Model.IsDiscontinued = product.IsDiscontinued;

                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE
        public void DeleteProduct(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product product = db.Product.Find(id);
                db.Product.Remove(product);
                db.SaveChanges();
            }
        }

        public List<Supplier> GetSupplierList()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var List = db.Supplier.ToList();
                return List;
            }
        }
    }
}
