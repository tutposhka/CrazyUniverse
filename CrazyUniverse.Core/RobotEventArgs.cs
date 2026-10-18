namespace CrazyUniverse.Core
{
    public enum RobotEventType
    {
        Added,
        Removed,
        CrazyAction
    }

    public class RobotEventArgs : EventArgs
    {
        public Robot Actor { get; }
        public RobotEventType Type { get; }

        public RobotEventArgs(Robot actor, RobotEventType type)
        {
            Actor = actor;
            Type = type;
        }
    }
}