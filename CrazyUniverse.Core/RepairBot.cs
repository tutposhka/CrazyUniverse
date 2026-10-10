
namespace CrazyUniverse.Core
{
    public class RepairBot : Robot, IRepair
    {
        public RepairBot(string name, int battery) : base(name, battery)
        {
        }

        public string Repair(Robot robot)
        {
            if (Battery < 10)
            {
                return Name + " has not enough battery";
            }

            if (robot == this)
            {
                return Name + " cannot repair itself";
            }

            UseBattery(10);
            robot.AddBattery(30);

            return Name + " repaired " + robot.Name + ".";
        }

        public override string CrazyAction()
        {
            if (Battery < 20)
            {
                return Name + " has not enough battery";
            }

            UseBattery(20);
            return Name + " tried to repair itself with a spoon";
        }
    }
}
