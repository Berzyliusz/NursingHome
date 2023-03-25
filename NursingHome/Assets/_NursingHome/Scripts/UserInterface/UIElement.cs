using UnityEngine;

namespace NursingHome.UserInterface
{
    public abstract class UIElement : MonoBehaviour
    {
        public abstract UIType Type { get; }
        public abstract void Show();
        public abstract void Hide();
        public abstract void Update();
    }
}