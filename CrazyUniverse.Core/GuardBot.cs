
namespace CrazyUniverse.Core
{
    public class GuardBot : Robot, IScan
    {
        public GuardBot(string name, int battery) : base(name, battery)
        {
        }

        public string Scan()
        {
            if (Battery < 5)
            {
                return Name + " has not enough battery";
            }

            UseBattery(5);
            return Name + " is scanning for enemies";
        }

        public override string CrazyAction()
        {
            if (Battery < 10)
            {
                return Name + " has not enough battery";
            }

            UseBattery(10);

            if (Battery < 20)
            {
                return Name + " is scared and runs away";
            }

            return Name + " is fighting an invisible enemy";
        }
    }
}
