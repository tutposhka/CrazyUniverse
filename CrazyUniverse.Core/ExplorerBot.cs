
namespace CrazyUniverse.Core
{
    public class ExplorerBot : Robot, IScan, IChargeable
    {
        public ExplorerBot(string name, int battery) : base(name, battery)
        {
        }

        public string Scan()
        {
            if (Battery < 5)
            {
                return Name + " has not enough battery";
            }

            UseBattery(5);
            return Name + " found a strange alien rock";
        }

        public string Charge()
        {
            AddBattery(20);
            return Name + " charged 20%";
        }

        public override string CrazyAction()
        {
            if (Battery < 15)
            {
                return Name + " has not enough battery";
            }

            UseBattery(15);
            return Name + " discovered a new planet made of cheese";
        }
    }
}
