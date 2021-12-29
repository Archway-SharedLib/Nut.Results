using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Nut.Results.AsyncLambdaSupport;

namespace Nut.Results.Test;

public class Match_ww
{
    // 書き心地
    public async Task Lambdaで呼び出す()
    {
        Task<Result> DoTask()
        {
            return Task.FromResult(Result.Ok());
        }

        ValueTask<Result> DoValueTask()
        {
            return new ValueTask<Result>();
        }

        Task<Result<int>> DoTaskV(int v)
        {
            return Task.FromResult(Result.Ok(v));
        }

        ValueTask<Result<int>> DoValueTaskV(int v)
        {
            return new ValueTask<Result<int>>(v);
        }

        Result.Ok().Match(() => Result.Ok(), (_) => Result.Ok());
        await Result.Ok().Match(() => Result.Ok().AsTask(), (_) => Result.Ok());
        await Result.Ok().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok());
        await Result.Ok().Match(async () => await DoTask(), (_) => Result.Ok());
        await Result.Ok().Match(async () => await DoValueTask(), (_) => Result.Ok());
        await Result.Ok().Match(() => Result.Ok(), (_) => Result.Ok().AsTask());
        await Result.Ok().Match(() => Result.Ok(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().Match(() => Result.Ok(), async (_) => await DoTask());
        await Result.Ok().Match(() => Result.Ok(), async (_) => await DoValueTask());
        await Result.Ok().Match(() => Result.Ok().AsTask(), (_) => Result.Ok().AsTask());
        await Result.Ok().Match(async () => await DoTask(), async (_) => await DoTask());
        await Result.Ok().Match(async () => await DoValueTask(), async (_) => await DoValueTask());
        await Result.Ok().Match(() => Result.Ok().AsTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().Match(async () => await DoTask(), async (_) => await DoValueTask());
        await Result.Ok().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok().AsTask());
        await Result.Ok().Match(async () => await DoValueTask(), async (_) => await DoTask());
        await Result.Ok().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().Match(async () => await DoValueTask(), async (_) => await DoValueTask());

        Result.Ok().Match(() => Result.Ok(1), (_) => Result.Ok(1));
        await Result.Ok().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1));
        await Result.Ok().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1));
        await Result.Ok().Match(async () => await DoTaskV(1), (_) => Result.Ok(1));
        await Result.Ok().Match(async () => await DoValueTaskV(1), (_) => Result.Ok(1));
        await Result.Ok().Match(() => Result.Ok(1), (_) => Result.Ok(1).AsTask());
        await Result.Ok().Match(() => Result.Ok(1), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().Match(() => Result.Ok(1), async (_) => await DoTaskV(1));
        await Result.Ok().Match(() => Result.Ok(1), async (_) => await DoValueTaskV(1));
        await Result.Ok().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok().Match(async () => await DoTaskV(1), async (_) => await DoTaskV(1));
        await Result.Ok().Match(async () => await DoValueTaskV(1), async (_) => await DoValueTaskV(1));
        await Result.Ok().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().Match(async () => await DoTaskV(1), async (_) => await DoValueTaskV(1));
        await Result.Ok().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok().Match(async () => await DoValueTaskV(1), async (_) => await DoTaskV(1));
        await Result.Ok().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().Match(async () => await DoValueTaskV(1), async (_) => await DoValueTaskV(1));

        Result.Ok(1).Match((_) => Result.Ok(), (_) => Result.Ok());
        await Result.Ok(1).Match((_) => Result.Ok().AsTask(), (_) => Result.Ok());
        await Result.Ok(1).Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok());
        await Result.Ok(1).Match(async (_) => await DoTask(), (_) => Result.Ok());
        await Result.Ok(1).Match(async (_) => await DoValueTask(), (_) => Result.Ok());
        await Result.Ok(1).Match((_) => Result.Ok(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).Match((_) => Result.Ok(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).Match((_) => Result.Ok(), async (_) => await DoTask());
        await Result.Ok(1).Match((_) => Result.Ok(), async (_) => await DoValueTask());
        await Result.Ok(1).Match((_) => Result.Ok().AsTask(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).Match(async (_) => await DoTask(), async (_) => await DoTask());
        await Result.Ok(1).Match(async (_) => await DoValueTask(), async (_) => await DoValueTask());
        await Result.Ok(1).Match((_) => Result.Ok().AsTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).Match(async (_) => await DoTask(), async (_) => await DoValueTask());
        await Result.Ok(1).Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).Match(async (_) => await DoValueTask(), async (_) => await DoTask());
        await Result.Ok(1).Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).Match(async (_) => await DoValueTask(), async (_) => await DoValueTask());

        Result.Ok(1).Match((_) => Result.Ok(1), (_) => Result.Ok(1));
        await Result.Ok(1).Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1));
        await Result.Ok(1).Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1));
        await Result.Ok(1).Match(async (_) => await DoTaskV(1), (_) => Result.Ok(1));
        await Result.Ok(1).Match(async (_) => await DoValueTaskV(1), (_) => Result.Ok(1));
        await Result.Ok(1).Match((_) => Result.Ok(1), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).Match((_) => Result.Ok(1), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).Match((_) => Result.Ok(1), async (_) => await DoTaskV(1));
        await Result.Ok(1).Match((_) => Result.Ok(1), async (_) => await DoValueTaskV(1));
        await Result.Ok(1).Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).Match(async (_) => await DoTaskV(1), async (_) => await DoTaskV(1));
        await Result.Ok(1).Match(async (_) => await DoValueTaskV(1), async (_) => await DoValueTaskV(1));
        await Result.Ok(1).Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).Match(async (_) => await DoTaskV(1), async (_) => await DoValueTaskV(1));
        await Result.Ok(1).Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).Match(async (_) => await DoValueTaskV(1), async (_) => await DoTaskV(1));
        await Result.Ok(1).Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).Match(async (_) => await DoValueTaskV(1), async (_) => await DoValueTaskV(1));

        // Task
        await Result.Ok().AsTask().Match(() => Result.Ok(), (_) => Result.Ok());
        await Result.Ok().AsTask().Match(() => Result.Ok().AsTask(), (_) => Result.Ok());
        await Result.Ok().AsTask().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok());
        await Result.Ok().AsTask().Match(async () => await DoTask(), (_) => Result.Ok());
        await Result.Ok().AsTask().Match(async () => await DoValueTask(), (_) => Result.Ok());
        await Result.Ok().AsTask().Match(() => Result.Ok(), (_) => Result.Ok().AsTask());
        await Result.Ok().AsTask().Match(() => Result.Ok(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().AsTask().Match(() => Result.Ok(), async (_) => await DoTask());
        await Result.Ok().AsTask().Match(() => Result.Ok(), async (_) => await DoValueTask());
        await Result.Ok().AsTask().Match(() => Result.Ok().AsTask(), (_) => Result.Ok().AsTask());
        await Result.Ok().AsTask().Match(T(async () => await DoTask()), T(async (_) => await DoTask()));
        await Result.Ok().AsTask().Match(VT(async () => await DoValueTask()), VT<IError>(async (_) => await DoValueTask()));
        await Result.Ok().AsTask().Match(() => Result.Ok().AsTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().AsTask().Match(T(async () => await DoTask()), VT(async (_) => await DoValueTask()));
        await Result.Ok().AsTask().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok().AsTask());
        await Result.Ok().AsTask().Match(VT(async () => await DoValueTask()), T(async (_) => await DoTask()));
        await Result.Ok().AsTask().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().AsTask().Match(VT(async () => await DoValueTask()), VT(async (_) => await DoValueTask()));

        await Result.Ok().AsTask().Match(() => Result.Ok(1), (_) => Result.Ok(1));
        await Result.Ok().AsTask().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1));
        await Result.Ok().AsTask().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1));
        await Result.Ok().AsTask().Match(async () => await DoTaskV(1), (_) => Result.Ok(1));
        await Result.Ok().AsTask().Match(async () => await DoValueTaskV(1), (_) => Result.Ok(1));
        await Result.Ok().AsTask().Match(() => Result.Ok(1), (_) => Result.Ok(1).AsTask());
        await Result.Ok().AsTask().Match(() => Result.Ok(1), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().AsTask().Match(() => Result.Ok(1), async (_) => await DoTaskV(1));
        await Result.Ok().AsTask().Match(() => Result.Ok(1), async (_) => await DoValueTaskV(1));
        await Result.Ok().AsTask().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok().AsTask().Match(T(async () => await DoTaskV(1)), T(async (_) => await DoTaskV(1)));
        await Result.Ok().AsTask().Match(VT(async () => await DoValueTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
        await Result.Ok().AsTask().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().AsTask().Match(T(async () => await DoTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
        await Result.Ok().AsTask().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok().AsTask().Match(VT(async () => await DoValueTaskV(1)), T(async (_) => await DoTaskV(1)));
        await Result.Ok().AsTask().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().AsTask().Match(VT(async () => await DoValueTaskV(1)), VT(async (_) => await DoValueTaskV(1)));

        await Result.Ok(1).AsTask().Match((_) => Result.Ok(), (_) => Result.Ok());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok().AsTask(), (_) => Result.Ok());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok());
        await Result.Ok(1).AsTask().Match(async (_) => await DoTask(), (_) => Result.Ok());
        await Result.Ok(1).AsTask().Match(async (_) => await DoValueTask(), (_) => Result.Ok());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(), async (_) => await DoTask());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(), async (_) => await DoValueTask());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok().AsTask(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).AsTask().Match(T(async (int _) => await DoTask()), T(async (_) => await DoTask()));
        await Result.Ok(1).AsTask().Match(VT(async (int _) => await DoValueTask()), VT(async (_) => await DoValueTask()));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok().AsTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).AsTask().Match(T(async (int _) => await DoTask()), VT(async (_) => await DoValueTask()));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).AsTask().Match(VT(async (int _) => await DoValueTask()), T(async (_) => await DoTask()));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).AsTask().Match(VT(async (int _) => await DoValueTask()), VT(async (_) => await DoValueTask()));

        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1), (_) => Result.Ok(1));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1));
        await Result.Ok(1).AsTask().Match(async (_) => await DoTaskV(1), (_) => Result.Ok(1));
        await Result.Ok(1).AsTask().Match(async (_) => await DoValueTaskV(1), (_) => Result.Ok(1));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1), async (_) => await DoTaskV(1));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1), async (_) => await DoValueTaskV(1));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).AsTask().Match(T(async (int _) => await DoTaskV(1)), T(async (_) => await DoTaskV(1)));
        await Result.Ok(1).AsTask().Match(VT(async (int _) => await DoValueTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).AsTask().Match(T(async (int _) => await DoTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).AsTask().Match(VT(async (int _) => await DoValueTaskV(1)), T(async (_) => await DoTaskV(1)));
        await Result.Ok(1).AsTask().Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).AsTask().Match(VT(async (int _) => await DoValueTaskV(1)), VT(async (_) => await DoValueTaskV(1)));

        // ValueTask
        await Result.Ok().AsValueTask().Match(() => Result.Ok(), (_) => Result.Ok());
        await Result.Ok().AsValueTask().Match(() => Result.Ok().AsTask(), (_) => Result.Ok());
        await Result.Ok().AsValueTask().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok());
        await Result.Ok().AsValueTask().Match(async () => await DoTask(), (_) => Result.Ok());
        await Result.Ok().AsValueTask().Match(async () => await DoValueTask(), (_) => Result.Ok());
        await Result.Ok().AsValueTask().Match(() => Result.Ok(), (_) => Result.Ok().AsTask());
        await Result.Ok().AsValueTask().Match(() => Result.Ok(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().AsValueTask().Match(() => Result.Ok(), async (_) => await DoTask());
        await Result.Ok().AsValueTask().Match(() => Result.Ok(), async (_) => await DoValueTask());
        await Result.Ok().AsValueTask().Match(() => Result.Ok().AsTask(), (_) => Result.Ok().AsTask());
        await Result.Ok().AsValueTask().Match(T(async () => await DoTask()), T(async (_) => await DoTask()));
        await Result.Ok().AsValueTask().Match(VT(async () => await DoValueTask()), VT(async (_) => await DoValueTask()));
        await Result.Ok().AsValueTask().Match(() => Result.Ok().AsTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().AsValueTask().Match(T(async () => await DoTask()), VT(async (_) => await DoValueTask()));
        await Result.Ok().AsValueTask().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok().AsTask());
        await Result.Ok().AsValueTask().Match(VT(async () => await DoValueTask()), T(async (_) => await DoTask()));
        await Result.Ok().AsValueTask().Match(() => Result.Ok().AsValueTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok().AsValueTask().Match(VT(async () => await DoValueTask()), VT(async (_) => await DoValueTask()));

        await Result.Ok().AsValueTask().Match(() => Result.Ok(1), (_) => Result.Ok(1));
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1));
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1));
        await Result.Ok().AsValueTask().Match(async () => await DoTaskV(1), (_) => Result.Ok(1));
        await Result.Ok().AsValueTask().Match(async () => await DoValueTaskV(1), (_) => Result.Ok(1));
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1), (_) => Result.Ok(1).AsTask());
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1), async (_) => await DoTaskV(1));
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1), async (_) => await DoValueTaskV(1));
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok().AsValueTask().Match(T(async () => await DoTaskV(1)), T(async (_) => await DoTaskV(1)));
        await Result.Ok().AsValueTask().Match(VT(async () => await DoValueTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().AsValueTask().Match(T(async () => await DoTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok().AsValueTask().Match(VT(async () => await DoValueTaskV(1)), T(async (_) => await DoTaskV(1)));
        await Result.Ok().AsValueTask().Match(() => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok().AsValueTask().Match(VT(async () => await DoValueTaskV(1)), VT(async (_) => await DoValueTaskV(1)));

        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(), (_) => Result.Ok());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok().AsTask(), (_) => Result.Ok());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok());
        await Result.Ok(1).AsValueTask().Match(async (_) => await DoTask(), (_) => Result.Ok());
        await Result.Ok(1).AsValueTask().Match(async (_) => await DoValueTask(), (_) => Result.Ok());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(), async (_) => await DoTask());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(), async (_) => await DoValueTask());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok().AsTask(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).AsValueTask().Match(T(async (int _) => await DoTask()), T(async (_) => await DoTask()));
        await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTask()), VT(async (_) => await DoValueTask()));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok().AsTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).AsValueTask().Match(T(async (int _) => await DoTask()), VT(async (_) => await DoValueTask()));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok().AsTask());
        await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTask()), T(async (_) => await DoTask()));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok().AsValueTask(), (_) => Result.Ok().AsValueTask());
        await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTask()), VT(async (_) => await DoValueTask()));

        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1), (_) => Result.Ok(1));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1));
        await Result.Ok(1).AsValueTask().Match(async (_) => await DoTaskV(1), (_) => Result.Ok(1));
        await Result.Ok(1).AsValueTask().Match(async (_) => await DoValueTaskV(1), (_) => Result.Ok(1));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1), async (_) => await DoTaskV(1));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1), async (_) => await DoValueTaskV(1));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).AsValueTask().Match(T(async (int _) => await DoTaskV(1)), T(async (_) => await DoTaskV(1)));
        await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1).AsTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).AsValueTask().Match(T(async (int _) => await DoTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsTask());
        await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTaskV(1)), T(async (_) => await DoTaskV(1)));
        await Result.Ok(1).AsValueTask().Match((_) => Result.Ok(1).AsValueTask(), (_) => Result.Ok(1).AsValueTask());
        await Result.Ok(1).AsValueTask().Match(VT(async (int _) => await DoValueTaskV(1)), VT(async (_) => await DoValueTaskV(1)));
    }
}
