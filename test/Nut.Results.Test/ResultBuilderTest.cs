using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;
using System.Threading.Tasks;
using System;

namespace Nut.Results.Test
{
    public class ResultBuilderTest
    {
        [Fact]
        public void Ok_成功の結果が返る()
        {
            var r = Result.Ok();
            r.IsOk.Should().BeTrue();
            r.IsError.Should().BeFalse();
        }

        [Fact]
        public void Error_失敗の結果が返る()
        {
            var r = Result.Error(new Error("message"));
            r.IsOk.Should().BeFalse();
            r.IsError.Should().BeTrue();
        }

        [Fact]
        public void Error_メッセージで失敗の結果が返る()
        {
            var r = Result.Error("message");
            r.IsOk.Should().BeFalse();
            r.IsError.Should().BeTrue();
            r.Should().BeError().And.Match(e => e is Error).And.Match(e => e.Message == "message");
        }

        [Fact]
        public void T_Ok_成功の結果が返る()
        {
            var r = Result.Ok("Ok Message");
            r.IsOk.Should().BeTrue();
            r.IsError.Should().BeFalse();
        }

        [Fact]
        public void T_Error_失敗の結果が返る()
        {
            var r = Result.Error<string>(new Error("message"));
            r.IsOk.Should().BeFalse();
            r.IsError.Should().BeTrue();
        }

        [Fact]
        public void T_Error_メッセージで失敗の結果が返る()
        {
            var r = Result.Error<string>("message");
            r.IsOk.Should().BeFalse();
            r.IsError.Should().BeTrue();
            r.Should().BeError().And.Match(e => e is Error).And.Match(e => e.Message == "message");
        }

        // Try

        [Fact]
        public void NoReturn_Sync_例外が発生しない場合は成功が返る()
            => Result.Try(() => { }).Should().BeOk();

        [Fact]
        public void NoReturn_Sync_例外が発生した場合は失敗が返る()
            => Result.Try(() => RaiseException("Failed")).Should().BeError().And.WithMessage("Failed");

        [Fact]
        public async Task NoReturn_Async_例外が発生しない場合は成功が返る()
        {
            var result = Result.Try(() => Task.Run(() => { }));
            result.Should().BeAssignableTo<Task<Result>>();
            (await result.ConfigureAwait(false)).Should().BeOk();
        }

        [Fact]
        public async Task NoReturn_Async_例外が発生した場合は失敗が返る()
        {
            var result = Result.Try(() => Task.Run(() => RaiseException("Failed")));
            result.Should().BeAssignableTo<Task<Result>>();
            (await result.ConfigureAwait(false)).Should().BeError().And.WithMessage("Failed");
        }

        [Fact]
        public void T_Sync_例外が発生しない場合は成功が返る()
            => Result.Try(() => "Good").Should().BeOk().And.Match(v => v == "Good");

        [Fact]
        public void T_Sync_例外が発生した場合は失敗が返る()
            => Result.Try(() => RaiseException<string>("Failed")).Should().BeError().And.WithMessage("Failed");

        [Fact]
        public async Task T_Async_例外が発生しない場合は成功が返る()
        {
            var result = Result.Try(() => Task.Run(() => "Good"));
            result.Should().BeAssignableTo<Task<Result<string>>>();
            (await result.ConfigureAwait(false)).Should().BeOk().And.Match(v => v == "Good");
        }

        [Fact]
        public async Task T_Async_例外が発生した場合は失敗が返る()
        {
            var result = Result.Try(() => Task.Run(() => RaiseException<string>("Failed")));
            result.Should().BeAssignableTo<Task<Result<string>>>();
            (await result.ConfigureAwait(false)).Should().BeError().And.WithMessage("Failed");
        }

        [Fact]
        public void Result_Sync_例外が発生しない場合は結果が返る()
        {
            Result.Try(() => Result.Ok()).Should().BeOk();
            Result.Try(() => Result.Error("Error")).Should().BeError().And.WithMessage("Error");
        }

        [Fact]
        public void Result_Sync_例外が発生した場合は失敗が返る()
            => Result.Try(() => RaiseException<Result>("Failed")).Should().BeError().And.WithMessage("Failed");

        [Fact]
        public async Task Result_Async_例外が発生しない場合は成功が返る()
        {
            var result = Result.Try(() => Task.Run(() => Result.Ok()));
            result.Should().BeAssignableTo<Task<Result>>();
            (await result.ConfigureAwait(false)).Should().BeOk();
        }

        [Fact]
        public async Task Result_Async_例外が発生した場合は失敗が返る()
        {
            var result = Result.Try(() => Task.Run(() => RaiseException<Result>("Failed")));
            result.Should().BeAssignableTo<Task<Result>>();
            (await result.ConfigureAwait(false)).Should().BeError().And.WithMessage("Failed");
        }

        [Fact]
        public void ResultT_Sync_例外が発生しない場合は結果が返る()
        {
            Result.Try(() => Result.Ok("Good")).Should().BeOk().And.Match(v => v == "Good");
            Result.Try(() => Result.Error<string>("Error")).Should().BeError().And.WithMessage("Error");
        }

        [Fact]
        public void ResultT_Sync_例外が発生した場合は失敗が返る()
            => Result.Try(() => RaiseException<Result<string>>("Failed")).Should().BeError().And.WithMessage("Failed");

        [Fact]
        public async Task ResultT_Async_例外が発生しない場合は成功が返る()
        {
            var result = Result.Try(() => Task.Run(() => Result.Ok("Good")));
            result.Should().BeAssignableTo<Task<Result<string>>>();
            (await result.ConfigureAwait(false)).Should().BeOk().And.Match(v => v == "Good");
        }

        [Fact]
        public async Task ResultT_Async_例外が発生した場合は失敗が返る()
        {
            var result = Result.Try(() => Task.Run(() => RaiseException<Result<string>>("Failed")));
            result.Should().BeAssignableTo<Task<Result<string>>>();
            (await result.ConfigureAwait(false)).Should().BeError().And.WithMessage("Failed");
        }

        private void RaiseException(string message)
            => throw new Exception(message);

        private T RaiseException<T>(string message)
            => throw new Exception(message);
    }
}
