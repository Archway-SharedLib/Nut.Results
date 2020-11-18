using Nut.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultSample.Refactoring_3
{
    public class UserService
    {
        public Result<string> UpdateUserName(string userId, string name)
        {
            var repository = new UserRepository();
            var result = repository.GetUserById(userId);
            if (result.IsOk) // !! if文がネストしていて冗長
            {
                var user = result.Get();
                user.Name = name;
                var saveResult = repository.Save(user);
                if (saveResult.IsOk)
                {
                    return saveResult.Get();
                }
            }
            // !! Repositoryから既に同様の内容が返っているため、冗長。
            return Result.Error<string>(new DataNotFoundError());
        }
    }
}
