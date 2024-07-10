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

    void Start()
    {
        gachaCanvas.SetActive(false);
        UpdateNum();
        gachaMoveManager = FindObjectOfType<GachaMove>();
        gachaLogManager = FindObjectOfType<GachaLogManager>();
        bagSortManager = FindObjectOfType<BagSortManager>();
    }

    void FixedUpdate() => UpdateNum();

    // アイテム数などを更新
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

    // ガチャボタンが押されたら
    public void PushGachaButton()
    {
        gachaCanvas.SetActive(true);
    }

    // 戻るボタンが押されたら
    public void PushBackGachaButton()
    {
        gachaCanvas.SetActive(false);
        bagSortManager.UpdateBag();
    }

    // ガチャ排出確率ボタンが押されたら
    public void PushGachaEmissionProbabilityButton()
    {
        ePPanel.SetActive(true);
    }

    // ガチャ履歴ボタンが押されたら
    public void PushGachaLogButton()
    {
        gachaLogManager.GetGachaLog();
        gachaLogManager.UpdateText();
        gachaLogPanel.SetActive(true);
    }

    // 単発ガチャボタンが押されたら
    public void PushSingleGachaButton()
    {
        if (Wallets.Get().free_amount + Wallets.Get().paid_amount > 0)
        {
            gachaMoveManager.SingleMove();
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel("ジェムが足りない!"));
        }
    }

    // 十連ガチャボタンが押されたら
    public void PushMultiGachaButton()
    {
        gachaMoveManager.MultiMove();
    }
}
