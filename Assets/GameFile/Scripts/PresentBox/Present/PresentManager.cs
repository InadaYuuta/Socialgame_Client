using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI reasonText, numText, receiptTermText;
    [SerializeField] Image presentImage;

    int present_id = -1;
    public int Present_Id { get { return present_id; } }
    int rewardCategory, presentNum;
    string reasonStr, numStr, receiptTermStr;
    string multiStr = "�~{0}";
    string termStr = "������\n{0}";

    ReceivePresent receivePresent;

    void Awake()
    {
        receivePresent = FindObjectOfType<ReceivePresent>();
    }

    // �v���[���g�̃e�L�X�g���̐ݒ�
    public void SetPresentParameter(int id, string reason, string num, string receiptTerm, int category)
    {
        reasonStr = reason;


        numStr = string.Format(multiStr, num.Replace("/", ""));
        receiptTermStr = string.Format(termStr, receiptTerm);


        present_id = id;
        reasonText.text = reasonStr;
        numText.text = numStr;
        receiptTermText.text = receiptTermStr;

        presentNum = int.Parse(num.Replace("/", ""));
        rewardCategory = category;

        presentImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
    }

    // �v���[���g�{�^���������ꂽ��
    public void OnPushPresentButton()
    {
        receivePresent.DisplayCheckReceivePanel();
        receivePresent.SetReceivePresentId(this.gameObject, present_id, rewardCategory, presentNum);
    }
}
