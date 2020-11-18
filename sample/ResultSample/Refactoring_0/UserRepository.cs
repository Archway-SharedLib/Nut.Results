using System;
using System.Collections.Generic;
using System.Text;

namespace ResultSample.Refactoring_0
{
    public class UserRepository
    {
        public User GetUserById(string userId)
        {
            return null;
        }

        public string Save(User user)
        {
            return user.Id;
        }
    }
}
