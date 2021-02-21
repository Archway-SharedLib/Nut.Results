using System.Threading.Tasks;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class AsTaskTest
    {
        [Fact]
        public async Task AsTask_Taskに変換できる()
        {
            var ok = Result.Ok();
            var taskOk = await ok.AsTask().ConfigureAwait(false);
            ok.Should().Be(taskOk).And.BeOk();
        }
    }
}