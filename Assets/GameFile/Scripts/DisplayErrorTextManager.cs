using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayErrorTextManager : MonoBehaviour
{
    public static DisplayErrorTextManager Instance { get; private set; }

    [SerializeField] GameObject ErrorCanvas;
    [SerializeField] TextMeshProUGUI errorText;

    string errorCode = "ERRORCODE:";

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
    }

    private void Start() => CloseErrorCanvas();

    // エラーメッセージを表示する　
    public void DisplayError(string errorMessage)
    {
        ErrorCanvas.SetActive(true);
        errorText.text = string.Format("{0}{1}", errorCode, errorMessage);
    }

    // エラーキャンバスを非表示にする
    public void CloseErrorCanvas()
    {
        ErrorCanvas.SetActive(false);
    }

    // タイトルに戻る
    public void TitleBack()
    {
        StartCoroutine(WaitCloseErrorCanvas());
        FadeManager.Instance.LoadScene("TitleScene");
    }

    // TODO: 今後もう一度処理を試すようなコードを追加する

    IEnumerator WaitCloseErrorCanvas()
    {
        yield return new WaitForSeconds(3f);
        ErrorCanvas.SetActive(false);
    }
}
