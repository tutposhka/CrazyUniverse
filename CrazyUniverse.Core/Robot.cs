
namespace CrazyUniverse.Core
{
    public abstract class Robot
    {
        public string Name { get; }
        public int Battery { get; private set; }

        public Robot(string name, int battery)
        {
            Name = name;
            Battery = battery;
        }

        public void UseBattery(int amount)
        {
            Battery -= amount;

            if (Battery < 0)
            {
                Battery = 0;
            }
        }

        public void AddBattery(int amount)
        {
            Battery += amount;

            if (Battery > 100)
            {
                Battery = 100;
            }
        }

        public virtual string GetInfo()
        {
            return Name + " | Battery: " + Battery + "%";
        }

        public abstract string CrazyAction();
    }
}
