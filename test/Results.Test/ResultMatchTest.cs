using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Archway.Results.Test
{
    public class ResultMatchTest
    {
        [Fact]
        public void VoidMatch_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().Match(null, _ => { });
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public void VoidMatch_errorパラメーターが指定されていあに場合は例外が発生する()
        {
            Action act = () => Result.Ok().Match(() => { }, null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }

        [Fact]
        public void VoidMatch_成功のときはokのactionが実行される()
        {
            var okExecuted = false;
            var errorExecuted = false;
            Result.Ok().Match(
                ok: () => { okExecuted = true; },
                error: _ => { errorExecuted = true; });
            okExecuted.Should().BeTrue();
            errorExecuted.Should().BeFalse();
        }

        [Fact]
        public void VoidMatch_失敗のときはerrorのactionが実行される()
        {
            var okExecuted = false;
            var errorExecuted = false;
            Result.Error(new Error()).Match(
                ok: () => { okExecuted = true; },
                error: _ => { errorExecuted = true; });
            okExecuted.Should().BeFalse();
            errorExecuted.Should().BeTrue();
        }

        [Fact]
        public void VoidMatch_失敗のときはerrorのactionにエラーの値が渡される()
        {
            var error = new Error();
            Result.Error(error).Match(
                ok: () => { },
                error: err => {
                    err.Should().Be(error);
                });
        }

        [Fact]
        public void ReturnMatch_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Action act = () => Result.Ok().Match(null, _ => "error");
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public void ReturnMatch_errorパラメーターが指定されていあに場合は例外が発生する()
        {
            Action act = () => Result.Ok().Match(() => "ok", null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }

        [Fact]
        public void ReturnMatch_成功のときはokのactionが実行される()
        {
            var okExecuted = false;
            var errorExecuted = false;
            var matchResult = Result.Ok().Match(
                ok: () => { okExecuted = true; return "ok";  },
                error: _ => { errorExecuted = true; return "error";  });
            okExecuted.Should().BeTrue();
            errorExecuted.Should().BeFalse();
            matchResult.Should().Be("ok");
        }

        [Fact]
        public void Returnatch_失敗のときはerrorのactionが実行される()
        {
            var okExecuted = false;
            var errorExecuted = false;
            var matchResult = Result.Error(new Error()).Match(
                ok: () => { okExecuted = true; return "ok"; },
                error: _ => { errorExecuted = true; return "error"; });
            okExecuted.Should().BeFalse();
            errorExecuted.Should().BeTrue();
            matchResult.Should().Be("error");
        }

        [Fact]
        public void ReturnMatch_失敗のときはerrorのactionにエラーの値が渡される()
        {
            var error = new Error();
            Result.Error(error).Match(
                ok: () => "ok",
                error: err => {
                    err.Should().Be(error);
                    return "error";
                });
        }

        [Fact]
        public void VoidMatchAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().MatchAsync(null, _ => Task.Run(() => { }));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public void VoidMatchAsync_errorパラメーターが指定されていあに場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().MatchAsync(() => Task.Run(() => { }), null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }

        [Fact]
        public async Task VoidMatchAsync_成功のときはokのactionが実行される()
        {
            var okExecuted = false;
            var errorExecuted = false;
            await Result.Ok().MatchAsync(
                ok: () => Task.Run(() => { okExecuted = true; }),
                error: _ => Task.Run(() => { errorExecuted = true; }));
            okExecuted.Should().BeTrue();
            errorExecuted.Should().BeFalse();
        }

        [Fact]
        public async Task VoidMatchAsync_失敗のときはerrorのactionが実行される()
        {
            var okExecuted = false;
            var errorExecuted = false;
            await Result.Error(new Error()).MatchAsync(
                ok: () => Task.Run(() => { okExecuted = true; }),
                error: _ => Task.Run(() => { errorExecuted = true; }));
            okExecuted.Should().BeFalse();
            errorExecuted.Should().BeTrue();
        }

        [Fact]
        public async Task VoidMatchAsync_失敗のときはerrorのactionにエラーの値が渡される()
        {
            var error = new Error();
            await Result.Error(error).MatchAsync(
                ok: () => Task.Run(() => { }),
                error: err => Task.Run(() => { err.Should().Be(error); }));
        }

        [Fact]
        public void ReturnMatchAsync_Okパラメーターが指定されていない場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().MatchAsync(null, _ => Task.Run(() => Task.Run(() => "error")));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
        }

        [Fact]
        public void ReturnMatchAsync_errorパラメーターが指定されていあに場合は例外が発生する()
        {
            Action act = () => Result.Ok().MatchAsync(() => Task.Run(() => Task.Run(() => "ok")), null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
        }

        [Fact]
        public async Task ReturnMatchAsync_成功のときはokのactionが実行される()
        {
            var okExecuted = false;
            var errorExecuted = false;
            var matchResult = await Result.Ok().MatchAsync(
                ok: () => Task.Run(() => { okExecuted = true; return "ok"; }),
                error: _ => Task.Run(() => { errorExecuted = true; return "error"; }));
            okExecuted.Should().BeTrue();
            errorExecuted.Should().BeFalse();
            matchResult.Should().Be("ok");
        }

        [Fact]
        public async Task ReturnMatchAsync_失敗のときはerrorのactionが実行される()
        {
            var okExecuted = false;
            var errorExecuted = false;
            var matchResult = await Result.Error(new Error()).MatchAsync(
                ok: () => Task.Run(() => { okExecuted = true; return "ok"; }),
                error: _ => Task.Run(() => { errorExecuted = true; return "error"; }));
            okExecuted.Should().BeFalse();
            errorExecuted.Should().BeTrue();
            matchResult.Should().Be("error");
        }

        [Fact]
        public async Task ReturnMatchAsync_失敗のときはerrorのactionにエラーの値が渡される()
        {
            var error = new Error();
            await Result.Error(error).MatchAsync(
                ok: () => Task.Run(() => "ok"),
                error: err => Task.Run(() => { err.Should().Be(error); return "error"; }));
        }
    }
}
