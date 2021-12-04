using System.Threading.Tasks;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class AsTaskTest
{
    [Fact]
    public async Task Taskに変換できる()
    {
        var ok = Result.Ok();
        var taskOk = await ok.AsTask().ConfigureAwait(false);
        ok.Should().Be(taskOk).And.BeOk();
    }
}
