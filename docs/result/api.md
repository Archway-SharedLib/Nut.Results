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

成功の結果を表す`Result`を作成するには`Result`型の静的メソッドである`Error(IError)`を利用します。

```cs
Result dataNotFoundResult = Result.Error(new DataNotFoundError());
```

`IError`インターフェイスは失敗の詳細を表します。エラーの詳細がメッセージだけなどの場合は`Error(string)`メソッドを利用します。
この場合は、インスタンスには`Error`型が利用されます。

```cs
Result errorResult = Result.Error("This method is fail.");
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
var error = task2Result.GetErrorOr(_ => new WarningError());
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
    if(IsNotExpectedValue(result)) return Result.Error<string>(new UnexpectedValueError());
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

```cs
var combineResult = Result.Ok("Hello").Combine(Reulst.Ok("World"));
combineResult.Map(v => 
{
  return v.Left + " " + v.Right; // Hello World 
});
```

結合する`Result{T}`の指定には、値を渡すオーバーライドと、`Func<T>`を渡すオーバーライドがあります。

値を渡すオーバーライドは、どちらか一方にエラーがある場合はそのエラーを返し、両方ともにエラーがある場合は`AggregateError`でエラーをまとめます。

```cs
var combineResult = Result.Error<string>(new SourceError()).Combine(Reulst.Error<string>(new DestError()));
combineResult.MapError(e => 
{
  if(e is AggregateError aggrErr) 
  {
    //...
  } 
});
```

`Func<T>`を渡すオーバーライドは、結合元の値が先に評価され、エラーの場合は`Func<T>`は実行されません。
結合元の値が成功の場合だけ、結合する`Func<T>`が実行されます。

```cs
var combineResult = Result.Error<string>(new SourceError()).Combine(() => Reulst.Error<string>(new DestError()));
combineResult.MapError(e => 
{
  if(e is SourceError srcErr) 
  {
    //...
  } 
});
```