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
        // シングルトンにする
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }
        resultPanel = GameObject.Find("ResultPanel");
        resultText = GameObject.Find("ResultText").GetComponent<TextMeshProUGUI>();
        resultText.text = "";
        resultPanel.SetActive(false);
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
