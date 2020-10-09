using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Archway.Results.Test.TestUtil;

namespace Archway.Results.Test
{
    public class ResultTOmitExtensionsTest
    {
        [Fact]
        public void T_Omit_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("A").Omit(null as Action<string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Omit_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            var result = errResult.Omit(_ =>
            {
                executed = true;
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public void T_Omit_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = Result.Ok("123").Omit(_ =>
            {
                executed = true;
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }

        [Fact]
        public void T_Omit_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).Omit(null as Action<string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Omit_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTOmitExtensions.Omit(null as Task<Result<string>>, _ => { });
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_Omit_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            var result = await errResult.Omit(_ =>
            {
                executed = true;
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task T_Omit_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("123")).Omit(_ =>
            {
                executed = true;
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        [Fact]
        public void T_Omit_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("A").Omit(null as Func<string, Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task T_Omit_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Result.Error<string>(error);
            var executed = false;
            var result = await errResult.Omit(_ =>
            {
                executed = true;
                return Task.Run(() => { });
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task T_Omit_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Result.Ok("123").Omit(_ =>
            {
                executed = true;
                return Task.Run(() => { });
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }
        
        [Fact]
        public void T_Omit_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("A")).Omit(null as Func<string, Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_Omit_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => ResultTOmitExtensions.Omit(null as Task<Result<string>>, _ => Task.Run(() => { }));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
        
        [Fact]
        public async Task T_Omit_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
        {
            var error = new Error();
            var errResult = Task.FromResult(Result.Error<string>(error));
            var executed = false;
            var result = await errResult.Omit(_ =>
            {
                executed = true;
                return Task.Run(() => { });
            });

            executed.Should().BeFalse();
            result.Should().BeError().And.Match(v => v == error);
        }
        
        [Fact]
        public async Task T_Omit_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
        {
            var executed = false;
            var result = await Task.FromResult(Result.Ok("123")).Omit(_ =>
            {
                executed = true;
                return Task.Run(() => { });
            });

            executed.Should().BeTrue();
            result.Should().BeOk();
        }
    }
}