using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestTitleManager : MonoBehaviour
{
    void Awake()
    {
        // 各マスターテーブル作成
        ItemsMaster.CreateTable();
        ItemCategories.CreateTable();
        ExchangeShopCategories.CreateTable();
        PaymentShops.CreateTable();
        ExchangeShops.CreateTable();
    }

    void Start()
    {
        // マスターデータチェック
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));

        List<IMultipartFormSection> form = new();
        form.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        // アイテムデータ作成
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.ITEM_REGISTRATION_URL, form, null));
    }
}
