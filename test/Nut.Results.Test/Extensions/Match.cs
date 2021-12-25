using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Match
{
    // result to result---

    [Fact]
    public void R2R_SSS_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok(), (Func<Result>)null, (IError err) => Result.Ok());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void R2R_SSS_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok(), () => Result.Ok(), (Func<IError, Result>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void R2R_SSS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = ResultExtensions.Match(Result.Ok(), () =>
        {
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
    public void R2R_SSS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = ResultExtensions.Match(Result.Error(err), () =>
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

    // result to result---

    [Fact]
    public async Task R2R_SAS_okがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), (Func<Task<Result>>)null, (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_SAS_errがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), () => Result.Ok().AsTask(), (Func<IError, Result>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_SAS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok(), () =>
        {
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
    public async Task R2R_SAS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err), () =>
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

    // result to result---

    [Fact]
    public async Task R2R_SSA_okがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), (Func<Result>)null, (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_SSA_errがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), () => Result.Ok(), (Func<IError, Task<Result>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_SSA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok(), () =>
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
    public async Task R2R_SSA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err), () =>
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

    // result to result---

    [Fact]
    public async Task R2R_SAA_okがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), (Func<Task<Result>>)null, (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_SAA_errがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), () => Result.Ok().AsTask(), (Func<IError, Task<Result>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_SAA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok(), () =>
        {
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
    public async Task R2R_SAA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err), () =>
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

    // result to result---

    [Fact]
    public async Task R2R_ASS_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result>)null, () => Result.Ok(), (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_ASS_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), (Func<Result>)null, (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_ASS_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), () => Result.Ok(), (Func<IError, Result>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_ASS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok().AsTask(), () =>
        {
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
    public async Task R2R_ASS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err).AsTask(), () =>
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

    // result to result---

    [Fact]
    public async Task R2R_AAS_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result>)null, () => Result.Ok().AsTask(), (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_AAS_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), (Func<Task<Result>>)null, (IError err) => Result.Ok());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_AAS_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), () => Result.Ok().AsTask(), (Func<IError, Result>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_AAS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok().AsTask(), () =>
        {
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
    public async Task R2R_AAS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err).AsTask(), () =>
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

    // result to result---

    [Fact]
    public async Task R2R_ASA_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result>)null, () => Result.Ok(), (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_ASA_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), (Func<Result>)null, (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_ASA_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), () => Result.Ok(), (Func<IError, Task<Result>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_ASA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok().AsTask(), () =>
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
    public async Task R2R_ASA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err).AsTask(), () =>
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

    // result to result---

    [Fact]
    public async Task R2R_AAA_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result>)null, () => Result.Ok().AsTask(), (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_AAA_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), (Func<Task<Result>>)null, (IError err) => Result.Ok().AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_AAA_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), () => Result.Ok().AsTask(), (Func<IError, Task<Result>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2R_AAA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok().AsTask(), () =>
        {
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
    public async Task R2R_AAA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err).AsTask(), () =>
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

    // result to result<T>---

    [Fact]
    public void R2T_SSS_okがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok(), (Func<Result<string>>)null, (IError err) => Result.Ok("A"));
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void R2T_SSS_errがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Match(Result.Ok(), () => Result.Ok("A"), (Func<IError, Result<string>>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void R2T_SSS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = ResultExtensions.Match(Result.Ok(), () =>
        {
            executedOk = true;
            return Result.Ok("A");
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
    public void R2T_SSS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = ResultExtensions.Match(Result.Error(err), () =>
        {
            executedOk = true;
            return Result.Ok("A");
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result to result<T>---

    [Fact]
    public async Task R2T_SAS_okがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), (Func<Task<Result<string>>>)null, (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_SAS_errがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), () => Result.Ok("A").AsTask(), (Func<IError, Result<string>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_SAS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok(), () =>
        {
            executedOk = true;
            return Result.Ok("A").AsTask();
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
    public async Task R2T_SAS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err), () =>
        {
            executedOk = true;
            return Result.Ok("A").AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result to result<T>---

    [Fact]
    public async Task R2T_SSA_okがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), (Func<Result<string>>)null, (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_SSA_errがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), () => Result.Ok("A"), (Func<IError, Task<Result<string>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_SSA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok(), () =>
        {
            executedOk = true;
            return Result.Ok("A");
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
    public async Task R2T_SSA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err), () =>
        {
            executedOk = true;
            return Result.Ok("A");
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result to result<T>---

    [Fact]
    public async Task R2T_SAA_okがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), (Func<Task<Result<string>>>)null, (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_SAA_errがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Match(Result.Ok(), () => Result.Ok("A").AsTask(), (Func<IError, Task<Result<string>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_SAA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok(), () =>
        {
            executedOk = true;
            return Result.Ok("A").AsTask();
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
    public async Task R2T_SAA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err), () =>
        {
            executedOk = true;
            return Result.Ok("A").AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result to result<T>---

    [Fact]
    public async Task R2T_ASS_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result>)null, () => Result.Ok("A"), (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_ASS_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), (Func<Result<string>>)null, (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_ASS_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), () => Result.Ok("A"), (Func<IError, Result<string>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_ASS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok().AsTask(), () =>
        {
            executedOk = true;
            return Result.Ok("A");
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
    public async Task R2T_ASS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err).AsTask(), () =>
        {
            executedOk = true;
            return Result.Ok("A");
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result to result<T>---

    [Fact]
    public async Task R2T_AAS_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result>)null, () => Result.Ok("A").AsTask(), (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_AAS_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), (Func<Task<Result<string>>>)null, (IError err) => Result.Ok("A"));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_AAS_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), () => Result.Ok("A").AsTask(), (Func<IError, Result<string>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_AAS_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok().AsTask(), () =>
        {
            executedOk = true;
            return Result.Ok("A").AsTask();
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
    public async Task R2T_AAS_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err).AsTask(), () =>
        {
            executedOk = true;
            return Result.Ok("A").AsTask();
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error);
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result to result<T>---

    [Fact]
    public async Task R2T_ASA_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result>)null, () => Result.Ok("A"), (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_ASA_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), (Func<Result<string>>)null, (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_ASA_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), () => Result.Ok("A"), (Func<IError, Task<Result<string>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_ASA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok().AsTask(), () =>
        {
            executedOk = true;
            return Result.Ok("A");
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
    public async Task R2T_ASA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err).AsTask(), () =>
        {
            executedOk = true;
            return Result.Ok("A");
        }, error =>
        {
            executedError = true;
            return Result.Error<string>(error).AsTask();
        });
        result.Should().BeError().And.Match(error => err == error);
        executedOk.Should().BeFalse();
        executedError.Should().BeTrue();
    }

    // result to result<T>---

    [Fact]
    public async Task R2T_AAA_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match((Task<Result>)null, () => Result.Ok("A").AsTask(), (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_AAA_okがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), (Func<Task<Result<string>>>)null, (IError err) => Result.Ok("A").AsTask());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_AAA_errがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Match(Result.Ok().AsTask(), () => Result.Ok("A").AsTask(), (Func<IError, Task<Result<string>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task R2T_AAA_Okの場合はokが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var result = await ResultExtensions.Match(Result.Ok().AsTask(), () =>
        {
            executedOk = true;
            return Result.Ok("A").AsTask();
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
    public async Task R2T_AAA_Errorの場合はerrが実行される()
    {
        var executedOk = false;
        var executedError = false;
        var err = new Error();
        var result = await ResultExtensions.Match(Result.Error(err).AsTask(), () =>
        {
            executedOk = true;
            return Result.Ok("A").AsTask();
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
