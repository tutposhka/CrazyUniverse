using System.ComponentModel;

namespace CrazyUniverse.Core
{
    public abstract class Robot : INotifyPropertyChanged
    {
        public string Name { get; }
        public int Battery { get; private set; }
        public int Reactions { get; private set; }

        public string Info
        {
            get { return GetInfo(); }
        }

        internal bool InRepository { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public event Action<Robot, string>? ActionDone;
        public event Action<string>? ReactionDone;

        protected Robot(string name, int battery)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Trim().Length > 30)
            {
                throw new ArgumentException("Name must be 1-30 characters");
            }

            if (battery < 0 || battery > 100)
            {
                throw new ArgumentException("Battery must be 0-100");
            }

            Name = name.Trim();
            Battery = battery;
        }

        public void UseBattery(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative");
            }

            Battery = Math.Max(0, Battery - amount);

            Notify(nameof(Battery));
            Notify(nameof(Info));
        }

        public void AddBattery(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative");
            }

            int space = 100 - Battery;

            if (amount > space)
            {
                Battery = 100;
            }
            else
            {
                Battery += amount;
            }

            Notify(nameof(Battery));
            Notify(nameof(Info));
        }

        public virtual string GetInfo()
        {
            return Name + " | Battery: " + Battery
                + "% | Reactions: " + Reactions;
        }

        protected string FinishAction(string message)
        {
            ActionDone?.Invoke(this, message);
            return message;
        }

        internal void OnRepositoryEvent(object? sender, RobotEventArgs e)
        {
            if (e.Actor == this)
            {
                return;
            }

            Reactions++;

            string message;

            if (e.Type == RobotEventType.Added)
            {
                message = Name + " welcomes " + e.Actor.Name;
            }
            else if (e.Type == RobotEventType.Removed)
            {
                message = Name + " says bye to " + e.Actor.Name;
            }
            else
            {
                UseBattery(1);
                message = ReactToAction(e.Actor) + " (-1 battery)";
            }

            Notify(nameof(Reactions));
            Notify(nameof(Info));

            ReactionDone?.Invoke(message);
        }

        protected virtual string ReactToAction(Robot other)
        {
            return Name + " watches " + other.Name;
        }

        private void Notify(string property)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(property));
        }

        public abstract string CrazyAction();
    }
}