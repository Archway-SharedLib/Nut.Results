using Nut.Results;

namespace ResultSample.Refactoring_2;

public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId);
        if (result.IsOk)
        {
            var user = result.Get();
            user.Name = name;
            var saveResult = repository.Save(user);
            if (saveResult.IsOk)
            {
                return saveResult.Get();
            }
        }
        // !! UserServiceを利用する側でも null が何を表すのか分からなくなってしまう。
        return null;
    }
}
