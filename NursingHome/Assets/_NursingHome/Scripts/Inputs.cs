using UnityEngine;

namespace NursingHome
{
    public interface IInputs
    {
        bool Use { get; }
        float MouseScroll { get; }
    }

    public class Inputs : IInputs
    {
        public bool Use => Input.GetKeyDown(KeyCode.E);
        public float MouseScroll => Input.mouseScrollDelta.y;
    }
}