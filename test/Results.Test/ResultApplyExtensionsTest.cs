using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Archway.Results.Test.TestUtil;

namespace Archway.Results.Test
{
    public class ResultApplyExtensionsTest
    {
        [Fact]
        public void Apply_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().Apply(null as Func<string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void Apply_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error(error);
            var executed = false;
            var result = errResult.Apply(() =>
            {
                executed = true;
                return "value";
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public void Apply_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expectedValue = "ok";
            var result = Result.Ok().Apply(() =>
            {
                executed = true;
                return expectedValue;
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == expectedValue);
        }

        [Fact]
        public void Apply_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).Apply(null as Func<string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void Apply_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultApplyExtensions.Apply(null as Task<Result>, () => "ok");
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task Apply_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error(error));
            var executed = false;
            var result = await errResult.Apply(() =>
            {
                executed = true;
                return "ok";
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task Apply_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expectedValue = "ok";
            var result = await Task.FromResult(Result.Ok()).Apply(() =>
            {
                executed = true;
                return expectedValue;
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == expectedValue);
        }
        
        [Fact]
        public void Apply_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().Apply(null as Func<Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task Apply_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error(error);
            var executed = false;
            var result = await errResult.Apply(() =>
            {
                executed = true;
                return Task.FromResult("ok");
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task Apply_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expectValue = "ok";
            var result = await Result.Ok().Apply(() =>
            {
                executed = true;
                return Task.FromResult(expectValue);
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == expectValue);
        }
        
        [Fact]
        public void Apply_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).Apply(null as Func<Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void Apply_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultApplyExtensions.Apply(null as Task<Result>, () => Task.Run(() => "ok"));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task Apply_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error(error));
            var executed = false;
            var result = await errResult.Apply(() =>
            {
                executed = true;
                return Task.FromResult("ok");
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task Apply_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var expectValue = "ok";
            var result = await Task.FromResult(Result.Ok()).Apply(() =>
            {
                executed = true;
                return Task.FromResult(expectValue);
            });
        
            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == expectValue);
        }
    }
}