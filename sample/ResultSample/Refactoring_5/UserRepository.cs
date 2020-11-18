using Nut.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultSample.Refactoring_5
{
    public class UserRepository
    {
        public Result<User> GetUserById(string userId)
        {
            return Result.Error<User>(new DataNotFoundError());
        }

        public Result<string> Save(User user)
        {
            // 自動的に Result.Ok に変換される
            return user.Id;
        }
    }
}
