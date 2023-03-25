using UnityEngine;

namespace NursingHome.Interactions
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField]
        InteractionDetector interactionDetector;

        void Update()
        {
            //TODO:
            // Update only when we need to]
            // get inputs from systems
            if(interactionDetector.SelectedItem == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactionDetector.SelectedItem.Pickup();
            }
        }
    }
}