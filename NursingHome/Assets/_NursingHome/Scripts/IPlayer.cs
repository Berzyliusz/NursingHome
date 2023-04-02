using UnityEngine;

namespace NursingHome
{
    public interface IPlayer
    {
        void SetFreezePlayer(bool isFrozen);

        Vector3 GetPlayerPosition();
        Vector3 GetPlayerAimPosition();
    }
}