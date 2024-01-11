using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestStartManager : MonoBehaviour
{
    public void GameStart()
    {
       FadeManager.Instance.LoadScene("MyPageScene");
    }
}
