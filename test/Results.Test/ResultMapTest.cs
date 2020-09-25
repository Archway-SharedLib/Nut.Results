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
    }
}