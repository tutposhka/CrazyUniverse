namespace CrazyUniverse.Core
{
    public class ChefBot : Robot, IChargeable
    {
        public ChefBot(string name, int battery) : base(name, battery)
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
            return FinishAction(Name + " cooked a pizza using a laser printer");
        }

        protected override string ReactToAction(Robot other)
        {
            return Name + " prepares a snack for " + other.Name;
        }
    }
}
