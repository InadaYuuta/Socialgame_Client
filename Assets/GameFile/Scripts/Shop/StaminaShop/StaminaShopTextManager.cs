using TMPro;
using UnityEngine;

public class StaminaShopTextManager : MonoBehaviour
{
    int free_currency, paid_currency, item_num;
    [SerializeField] TextMeshProUGUI currencyText, itemText;

    void Start() => ChangeTexts();

    void Update() => ChangeTexts();

    void ChangeTexts()
    {
        free_currency = Wallets.Get().free_amount;
        paid_currency = Wallets.Get().paid_amount;
        int total_currency = free_currency + paid_currency;
        currencyText.text = total_currency.ToString();
        itemText.text = Items.GetItemData(10001).item_num.ToString();
    }
}
