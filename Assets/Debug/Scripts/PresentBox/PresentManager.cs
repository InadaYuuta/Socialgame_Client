using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentManager : MonoBehaviour
{
    [SerializeField] GameObject pushPanel;
    [SerializeField] TextMeshProUGUI reasonText, numText, receiptTermText;
    [SerializeField] Image presentImage;

    string reasonStr, numStr, receiptTermStr;
    string multiStr = "×{0}";
    string termStr = "受取期限\n{0}";

    void Awake()
    {
        pushPanel.SetActive(false);
    }

    // プレゼントのテキスト等の設定
    public void SetPresentParameter(string reason, string num, string receiptTerm, int rewardCategory)
    {
        reasonStr = reason;

        numStr = string.Format(multiStr, num.Replace("/", ""));
        receiptTermStr = string.Format(termStr, receiptTerm);

        reasonText.text = reasonStr;
        numText.text = numStr;
        receiptTermText.text = receiptTermStr;
        presentImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resourcesフォルダの中の特定の画像を取得して入れる
    }

    // プレゼントボタンが押された時
    public void OnPushPresentButton()
    {
        pushPanel.SetActive(true);
    }

    // 受け取りボタンが押された時
    public void OnpushReceiptButton()
    {

    }

    // やめるボタンが押された時
    public void OnPushCancelButton()
    {
        pushPanel.SetActive(false);
    }

}
