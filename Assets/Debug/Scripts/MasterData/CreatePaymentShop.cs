using UnityEngine;

public class CreatePaymentShop : MonoBehaviour
{
    PaymentShopModel paymentShopModel;

    void Start()
    {
        if (paymentShopModel == null)
        {
            PaymentShops.CreateTable();
            // paymentShopModel = PaymentShops.GetPaymentShop(null);
            GetPaymentShop();
        }
    }

    public void GetPaymentShop()
    {
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));
    }
}
