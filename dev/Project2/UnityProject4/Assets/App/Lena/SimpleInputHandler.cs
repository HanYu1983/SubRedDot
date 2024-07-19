using UnityEngine;
using UnityEngine.UI;

public class SimpleInputHandler : MonoBehaviour
{
    private InputField inputField;

    void Start()
    {
        inputField = GetComponent<InputField>();
        if (inputField != null)
        {
            inputField.onEndEdit.AddListener(HandleInput);
        }
        else
        {
            Debug.LogError("InputField not found on this GameObject");
        }
    }

    void HandleInput(string value)
    {
        Debug.Log("Input received: " + value);
    }
}