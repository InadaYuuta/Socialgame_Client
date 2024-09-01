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

        CreateTables();
    }

    private void Update()
    {
        if (Users.Get().user_id != null && !isFlag)
        {
            // マスターデータチェック
             StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));
            isFlag = true;
        }
    }

    // テーブル作成の処理を呼び出す
    void CreateTables()
    {
        // インスタンステーブル
        Users.CreateTable();
        Wallets.CreateTable();
        Items.CreateTable();
        Weapons.CreateTable();
        PresentBoxes.CreateTable();
        Missions.CreateTable();

        // 各マスターテーブル
        ItemsMaster.CreateTable();
        ItemCategories.CreateTable();
        Items.CreateTable();
        ExchangeShopCategories.CreateTable();
        PaymentShops.CreateTable();
        ExchangeShops.CreateTable();
        WeaponMaster.CreateTable();
        WeaponCategories.CreateTable();
        WeaponRarities.CreateTable();
        WeaponExps.CreateTable();
        EvolutionWeapons.CreateTable();
        MissionMaster.CreateTable();
        MissionCategories.CreateTable();
        NewsMaster.CreateTable();
        RewardCategories.CreateTable();

        // ガチャ
        GachaLogs.Createtable();
        GachaWeapons.CreateTable();

        // ログテーブル
        LogCategories.CreateTable();
        LogMaster.CreateTable();
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
