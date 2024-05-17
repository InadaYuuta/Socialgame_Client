using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class TestTitleManager : MonoBehaviour
{
    static bool isFlag = false;
    void Awake()
    {
        // SQLiteのDBファイル作成
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }

        Users.CreateTable();
        Wallets.CreateTable();
        Items.CreateTable();

        // 各マスターテーブル作成
        ItemsMaster.CreateTable();
        ItemCategories.CreateTable();
        Items.CreateTable();
        ExchangeShopCategories.CreateTable();
        PaymentShops.CreateTable();
        ExchangeShops.CreateTable();
        WeaponMaster.CreateTable();
        WeaponCategories.CreateTable();
        WeaponRarities.CreateTable();
        Weapons.CreateTable();
        GachaWeapons.CreateTable();
        GachaLogs.Createtable();
        WeaponExps.CreateTable();
    }

    private void Update()
    {
        if (Users.Get().user_id != null && !isFlag)
        {
            // マスターデータチェック
           // StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));

            List<IMultipartFormSection> form = new();
            form.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
            // アイテムデータ作成
            // StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.ITEM_REGISTRATION_URL, form, null));
            isFlag = true;
        }
    }

    public void ResetGame()
    {
        // SQLiteのDBファイル作成
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        File.Delete(DBPath);
        FadeManager.Instance.LoadScene("TestScene", 1.0f);
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }
    }

    public void FinishGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
