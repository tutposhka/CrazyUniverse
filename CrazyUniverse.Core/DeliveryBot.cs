namespace CrazyUniverse.Core
{
    public class DeliveryBot : Robot, IScan, IChargeable
    {
        public DeliveryBot(string name, int battery) : base(name, battery)
        {
        }

        public string Scan()
        {
            if (Battery < 5)
            {
                return Name + " has not enough battery";
            }

            UseBattery(5);
            return Name + " scanned the delivery address";
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
            return FinishAction(Name + " delivered a package to the Moon in ten seconds");
        }

        protected override string ReactToAction(Robot other)
        {
            return Name + " delivers spare parts to " + other.Name;
        }
    }
}
