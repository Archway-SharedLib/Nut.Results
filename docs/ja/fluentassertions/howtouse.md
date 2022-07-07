# Nut.Results.FluentAssertionsの使い方

## 同じ結果かどうかを検証する(Beメソッド)

`Result`もしくは`Result{T}`が引数で与えられた値と同じ値かどうかを検証するには`Be`メソッドを利用します。

```cs
var result1 = Result.Ok("Success");
var result2 = Result.Ok("Success");
result1.Should().Be(result2);
```

## 成功かどうかを検証する(BeOkメソッド)

`Result`もしくは`Result{T}`が成功かどうかを検証するには`BeOk`メソッドを利用します。

```cs
var result = Result.Ok();
result.Should().BeOk();
```

## 失敗かどうかを検証する(BeErrorメソッド)

`Result`もしくは`Result{T}`が失敗かどうかを検証するには`BeError`メソッドを利用します。

```cs
var result = Result.Error(new Exception());
result.Should().BeError();
```

## 値が正しいかどうかを検証する(Matchメソッド)

`Result`もしくは`Result{T}`が失敗の場合、あるいは`Result{T}`が成功の場合に、設定されている値が正しいかどうかを検証するには`Match`メソッドを利用します。

```cs
var okResult = Result.Ok("Success");
okResult.Should().BeOk().And.Match(value => value == "Success");

var err = new Exception();
var errorResult = Result.Error(err);
errorResult.Should().BeError().And.Match(e => e == err);
```

## 失敗の場合にエラーオブジェクトの型が正しいかどうかを検証する(BeOfTypeメソッド)

`Result`もしくは`Result{T}`が失敗の場合に、設定されている`IError`の型が正しいかどうかを検証するには`BeOfType`メソッドを利用します。

```cs
var errorResult = Result.Error(new DataNotFoundException());
errorResult.Should().BeError().And.BeOfType(typeof(DataNotFoundException));

var errorResult2 = Result.Error(new DuplicateDataException());
errorResult2.Should().BeError().And.BeOfType<DuplicateDataException>();
```

## 失敗のj場合にエラーオブジェクトに設定されているメッセージが正しいかどうかを検証する(WithMessageメソッド)

`Result`もしくは`Result{T}`が失敗の場合に、設定されている`Exception`のメッセージの値が正しいかどうかを検証するには`WithMessage`メソッドを利用します。

```cs
var errorResult = Result.Error(new Exception("Oops."));
errorResult.Should().BeError().And.WithMessage("Oops.");
```
