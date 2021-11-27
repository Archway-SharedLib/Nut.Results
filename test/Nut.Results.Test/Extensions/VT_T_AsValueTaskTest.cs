using System.Threading.Tasks;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test;

public class VT_T_AsTaskTest
{
    [Fact]
    public async Task Taskに変換できる()
    {
        var ok = Result.Ok("ok");
        var taskOk = await ok.AsValueTask().ConfigureAwait(false);
        ok.Should().Be(taskOk).And.BeOk().And.Match(v => v == "ok");
    }
}
