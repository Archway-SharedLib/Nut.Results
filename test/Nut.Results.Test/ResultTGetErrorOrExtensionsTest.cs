using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nut.Results.Test
{
    public class ResultTGetErrorOrExtensionsTest
    {
        //sync - sync
        [Fact]
        public void T_syncsync_GetErrorOr_ifOkがnullの場合は例外が発生する()
        {
            Action act = () => Result.Ok("Ok").GetErrorOr((Func<string, IError>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void T_syncsync_GetOrError_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = Result.Error<string>(expected).GetErrorOr(_ => new Error());
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void T_syncsync_GetErrorOr_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var sourceValue = "ok";
            string paramValue = null;
            var result = Result.Ok(sourceValue).GetErrorOr(value =>
            {
                actionExecuted = true;
                paramValue = value;
                return actionResult;
            });

            actionExecuted.Should().BeTrue();
            paramValue.Should().Be(sourceValue);
            result.Should().BeSameAs(actionResult);
        }

        //async - sync
        [Fact]
        public void T_asyncsync_GetErrorOr_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("Ok")).GetErrorOr((Func<string, IError>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void T_asyncsync_GetErrorOr_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultTGetErrorOrExtensions.GetErrorOr((Task<Result<string>>)null, _ => new Error());
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task T_asyncsync_GetOrError_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Task.FromResult(Result.Error<string>(expected)).GetErrorOr(_ => new Error());
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task T_asyncsync_GetErrorOr_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var sourceValue = "ok";
            string paramValue = null;
            var result = await Task.FromResult(Result.Ok(sourceValue)).GetErrorOr(value =>
            {
                actionExecuted = true;
                paramValue = value;
                return actionResult;
            });

            actionExecuted.Should().BeTrue();
            paramValue.Should().Be(sourceValue);
            result.Should().BeSameAs(actionResult);
        }

        //sync - async
        [Fact]
        public void T_syncasync_GetErrorOr_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("Ok").GetErrorOr((Func<string, Task<IError>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task T_syncasync_GetOrError_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Result.Error<string>(expected).GetErrorOr(_ => Task.FromResult(new Error()));
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task T_syncasync_GetErrorOr_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var sourceValue = "ok";
            string paramValue = null;
            var result = await Result.Ok(sourceValue).GetErrorOr(value =>
            {
                actionExecuted = true;
                paramValue = value;
                return Task.FromResult(actionResult);
            });

            actionExecuted.Should().BeTrue();
            paramValue.Should().Be(sourceValue);
            result.Should().BeSameAs(actionResult);
        }

        //async - sync
        [Fact]
        public void T_asyncasync_GetErrorOr_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("Ok")).GetErrorOr((Func<string, Task<IError>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void T_asyncasync_GetErrorOr_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultTGetErrorOrExtensions.GetErrorOr((Task<Result<string>>)null, _ => Task.FromResult(new Error()));
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task T_asyncasync_GetOrError_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Task.FromResult(Result.Error<string>(expected)).GetErrorOr(_ => Task.FromResult(new Error()));
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task T_asyncasync_GetErrorOr_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var sourceValue = "ok";
            string paramValue = null;
            var result = await Task.FromResult(Result.Ok(sourceValue)).GetErrorOr(value =>
            {
                actionExecuted = true;
                paramValue = value;
                return Task.FromResult(actionResult);
            });

            actionExecuted.Should().BeTrue();
            paramValue.Should().Be(sourceValue);
            result.Should().BeSameAs(actionResult);
        }
    }
}
