using UnityEngine;

namespace NursingHome.Interactions
{
    public abstract class ItemParams : ScriptableObject
    {
        [field:SerializeField] 
        public string ItemName { get; private set; }


    }
}