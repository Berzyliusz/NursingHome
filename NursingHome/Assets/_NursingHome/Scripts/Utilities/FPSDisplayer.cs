using UnityEngine;
using TMPro;

/// <summary>
/// Displays FPS without Garbage Collection.
/// </summary>
public class FPSDisplayer : MonoBehaviour
{
    [SerializeField] KeyCode key = KeyCode.Home;
    [SerializeField] TextMeshProUGUI displayText = null;

    string[] strings;
    float deltaTime = 0.0f;
    bool displayFPS = false;

    void Awake()
    {
        strings = new string[500];

        for (int i = 0; i < 500; i++)
        {
            strings[i] = i.ToString();
        }

        displayFPS = false;
        displayText.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            displayFPS = !displayFPS;
            displayText.enabled = displayFPS;
        }

        if (!displayFPS) { return; }

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        try
        {
            displayText.text = strings[(int)fps];
        }
        catch { }
    }
}