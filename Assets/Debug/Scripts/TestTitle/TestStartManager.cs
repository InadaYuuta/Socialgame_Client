using UnityEngine;

public class TestStartManager : MonoBehaviour
{
    public void GameStart()
    {
       FadeManager.Instance.LoadScene("MyPageScene");
    }
}
