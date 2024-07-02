using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI reasonText, numText, receiptTermText;
    [SerializeField] Image presentImage;

    int present_id = -1;
    string reasonStr, numStr, receiptTermStr;
    string multiStr = "×{0}";
    string termStr = "受取期限\n{0}";

    ReceivePresent receivePresent;

    void Awake()
    {
        receivePresent = FindObjectOfType<ReceivePresent>();
    }

    // プレゼントのテキスト等の設定
    public void SetPresentParameter(int id, string reason, string num, string receiptTerm, int rewardCategory)
    {
        reasonStr = reason;

        numStr = string.Format(multiStr, num.Replace("/", ""));
        receiptTermStr = string.Format(termStr, receiptTerm);

        present_id = id;
        reasonText.text = reasonStr;
        numText.text = numStr;
        receiptTermText.text = receiptTermStr;
        presentImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resourcesフォルダの中の特定の画像を取得して入れる
    }

    // プレゼントボタンが押された時
    public void OnPushPresentButton()
    {
        receivePresent.DisplayCheckReceivePanel();
        receivePresent.SetReceivePresentId(present_id);
    }
}
