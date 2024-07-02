using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentManager : MonoBehaviour
{
    [SerializeField] GameObject pushPanel;
    [SerializeField] TextMeshProUGUI reasonText, numText, receiptTermText;
    [SerializeField] Image presentImage;

    string reasonStr, numStr, receiptTermStr;
    string multiStr = "�~{0}";
    string termStr = "������\n{0}";

    void Awake()
    {
        pushPanel.SetActive(false);
    }

    // �v���[���g�̃e�L�X�g���̐ݒ�
    public void SetPresentParameter(string reason, string num, string receiptTerm, int rewardCategory)
    {
        reasonStr = reason;

        numStr = string.Format(multiStr, num.Replace("/", ""));
        receiptTermStr = string.Format(termStr, receiptTerm);

        reasonText.text = reasonStr;
        numText.text = numStr;
        receiptTermText.text = receiptTermStr;
        presentImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
    }

    // �v���[���g�{�^���������ꂽ��
    public void OnPushPresentButton()
    {
        pushPanel.SetActive(true);
    }

    // �󂯎��{�^���������ꂽ��
    public void OnpushReceiptButton()
    {

    }

    // ��߂�{�^���������ꂽ��
    public void OnPushCancelButton()
    {
        pushPanel.SetActive(false);
    }

}
