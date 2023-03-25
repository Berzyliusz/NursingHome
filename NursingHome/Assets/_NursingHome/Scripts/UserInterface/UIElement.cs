using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public abstract class UIElement : MonoBehaviour
    {
        [SerializeField]
        [SceneObjectsOnly]
        protected GameObject parent;

        public abstract UIType Type { get; }
        public abstract void Show();
        public abstract void Hide();
        public abstract void UpdateUI(UIParams uiParams);
    }
}