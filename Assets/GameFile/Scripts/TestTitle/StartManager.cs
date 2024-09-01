using UnityEngine;

public class StartManager : MonoBehaviour
{
    public void GameStart()
    {
       FadeManager.Instance.LoadScene("MyPageScene");
    }
}
