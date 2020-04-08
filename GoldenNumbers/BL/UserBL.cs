using GoldenNumbers.DAL;
using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.BL
{
    public class UserBL
    {
        #region User
        public List<User> getUsersList()
        {
            return new UserDAL().getUsersList();
        }

        public User getUserById(int _id)
        {
            return new UserDAL().getUserById(_id);
        }

        public bool AddUser(User _User)
        {
           // if (_User.Number1 == "" || _User.Company == "" || _User.Type == "" || _User.Status == "")
               // return false;
            return new UserDAL().AddUser(_User);
        }

        public bool UpdateUser(User _User)
        {
           if (_User.Name == "" || _User.Email == "" || _User.Password == "" )
                return false;

            return new UserDAL().UpdateUser(_User);
        }

        public void DeleteUser(int _id)
        {
            new UserDAL().DeleteUser(_id);
        }

        internal IEnumerable<object> getUserList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}