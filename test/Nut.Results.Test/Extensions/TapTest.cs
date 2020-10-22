using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.Test.TestUtil;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class TapTest
    {
        [Fact]
        public void SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().Tap(null as Action);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public void SyncSync_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
        {
            var expected = Result.Ok();
            var executed = false;
            var result = expected.Tap(() =>
            {
                executed = true;
            });
            executed.Should().BeTrue();
            result.Should().Be(expected).And.BeOk();
        }
        
        [Fact]
        public void SyncSync_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
        {
            var executed = false;
            var expected = Result.Error(new Error());
            var result = expected.Tap(() =>
            {
                executed = true;
            });
            executed.Should().BeFalse();
            result.Should().Be(expected).And.BeError();
        }

        [Fact]
        public void AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).Tap(null as Action);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void AsyncSync_sourceが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Tap(null as Task<Result>, () => { });
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task AsyncSync_成功の場合はokのactionが実行される()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok()).Tap(() =>
            {
                executed = true;
            });
            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        [Fact]
        public async Task AsyncSync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = await Task.FromResult(Result.Error(error)).Tap(() =>
            { 
                executed = true;
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().Tap(null as Func<Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public async Task SyncAsync_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
        {
            var expected = Result.Ok();
            var executed = false;
            var result = await expected.Tap(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeTrue();
            result.Should().Be(expected).And.BeOk();
        }
        
        [Fact]
        public async Task SyncAsync_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
        {
            var executed = false;
            var expected = Result.Error(new Error());
            var result = await expected.Tap(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeFalse();
            result.Should().Be(expected).And.BeError();
        }
        
        [Fact]
        public void AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).Tap(null as Func<Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void AsyncAsync_sourceが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Tap(null as Task<Result>, () => { return Task.Run(() => { }); });
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task AsyncAsync_成功の場合はokのactionが実行される()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok()).Tap(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        [Fact]
        public async Task AsyncAsync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = await Task.FromResult(Result.Error(error)).Tap(() =>
            { 
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
    }
}