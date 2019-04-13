using System;
using System.Threading;
using System.Threading.Tasks;

namespace CookEat
{
    public static class TaskExtension
    {
        public static Task RunPeriodicly(
            Func<Task> actionAsync,
            TimeSpan delayTime,
            CancellationToken cancellationToken)
        {
            return Task.Run(
                async () =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        try
                        {
                            await actionAsync();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        await Task.Delay(delayTime,cancellationToken);
                    }
                },
                cancellationToken);
        }
    }
}