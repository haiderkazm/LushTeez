using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.DAL
{
    public class SubscriberDAL
    {
        GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();
        #region Number
       
        public List<Subscriber> getSubscriberList()
        {
            List<Subscriber> Subscribers;
            GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();

            Subscribers = db.Subscribers.Where(x => x.IsActive == 1).ToList();


            return Subscribers;
        }

        public Subscriber getSubscriberById(int _Id)
        {

            GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();


            Subscriber Subscribers = db.Subscribers.Where(x => x.IsActive == 1).FirstOrDefault(x => x.Id == _Id);


            return Subscribers;
        }

        public bool AddSubscriber(Subscriber _Subscriber)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Subscribers.Add(_Subscriber);
                db.SaveChanges();
            }
            return true;
        }

        public bool UpdateSubscriber(Subscriber _Subscriber)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Entry(_Subscriber).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }

        public void DeleteSubscriber(int _id)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Subscribers.Remove(db.Subscribers.FirstOrDefault(x => x.Id == _id));
                db.SaveChanges();
            }
        }
        #endregion
    }
}