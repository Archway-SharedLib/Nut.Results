using Nut.Results;

namespace ResultSample.Refactoring_3;

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
