using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Archway.Results.Test.TestUtil;

namespace Archway.Results.Test
{
    public class ResultTFlatMapErrorExtensionsTest
    {
        //T -> T
        
        [Fact]
        public void T_FlatMapError_SyncSync_errorパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Error<string>(new Error()).FlatMapError(null as Func<IError, Result<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public void T_FlatMapError_SyncSync_成功の場合は同じActionは実行されず成功の値が返る()
        {
            var errResult = Result.Ok("ok");
            var executed = false;
            var result = errResult.FlatMapError(e =>
            {
                executed = true;
                return Result.Ok("success");
            });

            executed.Should().BeFalse();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void T_FlatMapError_SyncSync_失敗の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expected = new Error();
            IError param = null;
            var result = Result.Error<string>(expected).FlatMapError(e =>
            {
                executed = true;
                param = e;
                return Result.Ok("success");
            });

            executed.Should().BeTrue();
            param.Should().Be(expected);
            result.Should().BeOk().And.Match(v => v == "success");
        }

        [Fact]
        public void T_FlatMapError_AsyncSync_errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("ok")).FlatMapError(null as Func<IError,Result<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public void T_FlatMapError_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTFlatMapErrorExtensions.FlatMapError(null as Task<Result<string>>, e => Result.Ok("ok"));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_FlatMapError_AsyncSync_失敗の場合はActionが実行され値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            IError param = null;
            var result = await errResult.FlatMapError(e =>
            {
                executed = true;
                param = e;
                return Result.Ok("success");
            });

            executed.Should().BeTrue();
            param.Should().Be(error);
            result.Should().BeOk().And.Match(v => v == "success");
        }
        
        [Fact]
        public async Task T_FlatMapError_AsyncSync_成功の場合はアクションが実行されず結果が成功で返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("ok")).FlatMapError(e =>
            {
                executed = true;
                return Result.Ok("success");
            });

            executed.Should().BeFalse();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void T_FlatMapError_SyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("ok").FlatMapError(null as Func<IError, Task<Result<string>>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public async Task T_FlatMapError_SyncAsync_失敗の場合はActionが実行され結果が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            IError param = null;
            var result = await errResult.FlatMapError(e =>
            {
                executed = true;
                param = e;
                return Task.FromResult(Result.Ok("success"));
            });

            executed.Should().BeTrue();
            param.Should().Be(error);
            result.Should().BeOk().And.Match(v => v == "success");
        }
        
        [Fact]
        public async Task T_FlatMapError_SyncAsync_成功の場合はアクションが実行されず成功のResultで返る()
        {
            var executed = false;
            var result = await Result.Ok("ok").FlatMapError(e =>
            {
                executed = true;
                return Task.FromResult(Result.Ok("success"));
            });

            executed.Should().BeFalse();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void T_FlatMapError_AsyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("ok")).FlatMapError(null as Func<IError, Task<Result<string>>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public void T_FlatMapError_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTFlatMapErrorExtensions.FlatMapError(null as Task<Result<string>>, e => Task.Run(() => Result.Ok("ok")));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_FlatMapError_AsyncAsync_失敗の場合はActionが実行され値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            IError param = null;
            var result = await errResult.FlatMapError(e =>
            {
                executed = true;
                param = e;
                return Task.FromResult(Result.Ok("success"));
            });

            executed.Should().BeTrue();
            param.Should().Be(error);
            result.Should().BeOk().And.Match(v => v == "success");
        }
        
        [Fact]
        public async Task T_FlatMapError_AsyncAsync_成功の場合はアクションが実行されず結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("ok")).FlatMapError(e =>
            {
                executed = true;
                return Task.FromResult(Result.Ok("success"));
            });
        
            executed.Should().BeFalse();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
    }
}