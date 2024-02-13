using TMPro;
using UnityEngine;

public class StaminaShopTextManager : MonoBehaviour
{
    int free_currency, paid_currency, item_num;
    string currencyStr = "通貨5を消費してスタミナを全回復する。\r\n所持通貨:";
    string itemStr = "スタミナ回復アイテムを消費してスタミナを全回復する。\r\n所持数:";
    [SerializeField] TextMeshProUGUI currencyText, itemText;

    void Start()
    {
        free_currency = Wallets.Get().free_amount;
        paid_currency = Wallets.Get().paid_amount;
    }

    // TODO: スタミナを更新したタイミングのみWallets情報を取得し直すように修正する
    void Update()
    {
        free_currency = Wallets.Get().free_amount;
        paid_currency = Wallets.Get().paid_amount;
        ChangeTexts();
    }

    void ChangeTexts()
    {
        string resultCurrencyStr = string.Format("{0}{1}個", currencyStr, free_currency + paid_currency);
        string resultItemStr = string.Format("{0}{1}個", itemStr, Items.GetItemData(10001).item_num);
        currencyText.text = resultCurrencyStr;
        itemText.text = resultItemStr;
    }
}
