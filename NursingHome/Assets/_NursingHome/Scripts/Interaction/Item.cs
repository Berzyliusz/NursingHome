namespace NursingHome.Interactions
{
    public class Item
    {
        public uint ChargesAmount { get; private set; }
        public uint MaxChargesAmount { get; private set; }
        public string DisplayName { get; private set; }
        public string AlternativeName { get; private set; }
        public PrankParams[] PrankParams { get; private set; }
        public bool IsUsedUp => ChargesAmount <= 0;

        public Item(uint chargesAmount, ItemParams itemParams)
        {
            MaxChargesAmount = chargesAmount;
            ChargesAmount = chargesAmount;
            DisplayName = itemParams.ItemName;
            AlternativeName = itemParams.AlternativeName;
            PrankParams = itemParams.PrankParams;
        }

        public void UseCharge()
        {
            ChargesAmount--;
        }
    }
}