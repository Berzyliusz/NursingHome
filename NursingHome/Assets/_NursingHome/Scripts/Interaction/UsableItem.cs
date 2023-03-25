namespace NursingHome.Interactions
{
    public class UsableItem : InteractableItem
    {
        public override string DisplayName => ItemParams.ItemName;

        public override ItemParams ItemParams => throw new System.NotImplementedException();

        public override void UseItem()
        {
            
        }
    }
}