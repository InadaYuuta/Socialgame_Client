using TMPro;
using UnityEngine;

public class StaminaShopTextManager : MonoBehaviour
{
    int free_currency, paid_currency, item_num;
    string currencyStr = "�ʉ�5������ăX�^�~�i��S�񕜂���B\r\n�����ʉ�:";
    string itemStr = "�X�^�~�i�񕜃A�C�e��������ăX�^�~�i��S�񕜂���B\r\n������:";
    [SerializeField] TextMeshProUGUI currencyText, itemText;

    void Start()
    {
        free_currency = Wallets.Get().free_amount;
        paid_currency = Wallets.Get().paid_amount;
    }

    // TODO: �X�^�~�i���X�V�����^�C�~���O�̂�Wallets�����擾�������悤�ɏC������
    void Update()
    {
        free_currency = Wallets.Get().free_amount;
        paid_currency = Wallets.Get().paid_amount;
        ChangeTexts();
    }

    void ChangeTexts()
    {
        string resultCurrencyStr = string.Format("{0}{1}��", currencyStr, free_currency + paid_currency);
        // string resultItemStr = string.Format("{0}{1}��", itemStr);
        currencyText.text = resultCurrencyStr;
        //itemText.text = resultItemStr;
    }
}
