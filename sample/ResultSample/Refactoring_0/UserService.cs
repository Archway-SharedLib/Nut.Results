namespace ResultSample.Refactoring_0;

public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var user = repository.GetUserById(userId); //　nullが返ってくる
                                                   // !! null が値がないのかエラーだったのかが分からない
        user.Name = name; // !! NullReferenceExceptionが発生する
        return repository.Save(user);
    }
}
