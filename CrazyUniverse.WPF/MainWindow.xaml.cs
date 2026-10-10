
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CrazyUniverse.Core;

namespace CrazyUniverse.WPF
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Robot> robots = new ObservableCollection<Robot>();

        public MainWindow()
        {
            InitializeComponent();

            RobotList.ItemsSource = robots;
            RobotList.DisplayMemberPath = "Name";

            robots.Add(new CleanerBot("Ash", 100));
            robots.Add(new ExplorerBot("Pathfinder", 80));
            robots.Add(new RepairBot("Marvin", 90));
            robots.Add(new GuardBot("Revenant", 70));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text;

            if (name == "")
            {
                MessageBox.Show("Enter robot name");
                return;
            }

            int type = TypeBox.SelectedIndex;

            if (type == 0)
                robots.Add(new CleanerBot(name, 100));
            else if (type == 1)
                robots.Add(new ExplorerBot(name, 100));
            else if (type == 2)
                robots.Add(new RepairBot(name, 100));
            else if (type == 3)
                robots.Add(new GuardBot(name, 100));

            NameBox.Clear();
            LogList.Items.Add(name + " added");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Robot robot = RobotList.SelectedItem as Robot;

            if (robot != null)
            {
                robots.Remove(robot);
                LogList.Items.Add(robot.Name + " deleted");
                InfoText.Text = "Select a robot";
            }
        }

        private void RobotList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Robot robot = RobotList.SelectedItem as Robot;

            if (robot != null)
            {
                InfoText.Text = robot.GetInfo();
            }
        }

        private void Crazy_Click(object sender, RoutedEventArgs e)
        {
            Robot robot = RobotList.SelectedItem as Robot;

            if (robot != null)
            {
                string result = robot.CrazyAction();
                LogList.Items.Add(result);
                InfoText.Text = robot.GetInfo();
            }
        }

        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            Robot robot = RobotList.SelectedItem as Robot;

            if (robot is IChargeable chargeable)
            {
                LogList.Items.Add(chargeable.Charge());
                InfoText.Text = robot.GetInfo();
            }
            else
            {
                LogList.Items.Add("This robot cannot charge");
            }
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            Robot robot = RobotList.SelectedItem as Robot;

            if (robot is IScan scanner)
            {
                LogList.Items.Add(scanner.Scan());
                InfoText.Text = robot.GetInfo();
            }
            else
            {
                LogList.Items.Add("This robot cannot scan");
            }
        }

        private void Repair_Click(object sender, RoutedEventArgs e)
        {
            Robot robot = RobotList.SelectedItem as Robot;

            if (robot != null)
            {
                RepairBot marvin = null;

                foreach (Robot r in robots)
                {
                    if (r is RepairBot)
                    {
                        marvin = r as RepairBot;
                        break;
                    }
                }

                if (marvin != null)
                {
                    LogList.Items.Add(marvin.Repair(robot));
                    InfoText.Text = robot.GetInfo();
                }
                else
                {
                    LogList.Items.Add("No repair robot found");
                }
            }
        }
    }
}
