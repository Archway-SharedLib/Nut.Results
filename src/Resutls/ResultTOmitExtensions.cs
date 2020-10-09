using System;
using System.Threading.Tasks;

namespace Archway.Results
{
    public static class ResultTOmitExtensions
    {
         // sync - sync T1 -> Void
         public static Result Omit<T>(this Result<T> source, Action<T> ok)
         {
             if (ok == null) throw new ArgumentNullException(nameof(ok));
             if (!source.IsOk) return Result.Error(source.errorValue);
        
             ok(source.value);
             return Result.Ok();
         }
        
         //async - sync T1 -> Void
         public static async Task<Result> Omit<T>(this Task<Result<T>> source, Action<T> ok)
         {
             if (source == null) throw new ArgumentNullException(nameof(source));
             if (ok == null) throw new ArgumentNullException(nameof(ok));
             
             var result = await source.ConfigureAwait(false);
             if (!result.IsOk) return Result.Error(result.errorValue);
        
             ok(result.value);
             return Result.Ok();
         }
        
         //sync - async T1 -> Void
         public static async Task<Result> Omit<T>(this Result<T> source, Func<T, Task> ok)
         {
             if (ok == null) throw new ArgumentNullException(nameof(ok));
             if (!source.IsOk) return Result.Error(source.errorValue);
        
             await ok(source.value);
             return Result.Ok();
         }
        
         //async - async T1 -> Void 
         public static async Task<Result> Omit<T>(this Task<Result<T>> source, Func<T, Task> ok)
         {
             if (source == null) throw new ArgumentNullException(nameof(source));
             if (ok == null) throw new ArgumentNullException(nameof(ok));
             
             var result = await source.ConfigureAwait(false);
             if (!result.IsOk) return Result.Error(result.errorValue);
        
             await ok(result.value).ConfigureAwait(false);
             return Result.Ok();
         }
    }
}