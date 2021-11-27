using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test;

public class T_MatchTest
{
    // result<T> to result--- 

    [Fact]
    public void T2R_SSS_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (Func<string, Result>)null, (IError err) => Result.Ok());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2R_SSS_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (_) => Result.Ok(), (Func<IError, Result>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2R_SSS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = ResultExtensions.Match(Result.Ok("A"), v =>
        {
            v.Should().Be("A");
            executedOk = true;
            return Result.Ok();
        }, err =>
        {
            executedError = true;
            return Result.Error(err);
        });
        result.Should().BeOk();
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public void T2R_SSS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = ResultExtensions.Match(Result.Error<string>(err), _ =>
        {
            executedOk = true;
            return Result.Ok();
        }, error =>
        {
            executedError = true;
            return Result.Error(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result--- 

    [Fact]
    public void T2R_SAS_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (Func<string, Task<Result>>)null, (IError err) => Result.Ok());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2R_SAS_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), _ => Result.Ok().AsTask(), (Func<IError, Result>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_SAS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A"), v =>
        {
            v.Should().Be("A");
            executedOk = true;
            return Result.Ok().AsTask();
        }, err =>
        {
            executedError = true;
            return Result.Error(err);
        });
        result.Should().BeOk();
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2R_SAS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err), _ =>
        {
            executedOk = true;
            return Result.Ok().AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result--- 

    [Fact]
    public void T2R_SSA_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (Func<string, Result>)null, (IError err) => Result.Ok().AsTask());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2R_SSA_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), _ => Result.Ok(), (Func<IError, Task<Result>>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_SSA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A"), _ =>
        {
            executedOk = true;
            return Result.Ok();
        }, err =>
        {
            executedError = true;
            return Result.Error(err).AsTask();
        });
        result.Should().BeOk();
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2R_SSA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err), _ =>
        {
            executedOk = true;
            return Result.Ok();
        }, error =>
        {
            executedError = true;
            return Result.Error(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result--- 

    [Fact]
    public void T2R_SAA_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (Func<string, Task<Result>>)null, (IError err) => Result.Ok().AsTask());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2R_SAA_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), _ => Result.Ok().AsTask(), (Func<IError, Task<Result>>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_SAA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A"), v =>
        {
            v.Should().Be("A");
            executedOk = true;
            return Result.Ok().AsTask();
        }, err =>
        {
            executedError = true;
            return Result.Error(err).AsTask();
        });
        result.Should().BeOk();
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2R_SAA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err), _ =>
        {
            executedOk = true;
            return Result.Ok().AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result--- 

    [Fact]
    public async Task T2R_ASS_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result<string>>)null, _ => Result.Ok(), (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_ASS_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), (Func<string, Result>)null, (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_ASS_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), _ => Result.Ok(), (Func<IError, Result>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_ASS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A").AsTask(), v =>
        {
            v.Should().Be("A");
            executedOk = true;
            return Result.Ok();
        }, err =>
        {
            executedError = true;
            return Result.Error(err);
        });
        result.Should().BeOk();
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2R_ASS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err).AsTask(), _ =>
        {
            executedOk = true;
            return Result.Ok();
        }, error =>
        {
            executedError = true;
            return Result.Error(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result--- 

    [Fact]
    public async Task T2R_AAS_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result<string>>)null, _ => Result.Ok().AsTask(), (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_AAS_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), (Func<string, Task<Result>>)null, (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_AAS_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), _ => Result.Ok().AsTask(), (Func<IError, Result>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_AAS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A").AsTask(), v =>
        {
            v.Should().Be("A");
            executedOk = true;
            return Result.Ok().AsTask();
        }, err =>
        {
            executedError = true;
            return Result.Error(err);
        });
        result.Should().BeOk();
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2R_AAS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err).AsTask(), _ =>
        {
            executedOk = true;
            return Result.Ok().AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result--- 

    [Fact]
    public async Task T2R_ASA_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result<string>>)null, _ => Result.Ok(), (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_ASA_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), (Func<string, Result>)null, (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_ASA_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), _ => Result.Ok(), (Func<IError, Task<Result>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_ASA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A").AsTask(), v =>
        {
            v.Should().Be("A");
            executedOk = true;
            return Result.Ok();
        }, err =>
        {
            executedError = true;
            return Result.Error(err).AsTask();
        });
        result.Should().BeOk();
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2R_ASA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err).AsTask(), _ =>
        {
            executedOk = true;
            return Result.Ok();
        }, error =>
        {
            executedError = true;
            return Result.Error(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result--- 

    [Fact]
    public async Task T2R_AAA_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result<string>>)null, _ => Result.Ok().AsTask(), (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_AAA_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), (Func<string, Task<Result>>)null, (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_AAA_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), _ => Result.Ok().AsTask(), (Func<IError, Task<Result>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2R_AAA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A").AsTask(), v =>
        {
            v.Should().Be("A");
            executedOk = true;
            return Result.Ok().AsTask();
        }, err =>
        {
            executedError = true;
            return Result.Error(err).AsTask();
        });
        result.Should().BeOk();
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2R_AAA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err).AsTask(), _ =>
        {
            executedOk = true;
            return Result.Ok().AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // -----------------------------------------------------------------------------
    // result<T> to result<T>--- 

    [Fact]
    public void T2T_SSS_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (Func<string, Result<string>>)null, (IError err) => Result.Ok("A"));
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2T_SSS_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), v => Result.Ok(v), (Func<IError, Result<string>>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2T_SSS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = ResultExtensions.Match(Result.Ok("A"), v =>
        {
            executedOk = true;
            return Result.Ok(v);
        }, err =>
        {
            executedError = true;
            return Result.Error<string>(err);
        });
        result.Should().BeOk().And.Match(v => v == "A");
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public void T2T_SSS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = ResultExtensions.Match(Result.Error<string>(err), v =>
        {
            executedOk = true;
            return Result.Ok(v);
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result<T>--- 

    [Fact]
    public void T2T_SAS_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (Func<string, Task<Result<string>>>)null, (IError err) => Result.Ok("A"));
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2T_SAS_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), v => Result.Ok(v).AsTask(), (Func<IError, Result<string>>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_SAS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A"), v =>
        {
            executedOk = true;
            return Result.Ok(v).AsTask();
        }, err =>
        {
            executedError = true;
            return Result.Error<string>(err);
        });
        result.Should().BeOk().And.Match(v => v == "A");
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2T_SAS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err), v =>
        {
            executedOk = true;
            return Result.Ok(v).AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result<T>--- 

    [Fact]
    public void T2T_SSA_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (Func<string, Result<string>>)null, (IError err) => Result.Ok("A").AsTask());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2T_SSA_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), v => Result.Ok(v), (Func<IError, Task<Result<string>>>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_SSA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A"), v =>
        {
            executedOk = true;
            return Result.Ok(v);
        }, err =>
        {
            executedError = true;
            return Result.Error<string>(err).AsTask();
        });
        result.Should().BeOk().And.Match(v => v == "A");
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2T_SSA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err), v =>
        {
            executedOk = true;
            return Result.Ok(v);
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result<T>--- 

    [Fact]
    public void T2T_SAA_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), (Func<string, Task<Result<string>>>)null, (IError err) => Result.Ok("A").AsTask());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T2T_SAA_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok("A"), v => Result.Ok(v).AsTask(), (Func<IError, Task<Result<string>>>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_SAA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A"), v =>
        {
            executedOk = true;
            return Result.Ok(v).AsTask();
        }, err =>
        {
            executedError = true;
            return Result.Error<string>(err).AsTask();
        });
        result.Should().BeOk().And.Match(v => v == "A");
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2T_SAA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err), v =>
        {
            executedOk = true;
            return Result.Ok(v).AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result<T>--- 

    [Fact]
    public async Task T2T_ASS_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result<string>>)null, v => Result.Ok(v), (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_ASS_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), (Func<string, Result<string>>)null, (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_ASS_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), v => Result.Ok(v), (Func<IError, Result<string>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_ASS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A").AsTask(), v =>
        {
            executedOk = true;
            return Result.Ok(v);
        }, err =>
        {
            executedError = true;
            return Result.Error<string>(err);
        });
        result.Should().BeOk().And.Match(v => v == "A");
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2T_ASS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err).AsTask(), v =>
        {
            executedOk = true;
            return Result.Ok(v);
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result<T>--- 

    [Fact]
    public async Task T2T_AAS_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result<string>>)null, v => Result.Ok(v).AsTask(), (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_AAS_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), (Func<string, Task<Result<string>>>)null, (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_AAS_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), v => Result.Ok(v).AsTask(), (Func<IError, Result<string>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_AAS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A").AsTask(), v =>
        {
            executedOk = true;
            return Result.Ok(v).AsTask();
        }, err =>
        {
            executedError = true;
            return Result.Error<string>(err);
        });
        result.Should().BeOk().And.Match(v => v == "A");
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2T_AAS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err).AsTask(), v =>
        {
            executedOk = true;
            return Result.Ok(v).AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result<T>--- 

    [Fact]
    public async Task T2T_ASA_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result<string>>)null, v => Result.Ok(v), (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_ASA_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), (Func<string, Result<string>>)null, (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_ASA_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), v => Result.Ok(v), (Func<IError, Task<Result<string>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_ASA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A").AsTask(), v =>
        {
            executedOk = true;
            return Result.Ok(v);
        }, err =>
        {
            executedError = true;
            return Result.Error<string>(err).AsTask();
        });
        result.Should().BeOk().And.Match(v => v == "A");
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2T_ASA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err).AsTask(), v =>
        {
            executedOk = true;
            return Result.Ok(v);
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result<T> to result<T>--- 

    [Fact]
    public async Task T2T_AAA_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result<string>>)null, v => Result.Ok(v).AsTask(), (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_AAA_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), (Func<string, Task<Result<string>>>)null, (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_AAA_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok("A").AsTask(), v => Result.Ok(v).AsTask(), (Func<IError, Task<Result<string>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T2T_AAA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok("A").AsTask(), v =>
        {
            executedOk = true;
            return Result.Ok(v).AsTask();
        }, err =>
        {
            executedError = true;
            return Result.Error<string>(err).AsTask();
        });
        result.Should().BeOk().And.Match(v => v == "A");
        executedOk.Should().BeTrue();
        executedError.Should().BeFalse();
    }

    [Fact]
    public async Task T2T_AAA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error<string>(err).AsTask(), v =>
        {
            executedOk = true;
            return Result.Ok(v).AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }
}
