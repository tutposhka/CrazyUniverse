using System.Diagnostics;
using System.Windows;
using CrazyUniverse.Core;

namespace CrazyUniverse.WPF
{
    public partial class SimulationWindow : Window
    {
        private CancellationTokenSource? cancelSource;
        private double? sequentialTime;
        private double? parallelTime;
        private string lastInput = "";

        public SimulationWindow(List<Robot> robots)
        {
            InitializeComponent();
            RobotsList.ItemsSource = robots;
        }

        private async void Sequential_Click(object sender, RoutedEventArgs e)
        {
            await StartSimulation(false);
        }

        private async void Parallel_Click(object sender, RoutedEventArgs e)
        {
            await StartSimulation(true);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            cancelSource?.Cancel();
            StatusText.Text = "Cancelling...";
        }

        private async Task StartSimulation(bool parallel)
        {
            if (RobotsList.SelectedItems.Count < 3)
            {
                StatusText.Text = "Select at least 3 robots";
                return;
            }

            int work = 5000000;
            if (WorkBox.SelectedIndex == 1)
            {
                work = 50000000;
            }
            if (WorkBox.SelectedIndex == 2)
            {
                work = 250000000;
            }

            List<SimulationInput> inputs = new List<SimulationInput>();
            string inputKey = work.ToString();

            foreach (Robot robot in RobotsList.SelectedItems)
            {
                inputs.Add(new SimulationInput(robot.Name, robot.Battery, robot.Reactions, work));
                inputKey += "|" + robot.Name + ":" + robot.Battery + ":" + robot.Reactions;
            }

            if (inputKey != lastInput)
            {
                lastInput = inputKey;
                sequentialTime = null;
                parallelTime = null;
                ResultsList.Items.Clear();
            }

            cancelSource = new CancellationTokenSource();
            CancellationToken token = cancelSource.Token;
            SequentialButton.IsEnabled = false;
            ParallelButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            RobotsList.IsEnabled = false;
            WorkBox.IsEnabled = false;
            StatusText.Text = "Running...";

            Stopwatch total = Stopwatch.StartNew();

            try
            {
                List<SimulationResult> results = new List<SimulationResult>();

                if (parallel)
                {
                    List<Task<SimulationResult>> tasks = new List<Task<SimulationResult>>();
                    foreach (SimulationInput input in inputs)
                    {
                        tasks.Add(Task.Run(() => RobotSimulation.Run(input, token)));
                    }

                    SimulationResult[] completed = await Task.WhenAll(tasks);
                    results.AddRange(completed);
                }
                else
                {
                    foreach (SimulationInput input in inputs)
                    {
                        SimulationResult result = await Task.Run(() => RobotSimulation.Run(input, token));
                        results.Add(result);
                    }
                }

                total.Stop();
                string mode = parallel ? "Parallel" : "Sequential";
                ResultsList.Items.Add("--- " + mode + " ---");

                bool allDone = true;
                foreach (SimulationResult result in results)
                {
                    ResultsList.Items.Add(result.Name + " | " + result.Status + " | "
                        + result.Message + " | " + result.Time + " ms");
                    if (result.Status != "Done")
                    {
                        allDone = false;
                    }
                }

                ResultsList.Items.Add("Total: " + total.ElapsedMilliseconds + " ms");

                if (parallel)
                {
                    parallelTime = allDone ? total.Elapsed.TotalMilliseconds : null;
                }
                else
                {
                    sequentialTime = allDone ? total.Elapsed.TotalMilliseconds : null;
                }

                if (parallelTime != null && sequentialTime != null)
                {
                    ResultsList.Items.Add("Compare: sequential "
                        + sequentialTime.Value.ToString("0") + " ms, parallel "
                        + parallelTime.Value.ToString("0") + " ms");
                }

                StatusText.Text = "Finished";
            }
            catch (Exception ex)
            {
                StatusText.Text = "Error";
                ResultsList.Items.Add(ex.Message);
            }
            finally
            {
                SequentialButton.IsEnabled = true;
                ParallelButton.IsEnabled = true;
                CancelButton.IsEnabled = false;
                RobotsList.IsEnabled = true;
                WorkBox.IsEnabled = true;
                cancelSource?.Dispose();
                cancelSource = null;
            }
        }
    }
}
