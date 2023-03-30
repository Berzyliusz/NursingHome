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

            if(ChargesAmount == 0)
            {
                //TODO: Notify UI that we have been all used up
            }
        }
    }
}