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
    public class a_PurchaseOrderRepository
    {
        // SELECT *
        public List<a_PurchaseOrderViewModel> GetAlla_PurchaseOrder()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var a_PurchaseOrders = db.a_PurchaseOrder.Include(p => p.a_Supplier).ToList();
                List<a_PurchaseOrderViewModel> ListView = new List<a_PurchaseOrderViewModel>();
                foreach (var a_PurchaseOrder in a_PurchaseOrders)
                {
                    a_PurchaseOrderViewModel Model = new a_PurchaseOrderViewModel();
                    Model.ID = a_PurchaseOrder.ID;
                    Model.Code = a_PurchaseOrder.Code;
                    //Model.PurchaseDate = a_PurchaseOrder.PurchaseDate;
                    Model.SupplierID = a_PurchaseOrder.SupplierID;
                    Model.Remarks = a_PurchaseOrder.Remarks;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        // SELECT *
        public a_PurchaseOrderViewModel Geta_PurchaseOrderById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_PurchaseOrder a_PurchaseOrder = db.a_PurchaseOrder.Find(id);

                a_PurchaseOrderViewModel Model = new a_PurchaseOrderViewModel();
                Model.ID = a_PurchaseOrder.ID;
                Model.Code = a_PurchaseOrder.Code;
                //Model.PurchaseDate = a_PurchaseOrder.PurchaseDate;
                Model.SupplierID = a_PurchaseOrder.SupplierID;
                Model.Remarks = a_PurchaseOrder.Remarks;

                return Model;
            }
        }

        // INSERT INTO
        public void CreateNewa_PurchaseOrder(a_PurchaseOrderViewModel a_PurchaseOrder)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_PurchaseOrder Model = new a_PurchaseOrder();
                Model.ID = a_PurchaseOrder.ID;
                Model.Code = a_PurchaseOrder.Code;
                Model.PurchaseDate = a_PurchaseOrder.PurchaseDate;
                Model.SupplierID = a_PurchaseOrder.SupplierID;
                Model.Remarks = a_PurchaseOrder.Remarks;

                db.a_PurchaseOrder.Add(Model);
                db.SaveChanges();
            }
        }

        // UPDATE
        public void Updatea_PurchaseOrder(a_PurchaseOrderViewModel a_PurchaseOrder)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_PurchaseOrder Model = new a_PurchaseOrder();
                Model.ID = a_PurchaseOrder.ID;
                Model.Code = a_PurchaseOrder.Code;
                Model.PurchaseDate = a_PurchaseOrder.PurchaseDate;
                Model.SupplierID = a_PurchaseOrder.SupplierID;
                Model.Remarks = a_PurchaseOrder.Remarks;

                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE
        public void Deletea_PurchaseOrder (int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                a_PurchaseOrder a_PurchaseOrder = db.a_PurchaseOrder.Find(id);
                db.a_PurchaseOrder.Remove(a_PurchaseOrder);
                db.SaveChanges();
            }
        }

        public List<a_Supplier> Geta_SupplierList()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var List = db.a_Supplier.ToList();
                return List;
            }
        }
    }
}
