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
    string multiStr = "�~{0}";
    string termStr = "����\n{0}";

    // �v���[���g�̃e�L�X�g���̐ݒ�
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

        presentImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
    }
}
