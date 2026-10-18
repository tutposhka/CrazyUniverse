using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CrazyUniverse.Core;

namespace CrazyUniverse.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Repository<CleanerBot> cleaners = new Repository<CleanerBot>();
        private Repository<ExplorerBot> explorers = new Repository<ExplorerBot>();
        private Repository<RepairBot> repairers = new Repository<RepairBot>();
        private Repository<GuardBot> guards = new Repository<GuardBot>();

        private int selectedType;
        private int sortIndex;
        private Robot? selectedRobot;
        private string robotName = "";
        private string searchText = "";

        public string[] Types { get; } =
        {
            "CleanerBot",
            "ExplorerBot",
            "RepairBot",
            "GuardBot"
        };

        public string[] SortOptions { get; } =
        {
            "Name A-Z",
            "Battery high first"
        };

        public ObservableCollection<Robot> Robots { get; } =
            new ObservableCollection<Robot>();

        public ObservableCollection<string> Log { get; } =
            new ObservableCollection<string>();

        public string Stats { get; private set; } = "";

        public RelayCommand AddCommand { get; }
        public RelayCommand RemoveCommand { get; }
        public RelayCommand CrazyCommand { get; }
        public RelayCommand ChargeCommand { get; }
        public RelayCommand ScanCommand { get; }
        public RelayCommand RepairCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {
            AddCommand = new RelayCommand(
                AddRobot,
                () => SelectedType >= 0
                    && SelectedType < Types.Length
                    && !string.IsNullOrWhiteSpace(RobotName)
                    && RobotName.Trim().Length <= 30);

            RemoveCommand = new RelayCommand(
                RemoveRobot,
                () => SelectedRobot != null);

            CrazyCommand = new RelayCommand(
                CrazyAction,
                () => SelectedRobot != null);

            ChargeCommand = new RelayCommand(
                Charge,
                () => SelectedRobot is IChargeable);

            ScanCommand = new RelayCommand(
                Scan,
                () => SelectedRobot is IScan);

            RepairCommand = new RelayCommand(
                Repair,
                () => SelectedRobot != null && FindRepairBot() != null);

            cleaners.Log += text => Log.Add("[Cleaner] " + text);
            explorers.Log += text => Log.Add("[Explorer] " + text);
            repairers.Log += text => Log.Add("[Repair] " + text);
            guards.Log += text => Log.Add("[Guard] " + text);

            cleaners.Add(new CleanerBot("Ash", 100));
            explorers.Add(new ExplorerBot("Pathfinder", 80));
            repairers.Add(new RepairBot("Marvin", 90));
            guards.Add(new GuardBot("Revenant", 70));

            UpdateList();
        }

        public int SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType == value)
                {
                    return;
                }

                selectedType = value;
                SelectedRobot = null;

                Notify(nameof(SelectedType));
                UpdateList();
            }
        }

        public Robot? SelectedRobot
        {
            get { return selectedRobot; }
            set
            {
                if (selectedRobot == value)
                {
                    return;
                }

                selectedRobot = value;

                Notify(nameof(SelectedRobot));
                UpdateCommands();
            }
        }

        public string RobotName
        {
            get { return robotName; }
            set
            {
                robotName = value;

                Notify(nameof(RobotName));
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;

                Notify(nameof(SearchText));
                UpdateList();
            }
        }

        public int SortIndex
        {
            get { return sortIndex; }
            set
            {
                sortIndex = value;

                Notify(nameof(SortIndex));
                UpdateList();
            }
        }

        private IEnumerable<Robot> GetCurrentRobots()
        {
            if (SelectedType == 0)
            {
                return cleaners.GetAll();
            }

            if (SelectedType == 1)
            {
                return explorers.GetAll();
            }

            if (SelectedType == 2)
            {
                return repairers.GetAll();
            }

            if (SelectedType == 3)
            {
                return guards.GetAll();
            }

            return new List<Robot>();
        }

        private List<Robot> GetAllRobots()
        {
            List<Robot> all = new List<Robot>();

            all.AddRange(cleaners.GetAll());
            all.AddRange(explorers.GetAll());
            all.AddRange(repairers.GetAll());
            all.AddRange(guards.GetAll());

            return all;
        }

        private void UpdateList()
        {
            Robot? oldSelection = SelectedRobot;
            string search = SearchText.Trim().ToLower();

            var result = GetCurrentRobots()
                .Where(r => r.Name.ToLower().Contains(search));

            if (SortIndex == 1)
            {
                result = result.OrderByDescending(r => r.Battery);
            }
            else
            {
                result = result.OrderBy(r => r.Name);
            }

            List<Robot> found = result.ToList();

            Robots.Clear();

            foreach (Robot robot in found)
            {
                Robots.Add(robot);
            }

            if (oldSelection != null && Robots.Contains(oldSelection))
            {
                SelectedRobot = oldSelection;
            }
            else
            {
                SelectedRobot = null;
            }

            var groups = GetAllRobots()
                .GroupBy(r => r.GetType().Name);

            Stats = "";

            foreach (var group in groups)
            {
                Stats += group.Key + ": " + group.Count()
                    + Environment.NewLine;
            }

            if (Stats == "")
            {
                Stats = "No robots";
            }

            Notify(nameof(Stats));
            UpdateCommands();
        }

        private void AddRobot()
        {
            string name = RobotName.Trim();

            if (SelectedType == 0)
            {
                cleaners.Add(new CleanerBot(name, 100));
            }
            else if (SelectedType == 1)
            {
                explorers.Add(new ExplorerBot(name, 100));
            }
            else if (SelectedType == 2)
            {
                repairers.Add(new RepairBot(name, 100));
            }
            else if (SelectedType == 3)
            {
                guards.Add(new GuardBot(name, 100));
            }

            RobotName = "";
            UpdateList();
        }

        private void RemoveRobot()
        {
            if (SelectedRobot is CleanerBot cleaner)
            {
                cleaners.Remove(cleaner);
            }
            else if (SelectedRobot is ExplorerBot explorer)
            {
                explorers.Remove(explorer);
            }
            else if (SelectedRobot is RepairBot repairer)
            {
                repairers.Remove(repairer);
            }
            else if (SelectedRobot is GuardBot guard)
            {
                guards.Remove(guard);
            }

            SelectedRobot = null;
            UpdateList();
        }

        private void CrazyAction()
        {
            if (SelectedRobot == null)
            {
                return;
            }

            int oldLogCount = Log.Count;
            string message = SelectedRobot.CrazyAction();

            // Successful action is already logged by Repository.
            if (Log.Count == oldLogCount)
            {
                Log.Add(message);
            }

            UpdateList();
        }

        private void Charge()
        {
            if (SelectedRobot is IChargeable robot)
            {
                Log.Add(robot.Charge());
            }

            UpdateList();
        }

        private void Scan()
        {
            if (SelectedRobot is IScan robot)
            {
                Log.Add(robot.Scan());
            }

            UpdateList();
        }

        private RepairBot? FindRepairBot()
        {
            return repairers.GetAll()
                .FirstOrDefault(r => r != SelectedRobot);
        }

        private void Repair()
        {
            RepairBot? mechanic = FindRepairBot();

            if (mechanic != null && SelectedRobot != null)
            {
                Log.Add(mechanic.Repair(SelectedRobot));
            }

            UpdateList();
        }

        private void UpdateCommands()
        {
            AddCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            CrazyCommand.RaiseCanExecuteChanged();
            ChargeCommand.RaiseCanExecuteChanged();
            ScanCommand.RaiseCanExecuteChanged();
            RepairCommand.RaiseCanExecuteChanged();
        }

        private void Notify(string property)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(property));
        }
    }
}