using System.Threading.Tasks;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class AsTask_T
{
    [Fact]
    public async Task Taskに変換できる()
    {
        var ok = Result.Ok("ok");
        var taskOk = await ok.AsTask().ConfigureAwait(false);
        ok.Should().Be(taskOk).And.BeOk().And.Match(v => v == "ok");
    }
}
