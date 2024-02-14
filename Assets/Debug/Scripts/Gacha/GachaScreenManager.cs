using TMPro;
using UnityEngine;

public class GachaScreenManager : MonoBehaviour
{
    [SerializeField] GameObject gachaCanvas, ePPanel;
    [SerializeField] TextMeshProUGUI amountText, fragmentText;

    int amountNum;
    int fragmentItemNum;

    void Start()
    {
        gachaCanvas.SetActive(false);
        UpdateNum();
    }

    void Update()
    {
    }

    void FixedUpdate() => UpdateNum();

    // �A�C�e�����Ȃǂ��X�V
    void UpdateNum()
    {
        amountNum = Wallets.Get().free_amount + Wallets.Get().paid_amount;
        fragmentItemNum = Items.GetItemData(30001).item_num;

        if (amountText != null && fragmentText != null)
        {
            amountText.text = amountNum.ToString();
            fragmentText.text = fragmentItemNum.ToString();
        }
    }

    // �K�`���{�^���������ꂽ��
    public void PushGachaButton()
    {
        gachaCanvas.SetActive(true);
    }

    // �߂�{�^���������ꂽ��
    public void PushBackGachaButton()
    {
        gachaCanvas.SetActive(false);
    }

    // �K�`���r�o�m���{�^���������ꂽ��
    public void PushGachaEmissionProbabilityButton()
    {
        ePPanel.SetActive(true);
    }

    // �K�`�������{�^���������ꂽ��
    public void PushGachaLogButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }

    // �P���K�`���{�^���������ꂽ��
    public void PushSingleGachaButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }

    // �\�A�K�`���{�^���������ꂽ��
    public void PushMultiGachaButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }
}
