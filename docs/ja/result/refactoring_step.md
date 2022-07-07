# リファクタリングからみるNut.Resultsの使い方

ここでは、一般的によく記載されるコードから、Nut.Resultsを利用することでどのように安全になるかをリファクタリングを通して説明します。
リファクタリングのコードは`sample`ディレクトリに配置されているため、参照してください。

## 0. 初期の状態

リファクタリングをするサンプルのストーリーは、よくある次のようなものになります。

1. サービスは指定されたIDでリポジトリからデータを取得する
2. 取得したデータを引数で指定された値を設定して更新
3. 更新した値をリポジトリを使って保存
4. 保存の結果返ってきた値を呼び出し元に返す

この処理のコードは次になります。

```cs
public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var user = repository.GetUserById(userId);
        user.Name = name;
        return repository.Save(user);
    }
}
```

リポジトリの実装は次になります。

```cs
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
```

このコードには次のような問題があります。

- リポジトリから返されるnullチェックをしていないので例外が発生する。
- リポジトリから返されるnullという値がデータが無い(が正しい動きなのか、失敗な)のか、何らかのエラーが発生したのかなど、なにを意味するのか分からない。

この問題に対応するためにリポジトリからは`Result{T}`を返すように変更します。

## 1. Resultによる結果の明示

リポジトリからは`Result{T}`を返すように変更しました。また、データがない旨も`DatanotFoundException`型を返すことで明示できています。

```cs
public class UserRepository
{
    public Result<User> GetUserById(string userId)
    {
        return Result.Error<User>(new DatanotFoundException());
    }

    public Result<string> Save(User user)
    {
        // 自動的に Result.Ok に変換される
        return user.Id;
    }
}
```

これを受けてサービス側を次のように修正します。

```cs
public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId); //　resultが返ってくる
        var user = result.Get();
        user.Name = name;
        var saveResult = repository.Save(user);
        return saveResult.Get();
    }
}
```

しかし、これにも次のような問題があります。

- `Get`メソッドは失敗だった場合に例外が発生する。

`Get`メソッドは状態が失敗だった場合は`InvalidOperationException`を発行します。これは状態のチェックを必ずしなければならない実装の不具合であることを明示するための仕様です。

この問題に対応するために結果が成功かどうかをチェックするように変更します。

## 2. Resultの状態をチェックする

`IsOk`プロパティを使って`Get`の前にチェックするように変更しました。

```cs
public class UserService
{
    public string UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId);
        if(result.IsOk)
        {
            var user = result.Get();
            user.Name = name;
            var saveResult = repository.Save(user);
            if(saveResult.IsOk)
            {
                return saveResult.Get();
            }
        }
        return null;
    }
}
```

この変更で成功した場合、失敗した場合の確実な処理が実行されるようになりましたが、失敗の場合に`null`を返してしまっており、次のような問題を生んでしまいました。

- このメソッドの呼び出し元が`null`を受け取ってしまい、`null`の意味が理解できない。
- リポジトリからデータがないということが明示されていたのに、消してしまっている。

この問題に対応するためにサービスでも`Result{T}`を返すように変更します。

## 3. サービスからもResultを返す

サービスのメソッドの戻り値も`Result{T}`に変更しました。

```cs
public class UserService
{
    public Result<string> UpdateUserName(string userId, string name)
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
        return Result.Error<string>(result.GetError());
    }
}
```

これで失敗の場合の原因も呼び出し元に明示できるようになり、呼び出し元も含めて確実な処理を行ってもらえるようになりました。しかし、このコードには次のような問題があり可読性が下がってしまっています。

- if文がネストしてしまっており冗長になっている。
- エラーを詰めなおしているだけで冗長になっている。

この問題に対応するために`Result`および`Result{T}`に提供されている拡張メソッドを利用するように変更します。

## 4. 拡張メソッドを利用して直線的な処理に変更する

`Map`や`FlatMap`を利用してコードの記述が直線的になるように変更しました。成功だった場合などの条件分岐を取り除いています。

```cs
public class UserService
{
    public Result<string> UpdateUserName(string userId, string name)
    {
        var repository = new UserRepository();
        var result = repository.GetUserById(userId);
        return result.Map(user =>
        {
            user.Name = name;
            return user;
        }).FlatMap(user =>
        {
            return repository.Save(user);
        });
    }
}
```

ここまででほぼリファクタリングは完了しています。最後に不要な変数の削除などを行います。

## 5. 不要な変数などの削除

次の変更を行いました。

- `result`変数が不要で冗長なため削除
- `FlatMap`のラムダ式は直接`Save`メソッドを指定できるため削除

```cs
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
```

これでリファクタリングは終了です。途中、チェック処理などを挿むことでコードが長くなってしまったり、ネストが発生して可読性が下がってしまう箇所もありました。しかし、拡張メソッドを活用することで最終的には直線的でシンプルなコードになりました。
