using UnityEngine;
using UnityEngine.UI;

public class InputAndButtonHandler : MonoBehaviour
{
    public InputField inputField;
    public Button submitButton;
    private string inputValue;

    void Start()
    {
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(HandleButtonClick);
        }
        else
        {
            Debug.LogError("Submit Button not assigned");
        }
    }

    void HandleButtonClick()
    {
        if (inputField != null)
        {
            inputValue = inputField.text;
            Debug.Log("Input value when button clicked: " + inputValue);
            // 這裡你可以添加更多代碼來處理輸入的值
        }
        else
        {
            Debug.LogError("InputField not assigned");
        }
    }
}