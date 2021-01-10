using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class T_GetOrTest
    {
        //sync - sync
        [Fact]
        public void SyncSync_ifErrorがnullの場合は例外が発生する()
        {
            Action act = () => Result.Ok("ok").GetOr((Func<IError, string>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SyncSync_成功の場合は成功の値が返される()
        {
            var expected = "Success";
            var result = Result.Ok(expected).GetOr(_ => "NG");
            result.Should().Be(expected);
        }

        [Fact]
        public void SyncSync_失敗の場合はアクションが実行されてその結果が返る()
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
        public void AsyncSync_ifErrorがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("ok").AsTask().GetOr((Func<IError, string>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AsyncSync_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.GetOr((Task<Result<string>>)null, _ => "");
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task AsyncSync_成功の場合は成功の値が返される()
        {
            var expected = "Success";
            var result = await Result.Ok(expected).AsTask().GetOr(_ => "NG");
            result.Should().Be(expected);
        }

        [Fact]
        public async Task AsyncSync_失敗の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = "NG";
            var actionExecuted = false;
            var sourceError = new Error();
            IError paramError = null;
            var result = await Result.Error<string>(sourceError).AsTask().GetOr(e =>
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
        public void SyncAsync_ifErrorがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("ok").GetOr((Func<IError, Task<string>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task SyncAsync_成功の場合は成功の値が返される()
        {
            var expected = "Success";
            var result = await Result.Ok(expected).GetOr(_ => Task.FromResult("NG"));
            result.Should().Be(expected);
        }

        [Fact]
        public async Task SyncAsync_失敗の場合はアクションが実行されてその結果が返る()
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
        public void AsyncAsync_ifErrorがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("ok").AsTask().GetOr((Func<IError, Task<string>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AsyncAsync_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.GetOr((Task<Result<string>>)null, _ => Task.FromResult(""));
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task AsyncAsync_成功の場合は成功の値が返される()
        {
            var expected = "Success";
            var result = await Result.Ok(expected).AsTask().GetOr(_ => Task.FromResult("NG"));
            result.Should().Be(expected);
        }

        [Fact]
        public async Task AsyncAsync_失敗の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = "NG";
            var actionExecuted = false;
            var sourceError = new Error();
            IError paramError = null;
            var result = await Result.Error<string>(sourceError).AsTask().GetOr(e =>
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
