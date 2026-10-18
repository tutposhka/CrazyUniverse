namespace CrazyUniverse.Core
{
    public class MedicBot : Robot, IChargeable, IRepair
    {
        public MedicBot(string name, int battery) : base(name, battery)
        {
        }

        public string Charge()
        {
            AddBattery(20);
            return Name + " charged 20%";
        }

        public string Repair(Robot robot)
        {
            if (Battery < 10)
            {
                return Name + " has not enough battery";
            }

            UseBattery(10);
            robot.AddBattery(15);
            return Name + " restored 15% battery to " + robot.Name;
        }

        public override string CrazyAction()
        {
            if (Battery < 12)
            {
                return Name + " has not enough battery";
            }

            UseBattery(12);
            return FinishAction(Name + " cured a computer virus with a digital bandage");
        }

        protected override string ReactToAction(Robot other)
        {
            return Name + " checks the battery health of " + other.Name;
        }
    }
}
