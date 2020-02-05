using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Playball.GroupManagement.Web.Controllers
{
    [Route("queue")]
    public class DemoNotRecommendedQueueController : Controller
    {
        private static readonly ConcurrentQueue<TaskCompletionSource<int>> TaskCompletationSourceQueue = new ConcurrentQueue<TaskCompletionSource<int>>();
        private static readonly ConcurrentQueue<CancellationTokenSource> cancellationTokenSourceQueue = new ConcurrentQueue<CancellationTokenSource>();

        [Route("ask")]
        public async Task<IActionResult> AskAsync()
        {
            var tcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
            TaskCompletationSourceQueue.Enqueue(tcs);
            var result = await tcs.Task;

            return Content(result.ToString());
        }

        [Route("tell/{value}")]
        public IActionResult Tell(int value)
        {
            if (TaskCompletationSourceQueue.TryDequeue(out var tcs))
            {
                if (!tcs.TrySetResult(value))
                {
                    return StatusCode(500);
                }
                return NoContent();
            }

            return NotFound();
        }

        [Route("cancel")]
        public IActionResult Cancel()
        {
            if (TaskCompletationSourceQueue.TryDequeue(out var tcs))
            {
                if (!tcs.TrySetCanceled())
                {
                    return StatusCode(500);
                }
                return NoContent();
            }

            return NotFound();
        }


        [Route("delay/{value}")]
        public async Task<IActionResult> DelayAsync(int value)
        {
            using (var cts = new CancellationTokenSource())
            {
                cancellationTokenSourceQueue.Enqueue(cts);
                await Task.Delay(value, cts.Token);
                cancellationTokenSourceQueue.TryDequeue(out _);
                return Content("Done Waiting");

            }
        }

        [Route("delay/cancel")]
        public IActionResult CancelDelay()
        {
            if (cancellationTokenSourceQueue.TryDequeue(out var cts))
            {
                cts.Cancel();
                return Content("Delay Cancelled!");
            }
            return NotFound();
        }
    }
}