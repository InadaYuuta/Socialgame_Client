using TMPro;
using UnityEngine;

public class GachaScreenManager : MonoBehaviour
{
    [SerializeField] GameObject gachaCanvas, ePPanel, gachaLogPanel;
    [SerializeField] TextMeshProUGUI amountText, fragmentText;

    int amountNum;
    int fragmentItemNum;

    GachaMove gachaMoveManager;
    GachaLogManager gachaLogManager;
    BagSortManager bagSortManager;

    HomeManager homeManager;

    void Start()
    {
        gachaCanvas.SetActive(false);
        UpdateNum();
        gachaMoveManager = FindObjectOfType<GachaMove>();
        gachaLogManager = FindObjectOfType<GachaLogManager>();
        bagSortManager = FindObjectOfType<BagSortManager>();
        homeManager = FindObjectOfType<HomeManager>();
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
        homeManager.GetHomeData();
        gachaCanvas.SetActive(true);
    }

    // �߂�{�^���������ꂽ��
    public void PushBackGachaButton()
    {
        homeManager.GetHomeData();
        gachaCanvas.SetActive(false);
        bagSortManager.UpdateBag();
    }

    // �K�`���r�o�m���{�^���������ꂽ��
    public void PushGachaEmissionProbabilityButton()
    {
        ePPanel.SetActive(true);
    }

    // �K�`�������{�^���������ꂽ��
    public void PushGachaLogButton()
    {
        gachaLogManager.GetGachaLog();
        gachaLogManager.UpdateText();
        gachaLogPanel.SetActive(true);
    }

    // �P���K�`���{�^���������ꂽ��
    public void PushSingleGachaButton()
    {
        if (Wallets.Get().free_amount + Wallets.Get().paid_amount > 0)
        {
            gachaMoveManager.SingleMove();
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel("�W�F��������Ȃ�!"));
        }
    }

    // �\�A�K�`���{�^���������ꂽ��
    public void PushMultiGachaButton()
    {
        gachaMoveManager.MultiMove();
    }
}
