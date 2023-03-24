using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] float rayLength = 0.5f;
    [SerializeField] LayerMask layerMask;
    
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, layerMask))
        {
            // if so, store it as our current selection
            Debug.Log("Raycast hit: " + hit.transform.name);
        }
        else
        {
            Debug.Log("Nothing hit");
        }
    }
}
