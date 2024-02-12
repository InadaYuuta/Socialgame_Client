using UnityEngine;

public class TitleBackManager : MonoBehaviour
{
    bool isFinish = false;

    public void TitleBack()
    {
        if (!isFinish)
        {
            isFinish = true;
            FadeManager.Instance.LoadScene("TestScene", 0.5f);
        }
    }
}
