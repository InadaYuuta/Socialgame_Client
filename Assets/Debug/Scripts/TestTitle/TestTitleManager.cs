using UnityEngine;

public class TestTitleManager : MonoBehaviour
{
    ItemCategoryModel itemCategoryModel;
    PaymentShopModel paymentShopModel;

    void Awake()
    {
        if(itemCategoryModel == null)
        {
        //ItemCategories.CreateTable();
        }
        if(paymentShopModel == null)
        {
        PaymentShops.CreateTable();
        }
    }

    void Start()
    {
        // �}�X�^�[�f�[�^�`�F�b�N
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));
    }
}
