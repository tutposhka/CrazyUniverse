using System.Linq;

namespace CrazyUniverse.Core
{
    public class Repository<T> : IRepository<T> where T : Robot
    {
        private List<T> items = new List<T>();

        public event EventHandler<RobotEventArgs>? Changed;
        public event Action<string>? Log;

        public void Add(T item)
        {
            if (item.InRepository)
            {
                throw new ArgumentException("Robot is already in a group");
            }

            items.Add(item);
            item.InRepository = true;

            Changed += item.OnRepositoryEvent;
            item.ActionDone += OnActionDone;
            item.ReactionDone += OnReactionDone;

            Log?.Invoke(item.Name + " added");

            Changed?.Invoke(
                this,
                new RobotEventArgs(item, RobotEventType.Added));
        }

        public void Remove(T item)
        {
            if (!items.Remove(item))
            {
                return;
            }

            Changed -= item.OnRepositoryEvent;
            item.ActionDone -= OnActionDone;
            item.ReactionDone -= OnReactionDone;

            item.InRepository = false;

            Log?.Invoke(item.Name + " removed");

            Changed?.Invoke(
                this,
                new RobotEventArgs(item, RobotEventType.Removed));
        }

        public IEnumerable<T> GetAll()
        {
            return items.AsReadOnly();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return items.Where(predicate);
        }

        private void OnActionDone(Robot robot, string message)
        {
            Log?.Invoke(message);

            Changed?.Invoke(
                this,
                new RobotEventArgs(robot, RobotEventType.CrazyAction));
        }

        private void OnReactionDone(string message)
        {
            Log?.Invoke(message);
        }
    }
}