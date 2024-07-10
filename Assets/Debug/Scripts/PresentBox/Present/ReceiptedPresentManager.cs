using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReceiptedPresentManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI reasonText, numText, receiptedText;
    [SerializeField] Image presentImage;

    int present_id = -1;
    public int Present_Id { get { return present_id; } }
    int rewardCategory;
    string reasonStr, numStr, receiptedStr;
    string multiStr = "×{0}";
    string termStr = "受取日\n{0}";

    // プレゼントのテキスト等の設定
    public void SetPresentParameter(int id, string reason, string num, string receiptedTerm, int category)
    {
        reasonStr = reason;

        numStr = string.Format(multiStr, num.Replace("/", ""));
        receiptedStr = string.Format(termStr, receiptedTerm);

        present_id = id;
        reasonText.text = reasonStr;
        numText.text = numStr;
        receiptedText.text = receiptedStr;

        rewardCategory = category;

        presentImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resourcesフォルダの中の特定の画像を取得して入れる
    }
}
