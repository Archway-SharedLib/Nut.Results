using Nut.Results;

namespace ResultSample.Refactoring_5;

public class UserService
{
    public Result<string> UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        return repository.GetUserById(userId).Map(user =>
        {
            user.Name = name;
            return user;
        }).FlatMap(repository.Save);
    }
}
