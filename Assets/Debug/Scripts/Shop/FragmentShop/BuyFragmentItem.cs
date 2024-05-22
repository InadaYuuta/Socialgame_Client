using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BuyFragmentItem : MonoBehaviour
{
    string buyStr = "購入完了";
    string cantBuyStr = "交換アイテムが足りない";
    [SerializeField] GameObject buyButton;
    [SerializeField] TextMeshProUGUI productText;
    string user_id;
    [SerializeField] string exchange_product_id;
    string productStr;

    void Start()
    {
        user_id = Users.Get().user_id;
        SetProductText();
    }

    // 交換アイテムが必要数あれば購入、そうでなければパネルを表示
    public void PushBuyButton()
    {
        int exchangeItemNum = Items.GetItemData(30001).item_num;
        if (exchangeItemNum > 10)
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel(buyStr));
            buyButton.SetActive(false);
            List<IMultipartFormSection> buyForm = new();
            buyForm.Add(new MultipartFormDataSection("uid", user_id));
            buyForm.Add(new MultipartFormDataSection("epid", exchange_product_id));
            StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.BUY_EXCHANGE_SHOP_URL, buyForm, null));
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel(cantBuyStr));
        }

    }

    void SetProductText()
    {
        ExchangeShopModel product = ExchangeShops.GetExchangeShopData(int.Parse(exchange_product_id));
        productStr = string.Format("{0}\n必要アイテム数{1}個", product.exchange_item_name, product.exchange_price);
        productText.text = productStr;
    }
}
