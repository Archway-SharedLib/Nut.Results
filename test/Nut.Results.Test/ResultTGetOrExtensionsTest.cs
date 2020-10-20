using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nut.Results
{
    public class ResultTGetOrExtensionsTest
    {
        //sync - sync
        [Fact]
        public void syncsync_GetOr_ifErrorがnullの場合は例外が発生する()
        {
            Action act = () => Result.Ok("ok").GetOr((Func<IError, string>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void syncsync_GetOr_成功の場合は成功の値が返される()
        {
            var expected = "Success";
            var result = Result.Ok(expected).GetOr(_ => "NG");
            result.Should().Be(expected);
        }

        [Fact]
        public void syncsync_GetOr_失敗の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = "NG";
            var actionExecuted = false;
            var sourceError = new Error();
            IError paramError = null;
            var result = Result.Error<string>(sourceError).GetOr(e =>
            {
                paramError = e;
                actionExecuted = true;
                return actionResult;
            });

            actionExecuted.Should().BeTrue();
            paramError.Should().BeSameAs(sourceError);
            result.Should().Be(actionResult);
        }

        //async - sync
        [Fact]
        public void asyncsync_GetOr_ifErrorがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("ok")).GetOr((Func<IError, string>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void asyncsync_GetOr_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultTGetOrExtensions.GetOr((Task<Result<string>>)null, _ => "");
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task asyncsync_GetOr_成功の場合は成功の値が返される()
        {
            var expected = "Success";
            var result = await Task.FromResult(Result.Ok(expected)).GetOr(_ => "NG");
            result.Should().Be(expected);
        }

        [Fact]
        public async Task asyncsync_GetOr_失敗の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = "NG";
            var actionExecuted = false;
            var sourceError = new Error();
            IError paramError = null;
            var result = await Task.FromResult(Result.Error<string>(sourceError)).GetOr(e =>
            {
                paramError = e;
                actionExecuted = true;
                return actionResult;
            });

            actionExecuted.Should().BeTrue();
            paramError.Should().BeSameAs(sourceError);
            result.Should().Be(actionResult);
        }

        //sync - async
        [Fact]
        public void syncasync_GetOr_ifErrorがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("ok").GetOr((Func<IError, Task<string>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task syncasync_GetOr_成功の場合は成功の値が返される()
        {
            var expected = "Success";
            var result = await Result.Ok(expected).GetOr(_ => Task.FromResult("NG"));
            result.Should().Be(expected);
        }

        [Fact]
        public async Task syncasync_GetOr_失敗の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = "NG";
            var actionExecuted = false;
            var sourceError = new Error();
            IError paramError = null;
            var result = await Result.Error<string>(sourceError).GetOr(e =>
            {
                paramError = e;
                actionExecuted = true;
                return Task.FromResult(actionResult);
            });

            actionExecuted.Should().BeTrue();
            paramError.Should().BeSameAs(sourceError);
            result.Should().Be(actionResult);
        }

        //async - async
        [Fact]
        public void asyncasync_GetOr_ifErrorがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("ok")).GetOr((Func<IError, Task<string>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void asyncasync_GetOr_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultTGetOrExtensions.GetOr((Task<Result<string>>)null, _ => Task.FromResult(""));
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task asyncasync_GetOr_成功の場合は成功の値が返される()
        {
            var expected = "Success";
            var result = await Task.FromResult(Result.Ok(expected)).GetOr(_ => Task.FromResult("NG"));
            result.Should().Be(expected);
        }

        [Fact]
        public async Task asyncasync_GetOr_失敗の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = "NG";
            var actionExecuted = false;
            var sourceError = new Error();
            IError paramError = null;
            var result = await Task.FromResult(Result.Error<string>(sourceError)).GetOr(e =>
            {
                paramError = e;
                actionExecuted = true;
                return Task.FromResult(actionResult);
            });

            actionExecuted.Should().BeTrue();
            paramError.Should().BeSameAs(sourceError);
            result.Should().Be(actionResult);
        }

    }
}
