using Nut.Results;

namespace ResultSample.Refactoring_4;

public class UserService
{
    public Result<string> UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId);
        return result.Map(user => // !! result 変数などが冗長
        {
            //処理が成功している場合だけ実行される
            user.Name = name;
            return user;
        }).FlatMap(user => // !! ラムダ式が冗長
        {
            //処理が成功している場合だけ実行される
            return repository.Save(user);
        });
        // ↑処理が失敗していた場合は、そのまま戻り値になる
    }
}
