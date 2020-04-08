using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.DAL
{
    public class CustomerDAL
    {
        GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();
        #region Customer
        public List<Customer> getCustomersList()
        {
            List<Customer> Customers;
            GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();

            Customers = db.Customers.Where(x => x.IsActive == 1).ToList();


            return Customers;
        }

        public Customer getCustomerById(int _Id)
        {

            GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();


            Customer Customers = db.Customers.Where(x => x.IsActive == 1).FirstOrDefault(x => x.Id == _Id);


            return Customers;
        }

        public bool AddCustomer(Customer _Customer)
        {
           
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Customers.Add(_Customer);
                db.SaveChanges();
            }
            return true;
        }

        public bool UpdateCustomer(Customer _Customer)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Entry(_Customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }

        public void DeleteCustomer(int _id)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Customers.Remove(db.Customers.FirstOrDefault(x => x.Id == _id));
                db.SaveChanges();
            }
        }
        #endregion
    }
}