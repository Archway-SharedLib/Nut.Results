using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nut.Results.Test
{
    public class ResultGetErrorOrExtensionsTest
    {
        //sync - sync
        [Fact]
        public void syncsync_GetErrorOr_ifOkがnullの場合は例外が発生する()
        {
            Action act = () => Result.Ok().GetErrorOr((Func<IError>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void syncsync_GetOrError_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = Result.Error(expected).GetErrorOr(() => new Error());
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void syncsync_GetErrorOr_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var result = Result.Ok().GetErrorOr(() =>
            {
                actionExecuted = true;
                return actionResult;
            });

            actionExecuted.Should().BeTrue();
            result.Should().BeSameAs(actionResult);
        }

        //async - sync
        [Fact]
        public void asyncsync_GetErrorOr_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).GetErrorOr((Func<IError>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void asyncsync_GetErrorOr_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultGetErrorOrExtensions.GetErrorOr((Task<Result>)null, () => new Error());
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task asyncsync_GetOrError_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Task.FromResult(Result.Error(expected)).GetErrorOr(() => new Error());
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task asyncsync_GetErrorOr_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var result = await Task.FromResult(Result.Ok()).GetErrorOr(() =>
            {
                actionExecuted = true;
                return actionResult;
            });

            actionExecuted.Should().BeTrue();
            result.Should().BeSameAs(actionResult);
        }

        //sync - async
        [Fact]
        public void syncasync_GetErrorOr_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().GetErrorOr((Func<Task<IError>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task syncasync_GetOrError_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Result.Error(expected).GetErrorOr(() => Task.FromResult(new Error()));
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task syncasync_GetErrorOr_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var result = await Result.Ok().GetErrorOr(() =>
            {
                actionExecuted = true;
                return Task.FromResult(actionResult);
            });

            actionExecuted.Should().BeTrue();
            result.Should().BeSameAs(actionResult);
        }

        //async - async
        [Fact]
        public void asyncasync_GetErrorOr_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).GetErrorOr((Func<Task<IError>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void asyncasync_GetErrorOr_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultGetErrorOrExtensions.GetErrorOr((Task<Result>)null, () => Task.FromResult(new Error()));
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task asyncasync_GetOrError_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Task.FromResult(Result.Error(expected)).GetErrorOr(() => Task.FromResult(new Error()));
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task asyncasync_GetErrorOr_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var result = await Task.FromResult(Result.Ok()).GetErrorOr(() =>
            {
                actionExecuted = true;
                return Task.FromResult(actionResult);
            });

            actionExecuted.Should().BeTrue();
            result.Should().BeSameAs(actionResult);
        }
    }
}
