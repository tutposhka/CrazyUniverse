using System.Diagnostics;

namespace CrazyUniverse.Core
{
    public class SimulationInput
    {
        public string Name { get; }
        public int Battery { get; }
        public int Reactions { get; }
        public int Work { get; }

        public SimulationInput(string name, int battery, int reactions, int work)
        {
            Name = name;
            Battery = battery;
            Reactions = reactions;
            Work = work;
        }
    }

    public class SimulationResult
    {
        public string Name { get; set; } = "";
        public string Status { get; set; } = "";
        public string Message { get; set; } = "";
        public long Time { get; set; }
    }

    public static class RobotSimulation
    {
        public static SimulationResult Run(SimulationInput robot, CancellationToken token)
        {
            Stopwatch watch = Stopwatch.StartNew();
            SimulationResult result = new SimulationResult();
            result.Name = robot.Name;

            try
            {
                token.ThrowIfCancellationRequested();

                if (robot.Battery == 0)
                {
                    throw new InvalidOperationException("Battery is empty");
                }

                long score = 0;
                int number = robot.Battery + robot.Reactions + 7;

                for (int i = 0; i < robot.Work; i++)
                {
                    token.ThrowIfCancellationRequested();
                    score += (i % number) * (long)(i % 97);
                }

                result.Status = "Done";
                result.Message = "Score: " + score;
            }
            catch (OperationCanceledException)
            {
                result.Status = "Cancelled";
                result.Message = "Cancelled by user";
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Message = ex.Message;
            }

            watch.Stop();
            result.Time = watch.ElapsedMilliseconds;
            return result;
        }
    }
}
