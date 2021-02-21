// ReSharper disable CheckNamespace

using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        public static Task<Result<T>> AsTask<T>(this Result<T> source) => Task.FromResult(source);
    }
}