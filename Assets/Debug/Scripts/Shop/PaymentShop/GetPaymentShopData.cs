using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPaymentShopData : MonoBehaviour
{
    PaymentShopModel model;

    [SerializeField] TextMeshProUGUI payment1Text;

    int paid_currency, bonus_currency;

    void Start()
    {
        //model = PaymentShops.Get();
        var b = PaymentShops.GetPaymentShop("10001");
        Debug.Log(b.product_name);


        PaymentShopModel[] list = PaymentShops.GetPaymentShops();
        int count = 0;
        foreach (var element in list)
        {
            if (count > 0)
            {
                PaymentShopModel paymentModel = list[count];
                string a = string.Format("product_id:{0}", paymentModel.product_id);
                Debug.Log(a);
            }
            count++;
        }
        // TODO:�G���L�x�A�O�̃N�G�X�g�̂�Q�l�ɂ���id���Ƃɏ��i�𐶐����鏈�����쐬����
    }
}
