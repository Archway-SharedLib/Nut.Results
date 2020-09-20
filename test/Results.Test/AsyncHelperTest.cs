using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Archway.Results.Test
{
    public class AsyncHelperTest
    {
        private bool completed = false;

        [Fact]
        public void RunSync_同期的に実行される()
        {
            completed = false;
            var result = RunAsyncVoid();
            completed.Should().BeFalse();
            result.Wait();
            completed.Should().BeTrue();

            completed = false;
            AsyncHelper.RunSync(RunAsyncVoid);
            completed.Should().BeTrue();
        }

        [Fact]
        public void RunSync_wizReturn_同期的に実行される()
        {
            completed = false;
            var result = RunAsync();
            completed.Should().BeFalse();
            result.Wait();
            completed.Should().BeTrue();

            completed = false;
            var message = AsyncHelper.RunSync(RunAsync);
            completed.Should().BeTrue();
            message.Should().Be("async");
        }


        private Task RunAsyncVoid()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(100);
                completed = true;
            });
        }

        private Task<string> RunAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(100);
                completed = true;
                return "async";
            });
        }
    }
}
