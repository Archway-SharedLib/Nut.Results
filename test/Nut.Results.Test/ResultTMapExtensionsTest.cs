using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.Test.TestUtil;

namespace Nut.Results.Test
{
    public class ResultTMapExtensionsTest
    {
        [Fact]
        public void T_Map_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("A").Map(null as Func<string, string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Map_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
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
        public void T_Map_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
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
        public void T_Map_SyncSync_戻り値がnullの場合は例外が発生する()
        {
            Action act = () => Result.Ok("ok").Map(_ => null as string);
            act.Should().Throw<InvalidReturnValueException>();
        }
        
        [Fact]
        public void T_Map_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).Map(null as Func<string, string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Map_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTMapExtensions.Map(null as Task<Result<string>>, _ => "v");
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_Map_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
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
        public async Task T_Map_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
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
        public void T_Map_AsyncSync_戻り値がnullの場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("ok")).Map(_ => null as string);
            act.Should().Throw<InvalidReturnValueException>();
        }
        
        [Fact]
        public void T_Map_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("A").Map(null as Func<string, Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task T_Map_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
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
        public async Task T_Map_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
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
        public void T_Map_SyncAsync_戻り値がnullの場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("ok").Map(_ => Task.FromResult(null as string));
            act.Should().Throw<InvalidReturnValueException>();
        }
        
        [Fact]
        public void T_Map_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).Map(null as Func<string, Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Map_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTMapExtensions.Map(null as Task<Result<string>>, _ => Task.FromResult("v"));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_Map_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
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
        public async Task T_Map_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
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
        
        [Fact]
        public void T_Map_AsyncAsync_戻り値がnullの場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("ok")).Map(_ => Task.FromResult(null as string));
            act.Should().Throw<InvalidReturnValueException>();
        }
    }
}