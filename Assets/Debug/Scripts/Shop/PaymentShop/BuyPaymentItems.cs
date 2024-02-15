using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BuyPaymentItems : MonoBehaviour
{
    string buyStr = "çwì¸äÆóπ";
    [SerializeField] GameObject buyButton;
    [SerializeField] TextMeshProUGUI productText;
    [SerializeField] string user_id;
    [SerializeField] string product_id;
    string productStr;

    private void Start()
    {
        user_id = Users.Get().user_id;
        SetProductText();
    }

    public void PushBuyButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel(buyStr));
        buyButton.SetActive(false);
        List<IMultipartFormSection> buyForm = new();
        buyForm.Add(new MultipartFormDataSection("uid", user_id));
        buyForm.Add(new MultipartFormDataSection("pid", product_id));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.BUY_CURRENCY_URL, buyForm, null));
        Invoke("DisplayButton", 1);
    }

    void SetProductText()
    {
        PaymentShopModel product = PaymentShops.GetPaymentShop(int.Parse(product_id));
        productStr = string.Format("è§ïiñº:{0}\n{1}â~\nóLèûï™:{2}å¬\nñ≥èûï™{3}å¬", product.product_name, product.price, product.paid_currency, product.bonus_currency);
        productText.text = productStr;
    }

    void DisplayButton()
    {
        buyButton.SetActive(true);
    }

}
