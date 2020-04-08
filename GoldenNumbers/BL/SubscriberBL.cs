using GoldenNumbers.DAL;
using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.BL
{
    public class SubscriberBL
    {
        #region Subscriber
        public List<Subscriber> getSubscriberList()
        {
            return new SubscriberDAL().getSubscriberList();
        }

        public Subscriber getSubscriberById(int _id)
        {
            return new SubscriberDAL().getSubscriberById(_id);
        }

        public bool AddSubscriber(Subscriber _Subscriber)
        {
           if (_Subscriber.Email == "")
             return false;
            return new SubscriberDAL().AddSubscriber(_Subscriber);
        }

        public bool UpdateSubscriber(Subscriber _Subscriber)
        {
           // if (_Subscriber.Number1 == "" || _Number.Company == "" || _Number.Type == "" || _Number.Status == "")
               // return false;

            return new SubscriberDAL().UpdateSubscriber(_Subscriber);
        }

        public void DeleteSubscriber(int _id)
        {
            new SubscriberDAL().DeleteSubscriber(_id);
        }

        internal IEnumerable<object> getUserList()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}