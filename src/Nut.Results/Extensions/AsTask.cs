// ReSharper disable CheckNamespace

using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        public static Task<Result> AsTask(this Result source) => Task.FromResult(source);
    }
}