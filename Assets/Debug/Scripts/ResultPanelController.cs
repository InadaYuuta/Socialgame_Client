using System.Collections;
using TMPro;
using UnityEngine;

public class ResultPanelController : MonoBehaviour
{
    static GameObject resultPanel, communicationPanel;
    static TextMeshProUGUI resultText;

    public static ResultPanelController Instance { get; private set; }

    void Awake()
    {
        // シングルトンにする
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
        communicationPanel = GameObject.Find("CommunicationPanel");
        resultText = GameObject.Find("ResultText").GetComponent<TextMeshProUGUI>();
        resultText.text = "";
        resultPanel.SetActive(false);
        HideCommunicationPanel();
    }

    // 通信中を表示
    public static void DisplayCommunicationPanel()
    {
        communicationPanel.SetActive(true);
    }

    // 通信中を非表示に
    public static void HideCommunicationPanel()
    {
        communicationPanel.SetActive(false);
    }

    // 何かしらのメッセージを表示するときに呼び出す
    public static IEnumerator DisplayResultPanel(string resultStr)
    {
        resultText.text = resultStr;
        resultPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        resultPanel.SetActive(false);
        yield return null;
    }
}
