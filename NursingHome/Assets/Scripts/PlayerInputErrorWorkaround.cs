using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputErrorWorkaround : MonoBehaviour
{
    PlayerInput Input;

    void Awake()
    {
        Input = GetComponent<PlayerInput>();
    }

    void OnDisable()
    {
        Input.actions = null;
    }
}