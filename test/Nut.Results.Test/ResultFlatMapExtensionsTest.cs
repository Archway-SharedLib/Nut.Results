using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.Test.TestUtil;

namespace Nut.Results.Test
{
    public class ResultFlatMapExtensionsTest
    {
        //Void -> T
        
        [Fact]
        public void VoidT_FlatMap_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().FlatMap(null as Func<Result<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void VoidT_FlatMap_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error(error);
            var executed = false;
            var result = errResult.FlatMap(() =>
            {
                executed = true;
                return Result.Ok("value");
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public void VoidT_FlatMap_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expectedValue = "ok";
            var result = Result.Ok().FlatMap(() =>
            {
                executed = true;
                return Result.Ok(expectedValue);
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == expectedValue);
        }

        [Fact]
        public void VoidT_FlatMap_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).FlatMap(null as Func<Result<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void VoidT_FlatMap_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultFlatMapExtensions.FlatMap(null as Task<Result>, () => Result.Ok("ok"));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task VoidT_FlatMap_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error(error));
            var executed = false;
            var result = await errResult.FlatMap(() =>
            {
                executed = true;
                return Result.Ok("ok");
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task VoidT_FlatMap_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expectedValue = "ok";
            var result = await Task.FromResult(Result.Ok()).FlatMap(() =>
            {
                executed = true;
                return Result.Ok(expectedValue);
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == expectedValue);
        }
        
        [Fact]
        public void VoidT_FlatMap_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().FlatMap(null as Func<Task<Result<string>>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task VoidT_FlatMap_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error(error);
            var executed = false;
            var result = await errResult.FlatMap(() =>
            {
                executed = true;
                return Task.FromResult(Result.Ok("ok"));
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task VoidT_FlatMap_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expectValue = "ok";
            var result = await Result.Ok().FlatMap(() =>
            {
                executed = true;
                return Task.FromResult(Result.Ok(expectValue));
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == expectValue);
        }
        
        [Fact]
        public void VoidT_FlatMap_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).FlatMap(null as Func<Task<Result<string>>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void VoidT_FlatMap_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultFlatMapExtensions.FlatMap(null as Task<Result>, () => Task.Run(() => Result.Ok("ok")));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task VoidT_FlatMap_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error(error));
            var executed = false;
            var result = await errResult.FlatMap(() =>
            {
                executed = true;
                return Task.FromResult(Result.Ok("ok"));
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task VoidT_FlatMap_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expectValue = "ok";
            var result = await Task.FromResult(Result.Ok()).FlatMap(() =>
            {
                executed = true;
                return Task.FromResult(Result.Ok(expectValue));
            });
        
            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == expectValue);
        }
        
        //Void -> Void
        
        [Fact]
        public void VoidVoid_FlatMap_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().FlatMap(null as Func<Result>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void VoidVoid_FlatMap_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error(error);
            var executed = false;
            var result = errResult.FlatMap(() =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public void VoidVoid_FlatMap_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = Result.Ok().FlatMap(() =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }

        [Fact]
        public void VoidVoid_FlatMap_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).FlatMap(null as Func<Result>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void VoidVoid_FlatMap_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultFlatMapExtensions.FlatMap(null as Task<Result>, () => Result.Ok());
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task VoidVoid_FlatMap_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error(error));
            var executed = false;
            var result = await errResult.FlatMap(() =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task VoidVoid_FlatMap_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok()).FlatMap(() =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        [Fact]
        public void VoidVoid_FlatMap_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().FlatMap(null as Func<Task<Result>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task VoidVoid_FlatMap_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error(error);
            var executed = false;
            var result = await errResult.FlatMap(() =>
            {
                executed = true;
                return Task.FromResult(Result.Ok());
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task VoidVoid_FlatMap_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Result.Ok().FlatMap(() =>
            {
                executed = true;
                return Task.FromResult(Result.Ok());
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        [Fact]
        public void VoidVoid_FlatMap_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).FlatMap(null as Func<Task<Result>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void VoidVoid_FlatMap_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultFlatMapExtensions.FlatMap(null as Task<Result>, () => Task.Run(() => Result.Ok()));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task VoidVoid_FlatMap_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error(error));
            var executed = false;
            var result = await errResult.FlatMap(() =>
            {
                executed = true;
                return Task.FromResult(Result.Ok());
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task VoidVoid_FlatMap_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok()).FlatMap(() =>
            {
                executed = true;
                return Task.FromResult(Result.Ok());
            });
        
            executed.Should().BeTrue();
            result.Should().BeOk();
        }
    }
}