using System.Threading.Tasks;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class AsValueTaskTest
{
    [Fact]
    public async Task ValueTaskに変換できる()
    {
        var ok = Result.Ok();
        var taskOk = await ok.AsValueTask().ConfigureAwait(false);
        ok.Should().Be(taskOk).And.BeOk();
    }
}
