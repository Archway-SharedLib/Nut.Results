using System;
using System.Threading.Tasks;

namespace Archway.Results
{
    public static class ResultApplyExtensions
    {
         // sync - sync Void -> T
         public static Result<T> Apply<T>(this Result source, Func<T> ok)
         {
             if (ok == null) throw new ArgumentNullException(nameof(ok));
             if (!source.IsOk) return Result.Error<T>(source.errorValue);
        
             var value = ok();
             return Result.Ok(value);
         }
        
         //async - sync Void -> T
         public static async Task<Result<T>> Apply<T>(this Task<Result> source, Func<T> ok)
         {
             if (source == null) throw new ArgumentNullException(nameof(source));
             if (ok == null) throw new ArgumentNullException(nameof(ok));
             
             var result = await source.ConfigureAwait(false);
             if (!result.IsOk) return Result.Error<T>(result.errorValue);
        
             var value = ok();
             return Result.Ok(value);
         }
        
         //sync - async Void -> T
         public static async Task<Result<T>> Apply<T>(this Result source, Func<Task<T>> ok)
         {
             if (ok == null) throw new ArgumentNullException(nameof(ok));
             if (!source.IsOk) return Result.Error<T>(source.errorValue);
        
             var value = await ok().ConfigureAwait(false);
             return Result.Ok(value);
         }
        
         //async - async Void -> T
         public static async Task<Result<T>> Apply<T>(this Task<Result> source, Func<Task<T>> ok)
         {
             if (source == null) throw new ArgumentNullException(nameof(source));
             if (ok == null) throw new ArgumentNullException(nameof(ok));
             
             var result = await source.ConfigureAwait(false);
             if (!result.IsOk) return Result.Error<T>(result.errorValue);
        
             var value = await ok().ConfigureAwait(false);
             return Result.Ok(value);
         }
    }
}