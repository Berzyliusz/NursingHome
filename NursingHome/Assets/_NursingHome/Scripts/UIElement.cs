using UnityEngine;

namespace NursingHome.UserInterface
{
    public abstract class UIElement : MonoBehaviour
    {
        public abstract UIType Type { get; }
    }
}