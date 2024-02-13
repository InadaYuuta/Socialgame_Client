using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RegistrationController : UsersBase
{
    [SerializeField] TMP_InputField input;

    [SerializeField] string name = "tanaka";

    [SerializeField] TextMeshProUGUI uuidTex;
    string uuid;

    LoginChecker loginChecker;

    [SerializeField] GameObject registerUI;
    [SerializeField] GameObject startUI;

    ItemsModel[] itemsModel;

    private void Awake()
    {
        base.Awake();
        loginChecker = FindObjectOfType<LoginChecker>();

        string deviceId = string.Format("デバイスのユニークID:{0}", SystemInfo.deviceUniqueIdentifier);
        Debug.Log(deviceId);

        CreateSQLiteFile();
        CheckCreateTables();

        if (!string.IsNullOrEmpty(usersModel.user_id))
        {
            loginChecker.OnLoginFlag();
        }

        registerUI = GameObject.Find("RegisterUI");
        startUI = GameObject.Find("StartUI");

        if(Items.GetItemData(10001).item_id != null)
        {
            itemsModel = Items.GetItemDataAll();
        }
    }

    private void Start()
    {
        GetUserID();
        DisplayUI();
    }

    private void Update()
    {
        base.Update();
        GetUserID();
        DisplayUI();
    }

    // ログインしていればスタート用のUIをしていなければ登録用のUIを表示
    void DisplayUI()
    {
        if (!registerUI || !startUI) { return; }
        if (!loginChecker.IsLogin)
        {
            registerUI.SetActive(true);
            startUI.SetActive(false);
        }
        else
        {
            registerUI.SetActive(false);
            startUI.SetActive(true);
        }
    }

    void GetUserID()
    {
        if (usersModel.user_id == null) { return; }
        if (!string.IsNullOrEmpty(usersModel.user_id))
        {
            uuid = string.Format("UUID:{0}", usersModel.user_id);
            uuidTex.text = uuid;
        }
        else
        {
            Debug.Log("未登録");
        }
    }

    public void TestResist()
    {
        name = input.text;
        if (name.Length < 16)
        {
            List<IMultipartFormSection> registForm = new(); // WWWFormの新しいやり方
            registForm.Add(new MultipartFormDataSection("un", name));
            StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.REGISTRATION_URL, registForm, null));
            if (Users.Get().user_id != null)
            {
                loginChecker.OnLoginFlag(); // 登録完了
            }
        }
        else
        {
            Debug.Log("名前が長すぎる");
        }
    }

    void CreateSQLiteFile()
    {
        // SQLiteのDBファイル作成
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }
    }

    void CheckCreateTables()
    {
        // ユーザーテーブル
        if (usersModel == null)
        {
            Users.CreateTable();
            usersModel = Users.Get();
        }
        // ウォレットテーブル
        if (walletsModel == null)
        {
            Wallets.CreateTable();
            walletsModel = Wallets.Get();
        }
        // アイテムテーブル
        if (itemsModel == null)
        {
            Items.CreateTable();
            itemsModel = Items.GetItemDataAll();
        }

        // TODO:現状だと初めてゲームを開いた状態だと、Getの中身がないからエラー出る、それをSetしてから取得する処理に変える
    }
}
