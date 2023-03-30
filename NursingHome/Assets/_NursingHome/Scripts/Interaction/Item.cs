namespace NursingHome.Interactions
{
    public class Item
    {
        public uint ChargesAmount { get; }
        public ItemParams ItemParams { get; }

        public Item(uint chargesAmount, ItemParams itemParams)
        {
            ChargesAmount = chargesAmount;
            ItemParams = itemParams;
        }
    }
}