using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class T_TapTest
    {
        [Fact]
        public void SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("success").Tap(null as Action<string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public void SyncSync_成功の場合はokのactionが値が渡されて実行され呼び出した値と同じ値が返る()
        {
            var expected = Result.Ok("success");
            var executed = false;
            var value = "";
            var result = expected.Tap((v) =>
            {
                executed = true;
                value = v;
            });
            executed.Should().BeTrue();
            value.Should().Be("success");
            result.Should().Be(expected).And.BeOk();
        }
        
        [Fact]
        public void SyncSync_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
        {
            var executed = false;
            var expected = Result.Error<string>(new Error());
            var result = expected.Tap((v) =>
            {
                executed = true;
            });
            executed.Should().BeFalse();
            result.Should().Be(expected).And.BeError();
        }

        [Fact]
        public void AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("string")).Tap(null as Action<string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void AsyncSync_sourceが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Tap(null as Task<Result<string>>, new Action<string>(_ => { }));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task AsyncSync_成功の場合はokのactionが実行される()
        {
            var executed = false;
            var value = "";
            var result = await Task.FromResult(Result.Ok("success")).Tap(v =>
            {
                value = v;
                executed = true;
            });
            executed.Should().BeTrue();
            value.Should().Be("success");
            result.Should().BeOk().And.Match(v => v == "success");
        }
        
        [Fact]
        public async Task AsyncSync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = await Task.FromResult(Result.Error<string>(error)).Tap(v =>
            { 
                executed = true;
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("success").Tap(null as Func<string, Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public async Task SyncAsync_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
        {
            var expected = Result.Ok("success");
            var executed = false;
            var value = "";
            var result = await expected.Tap(v =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                    value = v;
                });
            });
            value.Should().Be("success");
            executed.Should().BeTrue();
            result.Should().Be(expected).And.BeOk().And.Match(v => v == "success");
        }
        
        [Fact]
        public async Task SyncAsync_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
        {
            var executed = false;
            var expected = Result.Error<string>(new Error());
            var result = await expected.Tap(v =>
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
            Func<Task> act = () => Task.FromResult(Result.Ok("success")).Tap(null as Func<string, Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void AsyncAsync_sourceが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Tap(null as Task<Result<string>>, _ => { return Task.Run(() => { }); });
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task AsyncAsync_成功の場合はokのactionが実行される()
        {
            var executed = false;
            var value = "";
            var result = await Task.FromResult(Result.Ok("success")).Tap(v =>
            {
                return Task.Run(() =>
                {
                    value = v;
                    executed = true;
                });
            });
            value.Should().Be("success");
            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == "success");
        }
        
        [Fact]
        public async Task AsyncAsync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = await Task.FromResult(Result.Error<string>(error)).Tap(_ =>
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