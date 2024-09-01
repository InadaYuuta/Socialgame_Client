using UnityEngine;

public class TitleBackManager : MonoBehaviour
{
    bool isFinish = false;

    public void TitleBack()
    {
        if (!isFinish)
        {
            isFinish = true;
            FadeManager.Instance.LoadScene("TitleScene", 0.5f);
        }
    }
}
