using System.Threading.Tasks;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class T_AsTaskTest
    {
        [Fact]
        public async Task T_AsTask_Taskに変換できる()
        {
            var ok = Result.Ok("ok");
            var taskOk = await ok.AsTask().ConfigureAwait(false);
            ok.Should().Be(taskOk).And.BeOk().And.Match(v => v == "ok");
        }
    }
}