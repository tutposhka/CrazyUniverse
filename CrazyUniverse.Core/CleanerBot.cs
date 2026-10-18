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

            return FinishAction(
                Name + " cleaned the whole city with a tiny toothbrush");
        }

        protected override string ReactToAction(Robot other)
        {
            return Name + " cleans the mess after " + other.Name;
        }
    }
}