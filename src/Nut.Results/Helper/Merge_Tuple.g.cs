// <auto-generated />
using System;

namespace Nut.Results;

public static partial class ResultHelper
{
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1)> Merge<T0, T1>(in Result<T0> dest0, in Result<T1> dest1)
        => dest0.Merge(dest1);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2)> Merge<T0, T1, T2>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2)
        => dest0.Merge(dest1, dest2);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3)> Merge<T0, T1, T2, T3>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3)
        => dest0.Merge(dest1, dest2, dest3);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4)> Merge<T0, T1, T2, T3, T4>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4)
        => dest0.Merge(dest1, dest2, dest3, dest4);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5)> Merge<T0, T1, T2, T3, T4, T5>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6)> Merge<T0, T1, T2, T3, T4, T5, T6>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7)> Merge<T0, T1, T2, T3, T4, T5, T6, T7>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
    /// <typeparam name="T1">1番目の成功の型</typeparam>
    /// <typeparam name="T2">2番目の成功の型</typeparam>
    /// <typeparam name="T3">3番目の成功の型</typeparam>
    /// <typeparam name="T4">4番目の成功の型</typeparam>
    /// <typeparam name="T5">5番目の成功の型</typeparam>
    /// <typeparam name="T6">6番目の成功の型</typeparam>
    /// <typeparam name="T7">7番目の成功の型</typeparam>
    /// <typeparam name="T8">8番目の成功の型</typeparam>
    /// <returns>マージした結果</returns>
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7, dest8);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
    /// <param name="dest1">1番目の結果</param>
    /// <param name="dest2">2番目の結果</param>
    /// <param name="dest3">3番目の結果</param>
    /// <param name="dest4">4番目の結果</param>
    /// <param name="dest5">5番目の結果</param>
    /// <param name="dest6">6番目の結果</param>
    /// <param name="dest7">7番目の結果</param>
    /// <param name="dest8">8番目の結果</param>
    /// <param name="dest9">9番目の結果</param>
    /// <typeparam name="T0">最初の成功の型</typeparam>
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
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7, dest8, dest9);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
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
    /// <typeparam name="T0">最初の成功の型</typeparam>
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
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7, dest8, dest9, dest10);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
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
    /// <typeparam name="T0">最初の成功の型</typeparam>
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
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7, dest8, dest9, dest10, dest11);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
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
    /// <typeparam name="T0">最初の成功の型</typeparam>
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
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11, in Result<T12> dest12)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7, dest8, dest9, dest10, dest11, dest12);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
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
    /// <typeparam name="T0">最初の成功の型</typeparam>
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
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11, in Result<T12> dest12, in Result<T13> dest13)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7, dest8, dest9, dest10, dest11, dest12, dest13);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
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
    /// <typeparam name="T0">最初の成功の型</typeparam>
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
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11, in Result<T12> dest12, in Result<T13> dest13, in Result<T14> dest14)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7, dest8, dest9, dest10, dest11, dest12, dest13, dest14);
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <param name="dest0">最初の結果</param>
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
    /// <typeparam name="T0">最初の成功の型</typeparam>
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
    public static Result<(T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> Merge<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in Result<T0> dest0, in Result<T1> dest1, in Result<T2> dest2, in Result<T3> dest3, in Result<T4> dest4, in Result<T5> dest5, in Result<T6> dest6, in Result<T7> dest7, in Result<T8> dest8, in Result<T9> dest9, in Result<T10> dest10, in Result<T11> dest11, in Result<T12> dest12, in Result<T13> dest13, in Result<T14> dest14, in Result<T15> dest15)
        => dest0.Merge(dest1, dest2, dest3, dest4, dest5, dest6, dest7, dest8, dest9, dest10, dest11, dest12, dest13, dest14, dest15);
}