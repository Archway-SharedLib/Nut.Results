using Nut.Results;

namespace ResultSample.Refactoring_3;

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
        // !! 値を詰めなおすだけの作業になっているため、冗長。
        return Result.Error<string>(result.GetError());
    }
}
