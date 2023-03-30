namespace NursingHome.Interactions
{
    public class Item
    {
        public uint ChargesAmount { get; private set; }
        public uint MaxChargesAmount { get; private set; }
        public string DisplayName { get; private set; }
        public string AlternativeName { get; private set; }
        public ItemParams ItemParams { get; }
        public bool IsUsedUp => ChargesAmount <= 0;

        public Item(uint chargesAmount, ItemParams itemParams)
        {
            MaxChargesAmount = chargesAmount;
            ChargesAmount = chargesAmount;
            ItemParams = itemParams;
            DisplayName = itemParams.ItemName;
            AlternativeName = itemParams.AlternativeName;
        }

        public void UseCharge()
        {
            ChargesAmount--;
        }
    }
}