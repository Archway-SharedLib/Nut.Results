using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test
{
    public class T_CombineTest
    {
        // value

        [Fact]
        public void Value_SyncSync_両方ともエラーの場合はAggregateErrorが返る()
        {
            var result = Result.Error<string>(new SourceError()).Combine(Result.Error<int>(new DestError()));
            result.Should().BeError().And.Match(e => e is AggregateError);
            var aggr = result.GetError() as AggregateError;
            aggr.Errors.Should().HaveCount(2);
            aggr.Errors.Should().Contain(e => e is SourceError);
            aggr.Errors.Should().Contain(e => e is DestError);
        }

        // この確認は各 Sync / Asyncにも必要だが、数が多くなってしまうため、コードカバレッジ結果を含めてこれ一つで仕様が正しいと判断することとする
        [Fact]
        public void Value_SyncSync_エラーがAggregateErrorの場合はまとめられる()
        {
            var result = Result.Error<string>(new AggregateError(new SourceError(), new Error())).Combine(Result.Error<int>(new DestError()));
            result.Should().BeError().And.Match(e => e is AggregateError);
            var aggr = result.GetError() as AggregateError;
            aggr.Errors.Should().HaveCount(3);
            aggr.Errors.Should().Contain(e => e is SourceError);
            aggr.Errors.Should().Contain(e => e is Error);
            aggr.Errors.Should().Contain(e => e is DestError);
        }

        [Fact]
        public void Value_SyncSync_Sourceだけエラーの場合はSourceのエラーが返る()
        {
            var result = Result.Error<string>(new SourceError()).Combine(Result.Ok(1));
            result.Should().BeError().And.Match(e => e is SourceError);
        }

        [Fact]
        public void Value_SyncSync_Destだけエラーの場合はDestのエラーが返る()
        {
            var result = Result.Ok<string>("ok").Combine(Result.Error<int>(new DestError()));
            result.Should().BeError().And.Match(e => e is DestError);
        }

        [Fact]
        public void Value_SyncSync_両方ともOkの場合は両方の値のタプルが返る()
        {
            var leftExpect = "ok";
            var rightExpect = 123;
            var result = Result.Ok<string>(leftExpect).Combine(Result.Ok<int>(rightExpect));
            result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
        }

        [Fact]
        public void Value_AsyncSync_Sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(null as Task<Result<string>>, Result.Ok("ok"));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }

        [Fact]
        public async Task Value_AsyncSync_両方ともエラーの場合はAggregateErrorが返る()
        {
            var result = await Result.Error<string>(new SourceError()).AsTask()
                .Combine(Result.Error<int>(new DestError())).ConfigureAwait(false);
            result.Should().BeError().And.Match(e => e is AggregateError);
            var aggr = result.GetError() as AggregateError;
            aggr.Errors.Should().HaveCount(2);
            aggr.Errors.Should().Contain(e => e is SourceError);
            aggr.Errors.Should().Contain(e => e is DestError);
        }

        [Fact]
        public async Task Value_AsyncSync_Sourceだけエラーの場合はSourceのエラーが返る()
        {
            var result = await Result.Error<string>(new SourceError()).AsTask()
                .Combine(Result.Ok(1)).ConfigureAwait(false);
            result.Should().BeError().And.Match(e => e is SourceError);
        }

        [Fact]
        public async Task Value_AsyncSync_Destだけエラーの場合はDestのエラーが返る()
        {
            var result = await Result.Ok<string>("ok").AsTask()
                .Combine(Result.Error<int>(new DestError())).ConfigureAwait(false);
            result.Should().BeError().And.Match(e => e is DestError);
        }

        [Fact]
        public async Task Value_AsyncSync_両方ともOkの場合は両方の値のタプルが返る()
        {
            var leftExpect = "ok";
            var rightExpect = 123;
            var result = await Result.Ok<string>(leftExpect).AsTask()
                .Combine(Result.Ok<int>(rightExpect)).ConfigureAwait(false);
            result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
        }

        [Fact]
        public void Value_SyncAsync_Destがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(Result.Ok("Ok"), (Task<Result<int>>)null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("dest");
        }

        [Fact]
        public async Task Value_SyncAsync_両方ともエラーの場合はAggregateErrorが返る()
        {
            var result = await Result.Error<string>(new SourceError())
                .Combine(Result.Error<int>(new DestError()).AsTask()).ConfigureAwait(false);
            result.Should().BeError().And.Match(e => e is AggregateError);
            var aggr = result.GetError() as AggregateError;
            aggr.Errors.Should().HaveCount(2);
            aggr.Errors.Should().Contain(e => e is SourceError);
            aggr.Errors.Should().Contain(e => e is DestError);
        }

        [Fact]
        public async Task Value_SyncAsync_Sourceだけエラーの場合はSourceのエラーが返る()
        {
            var result = await Result.Error<string>(new SourceError())
                .Combine(Result.Ok(1).AsTask());
            result.Should().BeError().And.Match(e => e is SourceError);
        }

        [Fact]
        public async Task Value_SyncAsync_Destだけエラーの場合はDestのエラーが返る()
        {
            var result = await Result.Ok<string>("ok")
                .Combine(Result.Error<int>(new DestError()).AsTask()).ConfigureAwait(false);
            result.Should().BeError().And.Match(e => e is DestError);
        }

        [Fact]
        public async Task Value_SyncAsync_両方ともOkの場合は両方の値のタプルが返る()
        {
            var leftExpect = "ok";
            var rightExpect = 123;
            var result = await Result.Ok<string>(leftExpect)
                .Combine(Result.Ok<int>(rightExpect).AsTask()).ConfigureAwait(false);
            result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
        }

        [Fact]
        public void Value_AsyncAsync_Sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(null as Task<Result<string>>, Result.Ok("ok").AsTask());
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }

        [Fact]
        public void Value_AsyncAsync_Destがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(Result.Ok("Ok").AsTask(), (Task<Result<int>>)null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("dest");
        }

        [Fact]
        public async Task Value_AsyncAsync_両方ともエラーの場合はAggregateErrorが返る()
        {
            var result = await Result.Error<string>(new SourceError()).AsTask()
                .Combine(Result.Error<int>(new DestError()).AsTask()).ConfigureAwait(false);
            result.Should().BeError().And.Match(e => e is AggregateError);
            var aggr = result.GetError() as AggregateError;
            aggr.Errors.Should().HaveCount(2);
            aggr.Errors.Should().Contain(e => e is SourceError);
            aggr.Errors.Should().Contain(e => e is DestError);
        }

        [Fact]
        public async Task Value_AsyncAsync_Sourceだけエラーの場合はSourceのエラーが返る()
        {
            var result = await Result.Error<string>(new SourceError()).AsTask()
                .Combine(Result.Ok(1).AsTask()).ConfigureAwait(false);
            result.Should().BeError().And.Match(e => e is SourceError);
        }

        [Fact]
        public async Task Value_AsyncAsync_Destだけエラーの場合はDestのエラーが返る()
        {
            var result = await Result.Ok<string>("ok").AsTask()
                .Combine(Result.Error<int>(new DestError()).AsTask()).ConfigureAwait(false);
            result.Should().BeError().And.Match(e => e is DestError);
        }

        [Fact]
        public async Task Value_AsyncAsync_両方ともOkの場合は両方の値のタプルが返る()
        {
            var leftExpect = "ok";
            var rightExpect = 123;
            var result = await Result.Ok<string>(leftExpect).AsTask()
                .Combine(Result.Ok<int>(rightExpect).AsTask()).ConfigureAwait(false);
            result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
        }

        // func

        [Fact]
        public void Func_SyncSync_Destがnullの場合は例外が発生する()
        {
            Action act = () => ResultExtensions.Combine(Result.Ok("ok"), (Func<Result<int>>)null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("destFunc");
        }

        [Fact]
        public void Func_SyncSync_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
        {
            var executed = false;
            var result = Result.Error<string>(new SourceError()).Combine(() =>
            {
                executed = true;
                return Result.Error<int>(new DestError());
            });
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e is SourceError);
        }

        [Fact]
        public void Func_SyncSync_Destがエラーの場合はDestのエラーが返る()
        {
            var executed = false;
            var result = Result.Ok<string>("ok").Combine(() => {
                executed = true;
                return Result.Error<int>(new DestError());
            });
            executed.Should().BeTrue();
            result.Should().BeError().And.Match(e => e is DestError);
        }

        [Fact]
        public void Func_SyncSync_両方ともOkの場合は両方の値のタプルが返る()
        {
            var leftExpect = "ok";
            var rightExpect = 123;
            var result = Result.Ok<string>(leftExpect).Combine(() => Result.Ok<int>(rightExpect));
            result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
        }

        [Fact]
        public void Func_AsyncSync_Sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(null as Task<Result<string>>, () => Result.Ok("ok"));
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }

        [Fact]
        public void Func_AsyncSync_Destがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(Result.Ok("Ok").AsTask(), (Func<Result<int>>)null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("destFunc");
        }

        [Fact]
        public async Task Func_AsyncSync_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
        {
            var executed = false;
            var result = await Result.Error<string>(new SourceError()).AsTask().Combine(() =>
            {
                executed = true;
                return Result.Error<int>(new DestError());
            }).ConfigureAwait(false);
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e is SourceError);
        }

        [Fact]
        public async Task Func_AsyncSync_Destがエラーの場合はDestのエラーが返る()
        {
            var executed = false;
            var result = await Result.Ok<string>("ok").AsTask().Combine(() => {
                executed = true;
                return Result.Error<int>(new DestError());
            }).ConfigureAwait(false);
            executed.Should().BeTrue();
            result.Should().BeError().And.Match(e => e is DestError);
        }

        [Fact]
        public async Task Func_AsyncSync_両方ともOkの場合は両方の値のタプルが返る()
        {
            var leftExpect = "ok";
            var rightExpect = 123;
            var result = await Result.Ok<string>(leftExpect).AsTask()
                .Combine(() => Result.Ok<int>(rightExpect)).ConfigureAwait(false);
            result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
        }

        [Fact]
        public void Func_SyncAsync_Destがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(Result.Ok("Ok"), (Func<Task<Result<int>>>)null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("destFunc");
        }

        [Fact]
        public async Task Func_SyncAsync_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
        {
            var executed = false;
            var result = await Result.Error<string>(new SourceError()).Combine(() =>
            {
                executed = true;
                return Result.Error<int>(new DestError()).AsTask();
            }).ConfigureAwait(false);
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e is SourceError);
        }

        [Fact]
        public async Task Func_SyncAsync_Destがエラーの場合はDestのエラーが返る()
        {
            var executed = false;
            var result = await Result.Ok<string>("ok").Combine(() => {
                executed = true;
                return Result.Error<int>(new DestError()).AsTask();
            }).ConfigureAwait(false);
            executed.Should().BeTrue();
            result.Should().BeError().And.Match(e => e is DestError);
        }

        [Fact]
        public async Task Func_SyncAsync_両方ともOkの場合は両方の値のタプルが返る()
        {
            var leftExpect = "ok";
            var rightExpect = 123;
            var result = await Result.Ok<string>(leftExpect)
                .Combine(() => Result.Ok<int>(rightExpect).AsTask()).ConfigureAwait(false);
            result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
        }

        [Fact]
        public void Func_AsyncAsync_Sourceがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(null as Task<Result<string>>, () => Result.Ok("ok").AsTask());
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }

        [Fact]
        public void Func_AsyncAsync_Destがnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.Combine(Result.Ok("Ok").AsTask(), (Func<Task<Result<int>>>)null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("destFunc");
        }

        [Fact]
        public async Task Func_AsyncAsync_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
        {
            var executed = false;
            var result = await Result.Error<string>(new SourceError()).AsTask().Combine(() =>
            {
                executed = true;
                return Result.Error<int>(new DestError()).AsTask();
            }).ConfigureAwait(false);
            executed.Should().BeFalse();
            result.Should().BeError().And.Match(e => e is SourceError);
        }

        [Fact]
        public async Task Func_AsyncAsync_Destがエラーの場合はDestのエラーが返る()
        {
            var executed = false;
            var result = await Result.Ok<string>("ok").AsTask().Combine(() => {
                executed = true;
                return Result.Error<int>(new DestError()).AsTask();
            }).ConfigureAwait(false);
            executed.Should().BeTrue();
            result.Should().BeError().And.Match(e => e is DestError);
        }

        [Fact]
        public async Task Func_AsyncAsync_両方ともOkの場合は両方の値のタプルが返る()
        {
            var leftExpect = "ok";
            var rightExpect = 123;
            var result = await Result.Ok<string>(leftExpect).AsTask()
                .Combine(() => Result.Ok<int>(rightExpect).AsTask()).ConfigureAwait(false);
            result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
        }

        public class SourceError: Error { }

        public class DestError : Error { }
    }
}
