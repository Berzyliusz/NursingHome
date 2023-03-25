using UnityEngine;

namespace NursingHome.Interactions
{
    [CreateAssetMenu(menuName = "NursingHome/ItemParams")]
    public class ItemParams : ScriptableObject
    {
        [field:SerializeField] 
        public string ItemName { get; private set; }

        // Hold params for:
        // What pranks are available with this item?
        // Severity of this iteam being found upon us
    }
}