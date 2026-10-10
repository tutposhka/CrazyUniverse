
namespace CrazyUniverse.Core
{
    public class CleanerBot : Robot, IChargeable
    {
        public CleanerBot(string name, int battery) : base(name, battery)
        {
        }

        public string Charge()
        {
            AddBattery(20);
            return Name + " charged 20%";
        }

        public override string CrazyAction()
        {
            if (Battery < 10)
            {
                return Name + " has not enough battery";
            }

            UseBattery(10);
            return Name + " cleaned the whole city with a tiny toothbrush";
        }
    }
}
