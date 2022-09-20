// <auto-generated />
using System;
using System.Collections.Generic;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1)> Merge<T0, T1>(this in Result<T0> source, in Result<T1> dest1)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2)> Merge<T0, T1, T2>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3)> Merge<T0, T1, T2, T3>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4)> Merge<T0, T1, T2, T3, T4>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5)> Merge<T0, T1, T2, T3, T4, T5>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6)> Merge<T0, T1, T2, T3, T4, T5, T6>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7)> Merge<T0, T1, T2, T3, T4, T5, T6, T7>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if(dest8.IsError) errors.Add(dest8._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7, T8)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value, dest8._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <param name="dest9">9番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <typeparam name="T9">9番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if(dest8.IsError) errors.Add(dest8._capturedError.SourceException);
        if(dest9.IsError) errors.Add(dest9._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value, dest8._value, dest9._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <param name="dest9">9番目の結果</param>
    /// <param name="dest10">10番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <typeparam name="T9">9番目の成功の型</typeparam>
    /// <typeparam name="T10">10番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if(dest8.IsError) errors.Add(dest8._capturedError.SourceException);
        if(dest9.IsError) errors.Add(dest9._capturedError.SourceException);
        if(dest10.IsError) errors.Add(dest10._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value, dest8._value, dest9._value, dest10._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <param name="dest9">9番目の結果</param>
    /// <param name="dest10">10番目の結果</param>
    /// <param name="dest11">11番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <typeparam name="T9">9番目の成功の型</typeparam>
    /// <typeparam name="T10">10番目の成功の型</typeparam>
    /// <typeparam name="T11">11番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if(dest8.IsError) errors.Add(dest8._capturedError.SourceException);
        if(dest9.IsError) errors.Add(dest9._capturedError.SourceException);
        if(dest10.IsError) errors.Add(dest10._capturedError.SourceException);
        if(dest11.IsError) errors.Add(dest11._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value, dest8._value, dest9._value, dest10._value, dest11._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <param name="dest9">9番目の結果</param>
    /// <param name="dest10">10番目の結果</param>
    /// <param name="dest11">11番目の結果</param>
    /// <param name="dest12">12番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <typeparam name="T9">9番目の成功の型</typeparam>
    /// <typeparam name="T10">10番目の成功の型</typeparam>
    /// <typeparam name="T11">11番目の成功の型</typeparam>
    /// <typeparam name="T12">12番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11, in Result<T12> dest12)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if(dest8.IsError) errors.Add(dest8._capturedError.SourceException);
        if(dest9.IsError) errors.Add(dest9._capturedError.SourceException);
        if(dest10.IsError) errors.Add(dest10._capturedError.SourceException);
        if(dest11.IsError) errors.Add(dest11._capturedError.SourceException);
        if(dest12.IsError) errors.Add(dest12._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value, dest8._value, dest9._value, dest10._value, dest11._value, dest12._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <param name="dest9">9番目の結果</param>
    /// <param name="dest10">10番目の結果</param>
    /// <param name="dest11">11番目の結果</param>
    /// <param name="dest12">12番目の結果</param>
    /// <param name="dest13">13番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <typeparam name="T9">9番目の成功の型</typeparam>
    /// <typeparam name="T10">10番目の成功の型</typeparam>
    /// <typeparam name="T11">11番目の成功の型</typeparam>
    /// <typeparam name="T12">12番目の成功の型</typeparam>
    /// <typeparam name="T13">13番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11, in Result<T12> dest12, in Result<T13> dest13)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if(dest8.IsError) errors.Add(dest8._capturedError.SourceException);
        if(dest9.IsError) errors.Add(dest9._capturedError.SourceException);
        if(dest10.IsError) errors.Add(dest10._capturedError.SourceException);
        if(dest11.IsError) errors.Add(dest11._capturedError.SourceException);
        if(dest12.IsError) errors.Add(dest12._capturedError.SourceException);
        if(dest13.IsError) errors.Add(dest13._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value, dest8._value, dest9._value, dest10._value, dest11._value, dest12._value, dest13._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <param name="dest9">9番目の結果</param>
    /// <param name="dest10">10番目の結果</param>
    /// <param name="dest11">11番目の結果</param>
    /// <param name="dest12">12番目の結果</param>
    /// <param name="dest13">13番目の結果</param>
    /// <param name="dest14">14番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <typeparam name="T9">9番目の成功の型</typeparam>
    /// <typeparam name="T10">10番目の成功の型</typeparam>
    /// <typeparam name="T11">11番目の成功の型</typeparam>
    /// <typeparam name="T12">12番目の成功の型</typeparam>
    /// <typeparam name="T13">13番目の成功の型</typeparam>
    /// <typeparam name="T14">14番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11, in Result<T12> dest12, in Result<T13> dest13, in Result<T14> dest14)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if(dest8.IsError) errors.Add(dest8._capturedError.SourceException);
        if(dest9.IsError) errors.Add(dest9._capturedError.SourceException);
        if(dest10.IsError) errors.Add(dest10._capturedError.SourceException);
        if(dest11.IsError) errors.Add(dest11._capturedError.SourceException);
        if(dest12.IsError) errors.Add(dest12._capturedError.SourceException);
        if(dest13.IsError) errors.Add(dest13._capturedError.SourceException);
        if(dest14.IsError) errors.Add(dest14._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value, dest8._value, dest9._value, dest10._value, dest11._value, dest12._value, dest13._value, dest14._value));
    }

    /// <summary>
    /// 引数で渡された <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <param name="dest9">9番目の結果</param>
    /// <param name="dest10">10番目の結果</param>
    /// <param name="dest11">11番目の結果</param>
    /// <param name="dest12">12番目の結果</param>
    /// <param name="dest13">13番目の結果</param>
    /// <param name="dest14">14番目の結果</param>
    /// <param name="dest15">15番目の結果</param>
    /// <typeparam name="T0">もととなる結果の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <typeparam name="T9">9番目の成功の型</typeparam>
    /// <typeparam name="T10">10番目の成功の型</typeparam>
    /// <typeparam name="T11">11番目の成功の型</typeparam>
    /// <typeparam name="T12">12番目の成功の型</typeparam>
    /// <typeparam name="T13">13番目の成功の型</typeparam>
    /// <typeparam name="T14">14番目の成功の型</typeparam>
    /// <typeparam name="T15">15番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this in Result<T0> source, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11, in Result<T12> dest12, in Result<T13> dest13, in Result<T14> dest14, in Result<T15> dest15)
    {
        var errors = new List<Exception>();
        if(source.IsError) errors.Add(source._capturedError.SourceException);
        if(dest1.IsError) errors.Add(dest1._capturedError.SourceException);
        if(dest2.IsError) errors.Add(dest2._capturedError.SourceException);
        if(dest3.IsError) errors.Add(dest3._capturedError.SourceException);
        if(dest4.IsError) errors.Add(dest4._capturedError.SourceException);
        if(dest5.IsError) errors.Add(dest5._capturedError.SourceException);
        if(dest6.IsError) errors.Add(dest6._capturedError.SourceException);
        if(dest7.IsError) errors.Add(dest7._capturedError.SourceException);
        if(dest8.IsError) errors.Add(dest8._capturedError.SourceException);
        if(dest9.IsError) errors.Add(dest9._capturedError.SourceException);
        if(dest10.IsError) errors.Add(dest10._capturedError.SourceException);
        if(dest11.IsError) errors.Add(dest11._capturedError.SourceException);
        if(dest12.IsError) errors.Add(dest12._capturedError.SourceException);
        if(dest13.IsError) errors.Add(dest13._capturedError.SourceException);
        if(dest14.IsError) errors.Add(dest14._capturedError.SourceException);
        if(dest15.IsError) errors.Add(dest15._capturedError.SourceException);
        if (errors.Count > 0) return Result.Error<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(new AggregateException(errors));
        return Result.Ok((source._value, dest1._value, dest2._value, dest3._value, dest4._value, dest5._value, dest6._value, dest7._value, dest8._value, dest9._value, dest10._value, dest11._value, dest12._value, dest13._value, dest14._value, dest15._value));
    }

}
