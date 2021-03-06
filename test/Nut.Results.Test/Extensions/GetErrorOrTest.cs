﻿using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class GetErrorOrTest
    {
        //sync - sync
        [Fact]
        public void SyncSync_ifOkがnullの場合は例外が発生する()
        {
            Action act = () => Result.Ok().GetErrorOr((Func<IError>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SyncSync_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = Result.Error(expected).GetErrorOr(() => new Error());
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void SyncSync_成功の場合はアクションが実行されてその結果が返る()
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
        public void AsyncSync_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().AsTask().GetErrorOr((Func<IError>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AsyncSync_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.GetErrorOr((Task<Result>)null, () => new Error());
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task AsyncSync_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Result.Error(expected).AsTask().GetErrorOr(() => new Error());
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task AsyncSync_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var result = await Result.Ok().AsTask().GetErrorOr(() =>
            {
                actionExecuted = true;
                return actionResult;
            });

            actionExecuted.Should().BeTrue();
            result.Should().BeSameAs(actionResult);
        }

        //sync - async
        [Fact]
        public void SyncAsync_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().GetErrorOr((Func<Task<IError>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task SyncAsync_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Result.Error(expected).GetErrorOr(() => Task.FromResult(new Error()));
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task SyncAsync_成功の場合はアクションが実行されてその結果が返る()
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
        public void AsyncAsync_ifOkがnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().AsTask().GetErrorOr((Func<Task<IError>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AsyncAsync_sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.GetErrorOr((Task<Result>)null, () => Task.FromResult(new Error()));
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task AsyncAsync_失敗の場合は失敗の値が返される()
        {
            var expected = new Error();
            var result = await Result.Error(expected).AsTask().GetErrorOr(() => Task.FromResult(new Error()));
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task AsyncAsync_成功の場合はアクションが実行されてその結果が返る()
        {
            var actionResult = new Error();
            var actionExecuted = false;
            var result = await Result.Ok().AsTask().GetErrorOr(() =>
            {
                actionExecuted = true;
                return Task.FromResult(actionResult);
            });

            actionExecuted.Should().BeTrue();
            result.Should().BeSameAs(actionResult);
        }
    }
}
