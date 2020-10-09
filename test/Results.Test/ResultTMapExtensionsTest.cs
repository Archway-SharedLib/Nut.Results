using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Archway.Results.Test.TestUtil;

namespace Archway.Results.Test
{
    public class ResultTMapExtensionsTest
    {
        [Fact]
        public void T_Map_T1T2_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("A").Map(null as Func<string, string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Map_T1T2_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            var result = errResult.Map(_ =>
            {
                executed = true;
                return "Foo";
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public void T_Map_T1T2_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var resultValue = 123;
            var result = Result.Ok("123").Map(_ =>
            {
                executed = true;
                return resultValue;
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == resultValue);
        }
        
        [Fact]
        public void T_Map_T1T2_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).Map(null as Func<string, string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Map_T1T2_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTMapExtensions.Map(null as Task<Result<string>>, _ => "v");
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_Map_T1T2_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            var result = await errResult.Map(_ =>
            {
                executed = true;
                return "Foo";
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task T_Map_T1T2_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var resultValue = 123;
            var result = await Task.FromResult(Result.Ok("123")).Map(_ =>
            {
                executed = true;
                return resultValue;
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == resultValue);
        }
        
        [Fact]
        public void T_Map_T1T2_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("A").Map(null as Func<string, Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task T_Map_T1T2_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            var result = await errResult.Map(_ =>
            {
                executed = true;
                return Task.FromResult("Foo");
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task T_Map_T1T2_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var resultValue = 123;
            var result = await Result.Ok("123").Map(_ =>
            {
                executed = true;
                return Task.FromResult(resultValue);
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == resultValue);
        }
        
        [Fact]
        public void T_Map_T1T2_AsyncAync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).Map(null as Func<string, Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Map_T1T2_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTMapExtensions.Map(null as Task<Result<string>>, _ => Task.FromResult("v"));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_Map_T1T2_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            var result = await errResult.Map(_ =>
            {
                executed = true;
                return Task.FromResult("Foo");
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task T_Map_T1T2_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var resultValue = 123;
            var result = await Task.FromResult(Result.Ok("123")).Map(_ =>
            {
                executed = true;
                return Task.FromResult(resultValue);
            });

            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == resultValue);
        }
    }
}