using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestTitleManager : MonoBehaviour
{
    void Awake()
    {
        // �e�}�X�^�[�e�[�u���쐬
        ItemsMaster.CreateTable();
        ItemCategories.CreateTable();
        ExchangeShopCategories.CreateTable();
        PaymentShops.CreateTable();
        ExchangeShops.CreateTable();
        WeaponsMaster.CreateTable();
        WeaponCategories.CreateTable();
        WeaponRarities.CreateTable();
        Weapons.CreateTable();
        GachaWeapons.CreateTable();
    }

    void Start()
    {
        // �}�X�^�[�f�[�^�`�F�b�N
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));

        List<IMultipartFormSection> form = new();
        form.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        // �A�C�e���f�[�^�쐬
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.ITEM_REGISTRATION_URL, form, null));
    }
}
