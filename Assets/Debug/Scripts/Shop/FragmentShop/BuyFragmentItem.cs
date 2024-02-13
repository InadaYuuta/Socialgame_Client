using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BuyFragmentItem : MonoBehaviour
{
    string buyStr = "�w������";
    string cantBuyStr = "�����A�C�e��������Ȃ�";
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

    // �����A�C�e�����K�v������΍w���A�����łȂ���΃p�l����\��
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
        ExchangeShopModel product = ExchangeShops.GetExchangeShop(int.Parse(exchange_product_id));
        productStr = string.Format("{0}\n�K�v�A�C�e����{1}��", product.exchange_item_name, product.exchange_price);
        productText.text = productStr;
    }
}
