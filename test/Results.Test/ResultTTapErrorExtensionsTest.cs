using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Archway.Results.Test.TestUtil;

namespace Archway.Results.Test
{
    public class ResultTTapErrorExtensionsTest
    {
        [Fact]
        public void T_TapError_SyncSync_Errorパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("success").TapError(null as Action<IError>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }

        [Fact]
        public void T_TapError_SyncSync_失敗の場合はerrorのactionが実行され呼び出した値と同じ値が返る()
        {
            var expectedError = new Error();
            var expected = Result.Error<string>(expectedError);
            var executed = false;
            IError error = null;
            var result = expected.TapError(e =>
            {
                error = e;
                executed = true;
            });
            executed.Should().BeTrue();
            expectedError.Should().Be(error);
            result.Should().Be(expected).And.BeError();
        }
        
        [Fact]
        public void T_TapError_SyncSync_成功の場合はerrorのactionは実行されず呼び出した値と同じ値が返る()
        {
            var executed = false;
            var expected = Result.Ok("success");
            var result = expected.TapError(_ =>
            {
                executed = true;
            });
            executed.Should().BeFalse();
            result.Should().BeOk();
        }
        
        [Fact]
        public void T_TapError_AsyncSync_Errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("success")).TapError(null as Action<IError>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public void T_TapError_AsyncSync_sourceが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTTapErrorExtensions.TapError(null as Task<Result<string>>, _ => {});
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_TapError_AsyncSync_失敗の場合はerrorのactionが実行される呼び出した値と同じ値が返る()
        {
            var expectedError = new Error();
            var expected = Result.Error<string>(expectedError);
            var executed = false;
            IError error = null;
            var result = await Task.FromResult(expected).TapError(e =>
            {
                error = e;
                executed = true;
            });
            executed.Should().BeTrue();
            expectedError.Should().Be(error);
            result.Should().Be(expected).And.BeError();
        }
        
        [Fact]
        public async Task T_TapError_AsyncSync_成功の場合はerrorのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("success")).TapError(_ =>
            { 
                executed = true;
            });
            executed.Should().BeFalse();
            result.Should().BeOk();
        }
        
        [Fact]
        public void T_TapError_SyncAsync_Errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("success").TapError(null as Func<IError, Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public async Task T_TapError_SyncAsync_失敗の場合はerrorのactionが実行される呼び出した値と同じ値が返る()
        {
            var expectedError = new Error();
            var expected = Result.Error<string>(expectedError);
            var executed = false;
            IError error = null;
            var result = await expected.TapError(e =>
            {
                return Task.Run(() =>
                {
                    error = e;
                    executed = true;
                });
            });
            executed.Should().BeTrue();
            expectedError.Should().Be(error);
            result.Should().Be(expected).And.BeError();
        }
        
        [Fact]
        public async Task T_TapError_SyncAsync_成功の場合はerrorのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var result = await Result.Ok("success").TapError(_ =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeFalse();
            result.Should().BeOk();
        }
        
        
        [Fact]
        public void T_TapError_AsyncAsync_Errorパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("success")).TapError(null as Func<IError, Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }
        
        [Fact]
        public void T_TapError_AsyncAsync_sourceが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTTapErrorExtensions.TapError(null as Task<Result<string>>, _ => Task.Run(() => { }));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_TapError_AsyncAsync_失敗の場合はerrorのactionが実行される呼び出した値と同じ値が返る()
        {
            var expectedError = new Error();
            var expected = Result.Error<string>(expectedError);
            var executed = false;
            IError error = null;
            var result = await Task.FromResult(expected).TapError(e =>
            {
                return Task.Run(() =>
                {
                    error = e;
                    executed = true;
                });
            });
            executed.Should().BeTrue();
            expectedError.Should().Be(error);
            result.Should().Be(expected).And.BeError();
        }
        
        [Fact]
        public async Task T_TapError_AsyncAsync__成功の場合はerrorのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("success")).TapError(_ =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeFalse();
            result.Should().BeOk();
        }
    }
}