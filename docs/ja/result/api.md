# Nut.Resultsの使い方

## 成功の結果を表すResultを作成する(Okメソッド)

成功の結果を表す`Result`を作成するには`Result`型の静的メソッドである`Ok()`を利用します。

```cs
Result okResult = Result.Ok();
```

上記は単純に成功を表しています。値を含めたい場合は`Result{T}`を利用します。作成するには`Result`型の静的メソッドである`Ok{T}()`を利用します。

```cs
Result<string> okResult = Result.Ok("This is ok result!");
Result<int> ageResult = Result.Ok(18);
```

`Result{T}`の値には`null`を渡すことはできません。`null`になる可能性がある場合は、処理の内容を再検討し必ず何らかの値が返るようにするか、`Result`型を返すようにしてください。

## 失敗の結果を表すResultを作成する(Errorメソッド)

失敗の結果を表す`Result`を作成するには`Result`型の静的メソッドである`Error(Exception)`を利用します。

```cs
Result dataNotFoundResult = Result.Error(new DataNotFoundException());
```

`IError`インターフェイスは失敗の詳細を表します。エラーの詳細がメッセージだけなどの場合は`Error(string)`メソッドを利用します。
この場合は、インスタンスには`Exception`型が利用されます。

```cs
Result errorResult = Result.Error("This method is fail.");
```

## 処理結果に応じたResultを作成する(Tryメソッド)

処理の結果に応じて`Result`を作成するには、`Result`型の静的メソッドである`Try(Action)`メソッドを利用します。

```cs
Result ok = Result.Try(() => DoSomeMethod());
```

処理中に例外が発生した場合は、発生した例外を保持した失敗の`Result`が返ります。

```cs
var error = Result.Try(() => throw new Exception());
```

## 結果が成功だったか失敗だったかを確認する(IsOk/IsErrorメソッド)

`Result`と`Result{T}`の両方ともに成功かどうかを確認する`IsOk`メソッドと、失敗かどうかを確認する`IsError`メソッドが用意されています。

```cs
var taskResult = DoTask();
if (taskResult.IsOk())
{
  var task2Result = DoTask2();
  if (task2Result.IsError())
  {

  }
}
```

これらのメソッドは処理結果を単純に確認できますが、複雑な条件を生んでしまう可能性があるため、後述する`Tap`/`Map`/`FlatMap` メソッドを利用して処理を組み立てることを推奨します。

## 結果の値を取得する(Get/GetErrorメソッド)

`Result{T}`には成功の値を取得する`Get`メソッドがあります。失敗の場合の詳細を取得するには`GetError`メソッドを利用します。
`GetError`メソッドは`Result`と`Result{T}`の両方ともに用意されています。

```cs
var taskResult = DoTask();
if (taskResult.IsOk())
{
  var value = taskResult.Get();
}

var task2Result = DoTask2();
if (task2Result.IsError())
{
  var error = task2Result.GetError();
}
```

`Get`メソッドは結果が失敗の場合には`InvalidOperationException`が発生します。
同様に`GetError`メソッドも結果が成功の場合は`InvalidOperationException`が発生します。
そのため、`Get`/`GetError`メソッドを利用する前には必ず`IsOk`/`IsError`メソッドで結果の確認を行ってください。

これらのメソッドは処理結果を単純に取得できますが、プログラムミスで例外が発生する可能性があるため、
後述する`GetOr`/`Tap`/`Map`/`FlatMap` メソッドを利用して処理を組み立てることを推奨します。

## 結果の値もしくは規定値を取得する(GetOr/GetErrorOrメソッド)

`Result{T}`には成功の値を取得する`GetOr`メソッドがあります。失敗の場合の詳細を取得するには`GetErrorOr`メソッドを利用します。
`GetErrorOr`メソッドは`Result`と`Result{T}`の両方ともに用意されています。
`GetOr`, `GetErrorOr`ともに、規定値を返すためのアクションを指定します。
`GetOr`メソッドは結果が成功でなかった場合にアクションが実行されます。逆に`GetErrorOr`は結果が失敗でなかった場合にアクションが実行されます。

```cs
var taskResult = DoTask();
var value = taskResult.GetOr(_ => "default value");

var task2Result = DoTask2();
var error = task2Result.GetErrorOr(_ => new WarningException());
```

## 成功の場合に処理を行う(Tapメソッド)

`Result`と`Result{T}`には成功の場合に処理を行う`Tap`メソッドがあります。
これは、結果が成功の場合にだけ指定したアクションが実行され、戻り値は同じ値が返されます。
`Result{T}`の場合は、アクションの引数に成功の値が設定されます。

```cs
var taskResult = DoSuccessTask().Tap(value => SomethingProcess(value));
```

## 失敗の場合に処理を行う(TapErrorメソッド)

`Result`と`Result{T}`には失敗の場合に処理を行う`TapError`メソッドがあります。
これは、結果が失敗の場合にだけ指定したアクションが実行され、戻り値は同じ値が返されます。
アクションの引数には失敗の値が設定されます。

```cs
var taskResult = DoSuccessTask()
  .TapError(error => SendErrorMail(error))
  .FlatMapError(error => ResolveError(error));
```

## 成功の場合に新しい成功の値を作成して返す処理を行う(Mapメソッド)

`Result{T}`には成功の場合に処理を行う`Map`メソッドがあります。
これは、結果が成功の場合にだけ指定したアクションが実行され、アクションの結果を設定した新しい`Result{T}`が返されます。
アクションの引数には成功の値が設定されます。

```cs
var somethingResult = DoSuccessTask()
  .Map(taskResult => SomethingProcess(taskResult));
```

## 失敗の場合に新しい失敗の値を作成して返す処理を行う(MapErrorメソッド)

`Result`と`Result{T}`には失敗の場合に処理を行う`MapError`メソッドがあります。
これは、結果が失敗の場合にだけ指定したアクションが実行され、アクションの戻り値である新しい`IError`を設定した新しい失敗の`Result`または`Result{T}`が返されます。
アクションの引数には失敗の値が設定されます。

```cs
var somethingResult = DoSuccessTask()
  .MapError(taskError => NormalizeError(taskError));
```

## 成功の場合に新しいResultを作成して返す処理を行う(FlatMapメソッド)

`Result`と`Result{T}`には成功の場合に処理を行う`FlatMap`メソッドがあります。
これは、結果が成功の場合にだけ指定したアクションが実行され、アクションの結果として返された新しい`Result`または`Result{T}`をそのまま返します。
`Result{T}`の場合は、アクションの引数に成功の値が設定されます。

```cs
var somethingResult = DoSuccessTask()
  .FlatMap(result =>
  {
    if(IsNotExpectedValue(result)) return Result.Error<string>(new UnexpectedValueException());
    return Result.Ok("Good value!");
  });
```

## 失敗の場合に新しいResultを作成して返す処理を行う(FlatMapErrorメソッド)

`Result`と`Result{T}`には失敗の場合に処理を行う`FlatMapError`メソッドがあります。
これは、結果が失敗の場合にだけ指定したアクションが実行され、アクションの結果として返された新しい`Result`または`Result{T}`をそのまま返します。
アクションの引数に失敗の値が設定されます。

```cs
var somethingResult = DoSuccessTask()
  .FlatMapError(error =>
  {
    if(IsRecoverableError(error)) return Result.Ok(Recovery(error));
    return Result.Error<string>(error);
  });
```

## 二つのResultを結合する(Combineメソッド)

`Result{T}`には指定された`Result{T}`の結果を結合して返す`Combine`メソッドがあり、
結果が両方とも成功の場合は`(TSource Left, TDest Right)`型のタプルを返します。
メソッドは、結合元の値を先に評価し、失敗の場合は`Func<T>`は実行されません。
結合元の値が成功の場合だけ、結合する`Func<T>`が実行されます。

```cs
var combineResult = Result.Error<string>(new SourceException())
  .Combine(() => Reulst.Error<string>(new DestException()));
combineResult.MapError(e =>
{
  if(e is SourceError srcErr)
  {
    //...
  }
});
```

## Result{T}の値を空にする(Emptyメソッド)

`Result{T}`の値を削除して`Result`を作成するには`Empty`メソッドを利用します。
結果が失敗の場合には、そのエラーは戻り値の`Result`に引き継がれます。

```cs
Result.Ok("Hello").Empty()
```

## ネストしたResultを解除する(Flattenメソッド)

`Result{T}`の型パラメーターが`Result{T}`もしくは`Reulst`の場合に、ネストを解除するには`Flatten`メソッドを利用します。

```cs
var nested = Result.Ok(Reulst.Ok("Good"));
var flat = nested.Flatten(); //Reulst.Ok("Good")
```

## 成功の場合の値が意図している値かを取得する(Containsメソッド)

`Result{T}`が成功の場合に、含まれている値が意図している値かどうかを調べます。失敗の場合は必ず`false`が返ります。
メソッドのオーバーライドで値だけを指定した場合は、`EqualityComparer<T>.Default`を利用して、比較されます。
また、`Func<T, bool>`を指定して比較ロジックを直接指定することもできます。

```cs
Result.Ok("Ok").Contains("Ok"); // true
Result.Ok("ok").Contains("OK", StringComparer.InvariantCultureIgnoreCase); // true
Result.Ok("Ok").Contains(v => v == "Ok"); // true
Result.Error<string>("err").Contains("err"); // false
```

## 失敗の場合の値が意図している値かを取得する(ContainsErrorメソッド)

`Result`および`Result{T}`が失敗の場合に、含まれているエラーの値が意図している値かどうかを調べます。成功の場合は必ず`false`が返ります。
メソッドのオーバーライドで値だけを指定した場合は、`EqualityComparer<Exception>.Default`を利用して、比較されます。
また、`Func<Exception, bool>`を指定して比較ロジックを直接指定することもできます。

```cs
var err = new Error();
Result.Error<string>(err).ContainsError(err); // true
Result.Error(err).ContainsError(err, EqualityComparer<Exception>.Default); // true
Result.Error(err).ContainsError(e => e == err); // true
Result.Ok("err").ContainsError("err"); // false
```

## 成功の場合と失敗の場合の処理を行う(Matchメソッド)

`Result`および`Result{T}`で設定された結果ごとの処理を実行します。設定した処理は`Result`または`Result{T}`を返し、その値が`Match`メソッドの結果として利用されます。

```cs
Result.Ok("ok").Match(
  ok: value => SomethingProcess(value),
  err: error => HandleError(error)
);
```

## Taskに変換する(AsTaskメソッド)

`Result`および`Result{T}`の`AsTask`メソッドで`Task`に変換できます。

```cs
await Result.Ok("ok").AsTask();
```

## 失敗の場合に成功の型を変換したResultを返す(PassOnErrorメソッド)

`Result`および`Result{T}`が失敗の場合に、成功の型を変換したいことがあります。そのような場合は`PassOnError`メソッドを利用します。

```cs
Result<string> result = Somemethod();
if(result.IsError()) return result.PassOnError<string, int>();
```
