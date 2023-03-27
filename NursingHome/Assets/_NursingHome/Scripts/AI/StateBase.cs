using UnityEngine;

namespace NursingHome.AI
{
    public class StateBase : MonoBehaviour
    {
        protected AI ai;

        public void SetupAI(AI ai)
        {
            this.ai = ai;
        }
    }
}