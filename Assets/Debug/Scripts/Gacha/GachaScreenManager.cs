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
    }

    // ガチャ排出確率ボタンが押されたら
    public void PushGachaEmissionProbabilityButton()
    {
        ePPanel.SetActive(true);
    }

    // ガチャ履歴ボタンが押されたら
    public void PushGachaLogButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }

    // 単発ガチャボタンが押されたら
    public void PushSingleGachaButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }

    // 十連ガチャボタンが押されたら
    public void PushMultiGachaButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }
}
