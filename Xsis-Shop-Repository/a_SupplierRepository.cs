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
    public class a_SupplierRepository
    {
        // SELECT * FROM a_Supplier
        public List<a_SupplierViewModel> GetAlla_Supplier()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var Lista_Supplier = db.a_Supplier.ToList();
                List<a_SupplierViewModel> ListView = new List<a_SupplierViewModel>();
                foreach (var a_Supplier in Lista_Supplier)
                {
                    a_SupplierViewModel Model = new a_SupplierViewModel();
                    Model.ID = a_Supplier.ID;
                    Model.Code = a_Supplier.Code;
                    Model.Name= a_Supplier.Name;
                    Model.City = a_Supplier.City;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        public List<a_SupplierViewModel> GetAlla_Supplier(string Name, string City)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<Xsis_Shop_Models.a_Supplier> Lista_Supplier;

                bool NullName = string.IsNullOrWhiteSpace(Name);
                Name = NullName ? " " : Name.ToLower();

                bool NullCity = string.IsNullOrWhiteSpace(City);
                City = NullCity ? " " : City.ToLower();

                if (NullName && NullCity) // NULL
                    Lista_Supplier = db.a_Supplier.ToList();

                else if (NullCity)  // Hanya Name
                    Lista_Supplier= db.a_Supplier.Where(p => (
                        p.Name == Name
                    )).ToList();

                else if (NullName)  // Hanya City
                    Lista_Supplier = db.a_Supplier.Where(p => (
                        p.City == City
                    )).ToList();

                else // Semua Terisi
                {
                    Lista_Supplier = (
                        from p in db.a_Supplier where ((p.Name == Name) && (p.City == City))
                        select p
                    ).ToList();
                }
                List<a_SupplierViewModel> ListView = new List<a_SupplierViewModel>();

                foreach (var a_Supplier in Lista_Supplier)
                {
                    a_SupplierViewModel Model = new a_SupplierViewModel();
                    Model.ID = a_Supplier.ID;
                    Model.Code = a_Supplier.Code;
                    Model.Name = a_Supplier.Name;
                    Model.City = a_Supplier.City;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        // SELECT * FROM a_Supplier WHERE Id = Id
        public a_SupplierViewModel Geta_SupplierById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_Supplier a_Supplier = db.a_Supplier.Find(id);

                a_SupplierViewModel Model = new a_SupplierViewModel();
                Model.ID = a_Supplier.ID;
                Model.Code= a_Supplier.Code;
                Model.Name = a_Supplier.Name;
                Model.City = a_Supplier.City;

                return Model;
            }
        }

        // INSERT INTO a_Supplier VALUE a_Supplier
        public void CreateNewa_Supplier(a_SupplierViewModel a_Supplier)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Xsis_Shop_Models.a_Supplier Model = new Xsis_Shop_Models.a_Supplier();
                Model.Code = a_Supplier.Code;
                Model.Name = a_Supplier.Name;
                Model.City = a_Supplier.City;

                db.a_Supplier.Add(Model);
                db.SaveChanges();
            }
        }

        // UPDATE
        public void Updatea_Supplier(a_SupplierViewModel a_Supplier)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Xsis_Shop_Models.a_Supplier Model = new Xsis_Shop_Models.a_Supplier();
                Model.ID = a_Supplier.ID;
                Model.Code= a_Supplier.Code;
                Model.Name = a_Supplier.Name;
                Model.City = a_Supplier.City;

                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE
        public void Deletea_Supplier(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_Supplier a_Supplier = db.a_Supplier.Find(id);
                db.a_Supplier.Remove(a_Supplier);
                db.SaveChanges();
            }
        }
    }
}
