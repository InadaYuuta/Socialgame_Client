using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackManager : MonoBehaviour
{
    public void TitleBack()
    {
        FadeManager.Instance.LoadScene("TestScene");
    }
}
