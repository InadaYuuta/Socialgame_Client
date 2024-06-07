using System.Collections;
using TMPro;
using UnityEngine;

public class ResultPanelController : MonoBehaviour
{
    static GameObject resultPanel;
    static TextMeshProUGUI resultText;

    public static ResultPanelController Instance { get; private set; }

    void Awake()
    {
        // �V���O���g���ɂ���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        resultPanel = GameObject.Find("ResultPanel");
        resultText = GameObject.Find("ResultText").GetComponent<TextMeshProUGUI>();
        resultText.text = "";
        resultPanel.SetActive(false);
    }

    // ��������̃��b�Z�[�W��\������Ƃ��ɌĂяo��
    public static IEnumerator DisplayResultPanel(string resultStr)
    {
        resultText.text = resultStr;
        resultPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        resultPanel.SetActive(false);
        yield return null;
    }
}
