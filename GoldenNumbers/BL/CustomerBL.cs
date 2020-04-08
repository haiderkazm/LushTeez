using GoldenNumbers.DAL;
using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.BL
{
    public class CustomerBL
    {
        #region Customer
        public List<Customer> getCustomerList()
        {
            return new CustomerDAL().getCustomersList();
        }

        public Customer getCustomerById(int _id)
        {
            return new CustomerDAL().getCustomerById(_id);
        }

        public bool AddDesire(Customer _Customer)
        {
             if (_Customer.Name == "" || _Customer.Phone == "" ||_Customer.DesiredNumber=="")
                 return false;
            return new CustomerDAL().AddCustomer(_Customer);
        }
        public bool AddSaleNumber(Customer _Customer)
        {
            if (_Customer.Name == "" || _Customer.Phone == "" || _Customer.SellingNumber == "")
                return false;
            return new CustomerDAL().AddCustomer(_Customer);
        }
        public bool UpdateCustomer(Customer _Customer)
        {
           // if (_Number.Number1 == "" || _Number.Company == "" || _Number.Type == "" || _Number.Status == "")
             //   return false;

            return new CustomerDAL().UpdateCustomer(_Customer);
        }

        public void DeleteCustomer(int _id)
        {
            new CustomerDAL().DeleteCustomer(_id);
        }

        internal IEnumerable<object> getUserList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}