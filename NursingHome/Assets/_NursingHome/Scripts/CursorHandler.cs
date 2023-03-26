using UnityEngine;

namespace NursingHome
{
    public interface ICursor
    {
        void SetCursorVisible(bool visible);
        void SetCursorLocked(CursorLockMode lockMode);
    }

    public class CursorHandler : ICursor
    {
        public void SetCursorLocked(CursorLockMode lockMode)
        {
            Cursor.lockState = lockMode;
        }

        public void SetCursorVisible(bool isVisible)
        {
            Cursor.visible = isVisible;
        }
    }
}