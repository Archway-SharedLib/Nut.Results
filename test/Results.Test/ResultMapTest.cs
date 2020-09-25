using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Archway.Results.Test.TestUtil;

namespace Archway.Results.Test
{
    public class ResultMapTest
    {
        [Fact]
        public void VoidMap_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().Map(null as Action);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public void VoidMap_成功の場合はokのactionが実行される()
        {
            var executed = false;
            var result = Result.Ok().Map(() =>
            {
                executed = true;
            });
            executed.Should().BeTrue();
            result.Should().BeOk();
        }

        [Fact]
        public void VoidMap_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = Result.Error(error).Map(() =>
            {
                executed = true;
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }

        [Fact]
        public void TaskMap_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().Map(null as Func<Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void TaskMap_成功の場合はokのactionが同期的に実行される()
        {
            var executed = false;
            var result = Result.Ok().Map(() =>
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
        public void TaskMap_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = Result.Error(error).Map(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void ReturnMap_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().Map(null as Func<string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void ReturnMap_成功の場合はokのactionが同期的に実行される()
        {
            var executed = false;
            var result = Result.Ok().Map(() =>
            {
                executed = true;
                return "ok";
            });
            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void ReturnMap_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = Result.Error(error).Map(() =>
            {
                executed = true;
                return "ok";
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void TaskReturnMap_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().Map(null as Func<Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void TaskReturnMap_成功の場合はokのactionが同期的に実行される()
        {
            var executed = false;
            var result = Result.Ok().Map(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                    return "ok";
                });
            });
            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void TaskReturnMap_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = Result.Error(error).Map(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                    return "ok";
                });
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void VoidMapAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().MapAsync(null as Func<Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task VoidMapAsync_成功の場合はokのactionが実行される()
        {
            var executed = false;
            var result = await Result.Ok().MapAsync(() =>
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
        public async Task VoidMapAsync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = await Result.Error(error).MapAsync(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void ReturnMapAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().MapAsync(null as Func<Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task ReturnMapAsync_成功の場合はokのactionが実行される()
        {
            var executed = false;
            var result = await Result.Ok().MapAsync(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                    return "ok";
                });
            });
            executed.Should().BeTrue();
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public async Task ReturnMapAsync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = await Result.Error(error).MapAsync(() =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                    return "ok";
                });
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void T_ReturnMap_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("success").Map(null as Func<string, string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_ReturnMap_成功の場合はokのactionが実行され引数に値が渡される()
        {
            var executed = false;
            var paramValue = "";
            Result.Ok("success").Map(v =>
            {
                paramValue = v;
                executed = true;
                return "ok";
            });
            executed.Should().BeTrue();
        }
        
        [Fact]
        public void T_ReturnMap_成功の場合はokのactionの結果が成功として返される()
        {
            var result = Result.Ok("success").Map(_ =>
            {
                return "ok";
            });
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void T_ReturnMap_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = Result.Error<string>(error).Map(v =>
            {
                executed = true;
                return "ok";
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void T_TaskReturnMap_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("success").Map(null as Func<string, Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_TaskReturnMap_成功の場合はokのactionが実行され引数に値が渡される()
        {
            var paramValue = "";
            var executed = false;
            Result.Ok("success").Map(v =>
            {
                paramValue = v;
                executed = true;
                return Task.FromResult("ok");
            });
            paramValue.Should().Be("success");
            executed.Should().BeTrue();
        }
        
        [Fact]
        public void T_TaskReturnMap_成功の場合はokのactionの結果が成功として返される()
        {
            var result = Result.Ok("success").Map(v =>
            {
                return Task.FromResult("ok");
            });
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public void T_TaskReturnMap_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = Result.Error<string>(error).Map(v =>
            {
                executed = true;
                return Task.FromResult("ok");
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void T_VoidMap_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("success").Map(null as Action<string>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_VoidMap_成功の場合はokのactionが実行され引数に値が渡される()
        {
            var paramValue = "";
            var executed = false;
            Result.Ok("success").Map(v =>
            {
                paramValue = v;
                executed = true;
            });
            paramValue.Should().Be("success");
            executed.Should().BeTrue();
        }
        
        [Fact]
        public void T_VoidMap_成功の場合はokのactionの結果が成功として返される()
        {
            var result = Result.Ok("success").Map(_ =>
            {
            });
            result.Should().BeOk();
        }
        
        [Fact]
        public void T_VoidMap_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = Result.Error<string>(error).Map(v =>
            {
                executed = true;
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void T_TaskMap_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok("success").Map(null as Func<string, Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public void T_TaskMap_成功の場合はokのactionが実行され引数に値が渡される()
        {
            var paramValue = "";
            var executed = false;
            Result.Ok("success").Map(v =>
            {
                return Task.Run(() =>
                {
                    paramValue = v;
                    executed = true;
                });
                
            });
            paramValue.Should().Be("success");
            executed.Should().BeTrue();
        }
        
        [Fact]
        public void T_TaskMap_成功の場合はokのactionの結果が成功として返される()
        {
            var result = Result.Ok("success").Map(_ =>
            {
                return Task.Run(() => { });
            });
            result.Should().BeOk();
        }
        
        [Fact]
        public void T_TaskMap_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = Result.Error<string>(error).Map(v =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                });
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void T_ReturnMapAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("success").MapAsync(null as Func<string, Task<string>>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task T_ReturnMapAsync_成功の場合はokのactionが実行され引数に値が渡される()
        {
            var paramValue = "";
            var executed = false;
            await Result.Ok("success").MapAsync(v =>
            {
                return Task.Run(() =>
                {
                    paramValue = v;
                    executed = true;
                    return "ok";
                });
                
            });
            paramValue.Should().Be("success");
            executed.Should().BeTrue();
        }
        
        [Fact]
        public async Task T_ReturnMapAsync_成功の場合はokのactionの結果が成功として返される()
        {
            var result = await Result.Ok("success").MapAsync(_ =>
            {
                return Task.Run(() => "ok");
            });
            result.Should().BeOk().And.Match(v => v == "ok");
        }
        
        [Fact]
        public async Task T_ReturnMapAsync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = await Result.Error<string>(error).MapAsync(v =>
            {
                return Task.Run(() =>
                {
                    executed = true;
                    return "ok";
                });
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e == error);
        }
        
        [Fact]
        public void T_VoidMapAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok("success").MapAsync(null as Func<string, Task>);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }
        
        [Fact]
        public async Task T_VoidMapAsync_成功の場合はokのactionが実行され引数に値が渡される()
        {
            var paramValue = "";
            var executed = false;
            await Result.Ok("success").MapAsync(v =>
            {
                return Task.Run(() =>
                {
                    paramValue = v;
                    executed = true;
                });
                
            });
            paramValue.Should().Be("success");
            executed.Should().BeTrue();
        }
        
        [Fact]
        public async Task T_VoidMapAsync_成功の場合はokのactionの結果が成功として返される()
        {
            var result = await Result.Ok("success").MapAsync(_ =>
            {
                return Task.Run(() => { });
            });
            result.Should().BeOk();
        }
        
        [Fact]
        public async Task T_VoidMapAsync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
        {
            var executed = false;
            var error = new Error();
            var result = await Result.Error<string>(error).MapAsync(v =>
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