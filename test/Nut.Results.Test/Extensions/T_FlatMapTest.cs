using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.Test.TestUtil;

namespace Nut.Results.Test
{
    public class T_FlatMapTest
    {
        //T1 -> Void
        
        [Fact]
        public void TVoid_FlatMap_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("A").FlatMap(null as Func<string, Result>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void TVoid_FlatMap_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            var result = errResult.FlatMap(_ =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public void TVoid_FlatMap_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = Result.Ok("123").FlatMap(_ =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }

        [Fact]
        public void TVoid_FlatMap_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).FlatMap(null as Func<string, Result>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void TVoid_FlatMap_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result<string>>, _ => Result.Ok());
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task TVoid_FlatMap_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            var result = await errResult.FlatMap(_ =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task TVoid_FlatMap_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("123")).FlatMap(_ =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        [Fact]
        public void TVoid_FlatMap_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("A").FlatMap(null as Func<string, Task<Result>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task TVoid_FlatMap_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            var result = await errResult.FlatMap(_ =>
            {
                executed = true;
                return Task.Run(() => Result.Ok());
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task TVoid_FlatMap_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Result.Ok("123").FlatMap(_ =>
            {
                executed = true;
                return Task.Run(() => Result.Ok());
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        [Fact]
        public void TVoid_FlatMap_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).FlatMap(null as Func<string, Task<Result>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void TVoid_FlatMap_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result<string>>, _ => Task.Run(() => Result.Ok()));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task TVoid_FlatMap_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            var result = await errResult.FlatMap(_ =>
            {
                executed = true;
                return Task.Run(() => Result.Ok());
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task TVoid_FlatMap_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("123")).FlatMap(_ =>
            {
                executed = true;
                return Task.Run(() => Result.Ok());
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        //T1 -> T2
        [Fact]
        public void TT_FlatMap_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("A").FlatMap(null as Func<string, Result<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void TT_FlatMap_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            var result = errResult.FlatMap(_ =>
            {
                executed = true;
                return Result.Ok("ok");
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public void TT_FlatMap_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = Result.Ok("123").FlatMap(_ =>
            {
                executed = true;
                return Result.Ok("ok");
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == "ok");
        }

        [Fact]
        public void TT_FlatMap_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).FlatMap(null as Func<string, Result<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void TT_FlatMap_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result<string>>, _ => Result.Ok("ok"));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task TT_FlatMap_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            var result = await errResult.FlatMap(_ =>
            {
                executed = true;
                return Result.Ok("ok");
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task TT_FlatMap_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("123")).FlatMap(_ =>
            {
                executed = true;
                return Result.Ok("ok");
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void TT_FlatMap_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("A").FlatMap(null as Func<string, Task<Result<string>>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task TT_FlatMap_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            var result = await errResult.FlatMap(_ =>
            {
                executed = true;
                return Task.Run(() => Result.Ok("ok"));
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task TT_FlatMap_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Result.Ok("123").FlatMap(_ =>
            {
                executed = true;
                return Task.Run(() => Result.Ok("ok"));
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void TT_FlatMap_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).FlatMap(null as Func<string, Task<Result<string>>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void TT_FlatMap_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result<string>>, _ => Task.Run(() => Result.Ok("ok")));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task TT_FlatMap_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            var result = await errResult.FlatMap(_ =>
            {
                executed = true;
                return Task.Run(() => Result.Ok("ok"));
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task TT_FlatMap_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("123")).FlatMap(_ =>
            {
                executed = true;
                return Task.Run(() => Result.Ok("ok"));
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
    }
}