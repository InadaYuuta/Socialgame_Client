using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BuyPaymentItems : MonoBehaviour
{
    string buyStr = "�w������";
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
    }

    void SetProductText()
    {
        PaymentShopModel product = PaymentShops.GetPaymentShop(int.Parse(product_id));
        productStr = string.Format("���i��:{0}\n{1}�~\n�L����:{2}��\n������{3}��", product.product_name, product.price, product.paid_currency, product.bonus_currency);
        productText.text = productStr;
    }
}
