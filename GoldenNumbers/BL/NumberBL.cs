using GoldenNumbers.DAL;
using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.BL
{
    public class NumberBL
    {

        #region Number
        public List<Number> getNumberList()
        {
            return new NumberDAL().getNumbersList();
        }

        public Number getNumberById(int _id)
        {
            return new NumberDAL().getNumberById(_id);
        }

        public bool AddNumber(Number _Number)
        {
           if (_Number.Number1 == ""  ||  _Number.Company == "" || _Number.Type == "")
               return false;
           
            return new NumberDAL().AddNumber(_Number);
        }

    

        public bool UpdateNumber(Number _Number)
        {
            if (_Number.Number1 == "" || _Number.Company == "" || _Number.Type == "" || _Number.Status == "")
                return false;

            return new NumberDAL().UpdateNumber(_Number);
        }

        public void DeleteNumber(int _id)
        {
            new NumberDAL().DeleteNumber(_id);
        }

        internal IEnumerable<object> getUserList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}