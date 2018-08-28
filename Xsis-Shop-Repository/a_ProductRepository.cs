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
    public class a_ProductRepository
    {
        // SELECT * FROM a_Product
        public List<Xsis_Shop_ViewModels.a_ProductViewModel> GetAlla_Product()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var Lista_Product = db.a_Product.ToList();
                List<Xsis_Shop_ViewModels.a_ProductViewModel> ListView = new List<Xsis_Shop_ViewModels.a_ProductViewModel>();
                foreach (var a_Product in Lista_Product)
                {
                    Xsis_Shop_ViewModels.a_ProductViewModel Model = new Xsis_Shop_ViewModels.a_ProductViewModel();
                    Model.ID = a_Product.ID;
                    Model.Code = a_Product.Code;
                    Model.Name = a_Product.Name;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        public List<Xsis_Shop_ViewModels.a_ProductViewModel> GetAlla_Product(string Name)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<Xsis_Shop_Models.a_Product> Lista_Product;

                bool NullName = string.IsNullOrWhiteSpace(Name);
                Name = NullName ? " " : Name.ToLower();

                if (NullName) // NULL
                    Lista_Product = db.a_Product.ToList();

                else if (NullName)  // Hanya Name
                    Lista_Product = db.a_Product.Where(p => (
                         p.Name == Name
                     )).ToList();

                else // Semua Terisi
                {
                    Lista_Product = (
                        from p in db.a_Product
                        where ((p.Name == Name))
                        select p
                    ).ToList();
                }
                List<Xsis_Shop_ViewModels.a_ProductViewModel> ListView = new List<Xsis_Shop_ViewModels.a_ProductViewModel>();

                foreach (var a_Product in Lista_Product)
                {
                    Xsis_Shop_ViewModels.a_ProductViewModel Model = new Xsis_Shop_ViewModels.a_ProductViewModel();
                    Model.ID = a_Product.ID;
                    Model.Code = a_Product.Code;
                    Model.Name = a_Product.Name;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        // SELECT * FROM a_Product WHERE Id = Id
        public a_ProductViewModel Geta_ProductById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_Product a_Product = db.a_Product.Find(id);

                Xsis_Shop_ViewModels.a_ProductViewModel Model = new Xsis_Shop_ViewModels.a_ProductViewModel();
                Model.ID = a_Product.ID;
                Model.Code = a_Product.Code;
                Model.Name = a_Product.Name;

                return Model;
            }
        }

        // INSERT INTO a_Product VALUE a_Product
        public void CreateNewa_Product(Xsis_Shop_ViewModels.a_ProductViewModel a_Product)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Xsis_Shop_Models.a_Product Model = new Xsis_Shop_Models.a_Product();
                Model.Code = a_Product.Code;
                Model.Name = a_Product.Name;

                db.a_Product.Add(Model);
                db.SaveChanges();
            }
        }

        // UPDATE
        public void Updatea_Product(Xsis_Shop_ViewModels.a_ProductViewModel a_Product)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Xsis_Shop_Models.a_Product Model = new Xsis_Shop_Models.a_Product();
                Model.ID = a_Product.ID;
                Model.Code = a_Product.Code;
                Model.Name = a_Product.Name;

                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE
        public void Deletea_Product(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_Product a_Product = db.a_Product.Find(id);
                db.a_Product.Remove(a_Product);
                db.SaveChanges();
            }
        }
    }
}
