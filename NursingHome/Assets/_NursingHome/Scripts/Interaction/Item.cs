namespace NursingHome.Interactions
{
    public class Item
    {
        public uint ChargesAmount { get; private set; }
        public ItemParams ItemParams { get; }

        public Item(uint chargesAmount, ItemParams itemParams)
        {
            ChargesAmount = chargesAmount;
            ItemParams = itemParams;
        }

        public void UseCharge()
        {
            ChargesAmount--;
        }
    }
}