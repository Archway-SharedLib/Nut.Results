using System;
using System.Threading.Tasks;

namespace Nut.Results;

/// <summary>
/// <see cref="Task"/> または <see cref="ValueTask"/> を引数に取るメソッドでラムダで指定したときにオーバーライドの解決をサポートします。
/// </summary>
public static class AsyncLambdaSupport
{
    /// <summary>
    /// 引数で渡された値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok().AsTask().Match(T(async () => await DoTask()), T(async (_) => await DoTask()));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <returns>引数で渡された値</returns>
    public static Func<Task<Result>> T(Func<Task<Result>> action) => action;

    /// <summary>
    /// 引数で渡された値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok(1).AsTask().Match(T(async (int _) => await DoTask()), T(async (_) => await DoTask()));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <typeparam name="T">第1引数の型</typeparam>
    /// <returns>引数で渡された値</returns>
    public static Func<T,Task<Result>> T<T>(Func<T, Task<Result>> action) => action;

    /// <summary>
    /// 引数で渡された<see cref="IError"/>を引数に受け取る値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok(1).AsTask().Match(T(async (int _) => await DoTask()), T(async (_) => await DoTask()));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <returns>引数で渡された値</returns>
    public static Func<IError,Task<Result>> T(Func<IError, Task<Result>> action) => action;

    /// <summary>
    /// 引数で渡された値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok().AsTask().Match(T(async (int _) => await DoTask(1)), T(async (_) => await DoTask(1)));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>引数で渡された値</returns>
    public static Func<Task<Result<TResult>>> T<TResult>(Func<Task<Result<TResult>>> action) => action;

    /// <summary>
    /// 引数で渡された値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok(1).AsTask().Match(T(async (int _) => await DoTask(1)), T(async (_) => await DoTask(1)));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <typeparam name="T">第1引数の型</typeparam>
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>引数で渡された値</returns>
    public static Func<T,Task<Result<TResult>>> T<T, TResult>(Func<T, Task<Result<TResult>>> action) => action;

    /// <summary>
    /// 引数で渡された<see cref="IError"/>を引数に受け取る値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok(1).AsTask().Match(T(async (int _) => await DoTask(1)), T(async (_) => await DoTask(1)));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>引数で渡された値</returns>
    public static Func<IError,Task<Result<TResult>>> T<TResult>(Func<IError, Task<Result<TResult>>> action) => action;

    /// <summary>
    /// 引数で渡された値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok().AsValueTask().Match(VT(async () => await DoValueTask()), VT(async (_) => await DoValueTask()));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <returns>引数で渡された値</returns>
    public static Func<ValueTask<Result>> VT(Func<ValueTask<Result>> action) => action;

    /// <summary>
    /// 引数で渡された値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTask()), VT(async (_) => await DoValueTask()));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <typeparam name="T">第1引数の型</typeparam>
    /// <returns>引数で渡された値</returns>
    public static Func<T,ValueTask<Result>> VT<T>(Func<T, ValueTask<Result>> action) => action;

    /// <summary>
    /// 引数で渡された<see cref="IError"/>を引数に受け取る値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTask()), VT(async (_) => await DoValueTask()));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <returns>引数で渡された値</returns>
    public static Func<IError,ValueTask<Result>> VT(Func<IError, ValueTask<Result>> action) => action;

    /// <summary>
    /// 引数で渡された値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok().AsValueTask().Match(VT(async (int _) => await DoValueTask(1)), VT(async (_) => await DoValueTask(1)));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>引数で渡された値</returns>
    public static Func<ValueTask<Result<TResult>>> VT<TResult>(Func<ValueTask<Result<TResult>>> action) => action;

    /// <summary>
    /// 引数で渡された値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTask(1)), VT(async (_) => await DoValueTask(1)));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <typeparam name="T">第1引数の型</typeparam>
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>引数で渡された値</returns>
    public static Func<T,ValueTask<Result<TResult>>> VT<T, TResult>(Func<T, ValueTask<Result<TResult>>> action) => action;

    /// <summary>
    /// 引数で渡された<see cref="IError"/>を引数に受け取る値をそのまま返します。
    /// </summary>
    /// <example>
    /// using static Nut.Results.AsyncLambdaSupport;
    /// await Result.Ok(1).AsTask().Match(VT(async (int _) => await DoValueTask(1)), VT(async (_) => await DoValueTask(1)));
    /// </example>
    /// <param name="action">実行される処理</param>
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>引数で渡された値</returns>
    public static Func<IError,ValueTask<Result<TResult>>> VT<TResult>(Func<IError, ValueTask<Result<TResult>>> action) => action;
}
