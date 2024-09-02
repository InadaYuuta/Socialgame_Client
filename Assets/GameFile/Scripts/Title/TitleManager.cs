using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class TitleManager : MonoBehaviour
{
    static bool isFlag = false;
    void Awake()
    {
        // SQLite��DB�t�@�C���쐬
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
            // �}�X�^�[�f�[�^�`�F�b�N
             StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));
            isFlag = true;
        }
    }

    // �e�[�u���쐬�̏������Ăяo��
    void CreateTables()
    {
        // �C���X�^���X�e�[�u��
        Users.CreateTable();
        Wallets.CreateTable();
        Items.CreateTable();
        Weapons.CreateTable();
        PresentBoxes.CreateTable();
        Missions.CreateTable();

        // �e�}�X�^�[�e�[�u��
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

        // �K�`��
        GachaLogs.Createtable();
        GachaWeapons.CreateTable();

        // ���O�e�[�u��
        LogCategories.CreateTable();
        LogMaster.CreateTable();
    }

    public void ResetGame()
    {
        // SQLite��DB�t�@�C���쐬
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
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
