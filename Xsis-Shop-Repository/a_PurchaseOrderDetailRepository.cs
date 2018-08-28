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
    public class a_PurchaseOrderDetailRepository
    {
        // SELECT *
        public List<a_PurchaseOrderDetailViewModel> GetAlla_PurchaseOrderDetail()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var a_PurchaseOrderDetails = db.a_PurchaseOrderDetail.Include(p => p.a_Product).ToList();
                List<a_PurchaseOrderDetailViewModel> ListView = new List<a_PurchaseOrderDetailViewModel>();
                foreach (var a_PurchaseOrderDetail in a_PurchaseOrderDetails)
                {
                    a_PurchaseOrderDetailViewModel Model = new a_PurchaseOrderDetailViewModel();
                    Model.ID = a_PurchaseOrderDetail.ID;
                    Model.PurchaseOrderID = a_PurchaseOrderDetail.PurchaseOrderID;
                    Model.ProductID = a_PurchaseOrderDetail.ProductID;
                    //Model.Quantity = a_PurchaseOrderDetail.Quantity;
                    //Model.UnitPrice = a_PurchaseOrderDetail.UnitPrice;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        // SELECT *
        public a_PurchaseOrderDetailViewModel Geta_PurchaseOrderDetailById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_PurchaseOrderDetail a_PurchaseOrderDetail = db.a_PurchaseOrderDetail.Find(id);

                a_PurchaseOrderDetailViewModel Model = new a_PurchaseOrderDetailViewModel();
                Model.ID = a_PurchaseOrderDetail.ID;
                Model.PurchaseOrderID = a_PurchaseOrderDetail.PurchaseOrderID;
                Model.ProductID = a_PurchaseOrderDetail.ProductID;
                //Model.Quantity = a_PurchaseOrderDetail.Quantity;
                //Model.UnitPrice = a_PurchaseOrderDetail.UnitPrice;

                return Model;
            }
        }

        // INSERT INTO
        public void CreateNewa_PurchaseOrderDetail(a_PurchaseOrderDetailViewModel a_PurchaseOrderDetail)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_PurchaseOrderDetail Model = new a_PurchaseOrderDetail();
                Model.ID = a_PurchaseOrderDetail.ID;
                Model.PurchaseOrderID = a_PurchaseOrderDetail.PurchaseOrderID;
                Model.ProductID = a_PurchaseOrderDetail.ProductID;
                Model.Quantity = a_PurchaseOrderDetail.Quantity;
                Model.UnitPrice = a_PurchaseOrderDetail.UnitPrice;

                db.a_PurchaseOrderDetail.Add(Model);
                db.SaveChanges();
            }
        }

        // UPDATE
        public void Updatea_PurchaseOrderDetail(a_PurchaseOrderDetailViewModel a_PurchaseOrderDetail)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_PurchaseOrderDetail Model = new a_PurchaseOrderDetail();
                Model.ID = a_PurchaseOrderDetail.ID;
                Model.PurchaseOrderID = a_PurchaseOrderDetail.PurchaseOrderID;
                Model.ProductID = a_PurchaseOrderDetail.ProductID;
                Model.Quantity = a_PurchaseOrderDetail.Quantity;
                Model.UnitPrice = a_PurchaseOrderDetail.UnitPrice;

                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE
        public void Deletea_PurchaseOrderDetail(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_PurchaseOrderDetail a_PurchaseOrderDetail = db.a_PurchaseOrderDetail.Find(id);
                db.a_PurchaseOrderDetail.Remove(a_PurchaseOrderDetail);
                db.SaveChanges();
            }
        }

        public List<a_PurchaseOrder> Geta_PurchaseOrderList()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var List = db.a_PurchaseOrder.ToList();
                return List;
            }
        }

        public List<a_Product> Geta_ProductList()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var List = db.a_Product.ToList();
                return List;
            }
        }
    }
}
