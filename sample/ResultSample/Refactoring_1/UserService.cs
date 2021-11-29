using Nut.Results;

namespace ResultSample.Refactoring_1;

public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId); //　resultが返ってくる
        var user = result.Get(); // !! 失敗だった場合に InvalidOperationExceptionが発生する
        user.Name = name;
        var saveResult = repository.Save(user);
        return saveResult.Get(); // !! 失敗だった場合に InvalidOperationExceptionが発生する
    }
}
