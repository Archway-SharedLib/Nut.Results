using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.Test.TestUtil;

namespace Nut.Results.Test
{
    public class FlatMapErrorTest
    {
        //sync -> sync
        
        [Fact]
        public void SyncSync_errorパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Error(new Error()).FlatMapError(null as Func<IError, Result>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public void SyncSync_成功の場合は同じActionは実行されず成功の値が返る()
        {
            var errResult = Result.Ok();
            var executed = false;
            var result = errResult.FlatMapError(e =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeFalse();
            result.Should().BeOk();
        }
        
        [Fact]
        public void SyncSync_失敗の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expected = new Error();
            IError param = null;
            var result = Result.Error(expected).FlatMapError(e =>
            {
                executed = true;
                param = e;
                return Result.Ok();
            });

            executed.Should().BeTrue();
            param.Should().Be(expected);
            result.Should().BeOk();
        }

        //async -> sync

        [Fact]
        public void AsyncSync_errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).FlatMapError(null as Func<IError,Result>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public void AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.FlatMapError(null as Task<Result>, e => Result.Ok());
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task AsyncSync_失敗の場合はActionが実行され値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error(error));
            var executed = false;
            IError param = null;
            var result = await errResult.FlatMapError(e =>
            {
                executed = true;
                param = e;
                return Result.Ok();
            });

            executed.Should().BeTrue();
            param.Should().Be(error);
            result.Should().BeOk();
        }
        
        [Fact]
        public async Task AsyncSync_成功の場合はアクションが実行されず結果が成功で返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok()).FlatMapError(e =>
            {
                executed = true;
                return Result.Ok();
            });

            executed.Should().BeFalse();
            result.Should().BeOk();
        }

        //sync -> async

        [Fact]
        public void SyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().FlatMapError(null as Func<IError, Task<Result>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public async Task SyncAsync_失敗の場合はActionが実行され結果が返る()
        {
            var error = new Error();
            var errResult = Result.Error(error);
            var executed = false;
            IError param = null;
            var result = await errResult.FlatMapError(e =>
            {
                executed = true;
                param = e;
                return Task.FromResult(Result.Ok());
            });

            executed.Should().BeTrue();
            param.Should().Be(error);
            result.Should().BeOk();
        }
        
        [Fact]
        public async Task SyncAsync_成功の場合はアクションが実行されず成功のResultで返る()
        {
            var executed = false;
            var result = await Result.Ok().FlatMapError(e =>
            {
                executed = true;
                return Task.FromResult(Result.Ok());
            });

            executed.Should().BeFalse();
            result.Should().BeOk();
        }
        
        // async -> async

        [Fact]
        public void AsyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).FlatMapError(null as Func<IError, Task<Result>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public void AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.FlatMapError(null as Task<Result>, e => Task.Run(() => Result.Ok()));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task AsyncAsync_失敗の場合はActionが実行され値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error(error));
            var executed = false;
            IError param = null;
            var result = await errResult.FlatMapError(e =>
            {
                executed = true;
                param = e;
                return Task.FromResult(Result.Ok());
            });

            executed.Should().BeTrue();
            param.Should().Be(error);
            result.Should().BeOk();
        }
        
        [Fact]
        public async Task AsyncAsync_成功の場合はアクションが実行されず結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok()).FlatMapError(e =>
            {
                executed = true;
                return Task.FromResult(Result.Ok());
            });
        
            executed.Should().BeFalse();
            result.Should().BeOk();
        }
    }
}