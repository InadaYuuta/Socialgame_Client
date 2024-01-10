using UnityEngine;
using UnityEngine.InputSystem;

public class TestStartManager : MonoBehaviour
{
    void Update()
    {
        var a = Keyboard.current;
        var b = a.digit2Key.wasPressedThisFrame;
        if(b)
        {
            FadeManager.Instance.LoadScene("MyPageScene");
        }
    }
}
