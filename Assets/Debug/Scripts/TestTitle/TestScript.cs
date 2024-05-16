using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class TestScript : UsersBase
{
    [SerializeField] TMP_InputField input;

    [SerializeField] string name = "tanaka";

    [SerializeField] TextMeshProUGUI uuidTex;
    string uuid;

    LoginChecker loginChecker;

    [SerializeField] GameObject registerUI;
    [SerializeField] GameObject startUI;


    private void Awake()
    {
        base.Awake();
        loginChecker = FindObjectOfType<LoginChecker>();

        string deviceId = string.Format("デバイスのユニークID:{0}", SystemInfo.deviceUniqueIdentifier);
        Debug.Log(deviceId);

        // SQLiteのDBファイル作成
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }

        if (usersModel == null)
        {
            // テーブル作成処理
            Users.CreateTable();
            usersModel = Users.Get();
        }

        if (walletsModel == null)
        {
            Wallets.CreateTable();
            walletsModel = Wallets.Get();
        }

        if (!string.IsNullOrEmpty(usersModel.user_id))
        {
            loginChecker.OnLoginFlag();
        }

        registerUI = GameObject.Find("RegisterUI");
        startUI = GameObject.Find("StartUI");
    }

    private void Start()
    {
        GetUserID();
        DisplayUI();
    }

    private void Update()
    {
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
            StartCoroutine(Resist());
        }
        else
        {
            Debug.Log("名前が長すぎる");
        }
    }

    // 登録処理
    IEnumerator Resist(Action action = null)
    {
        // var deviceId = SystemInfo.deviceUniqueIdentifier;

        List<IMultipartFormSection> form = new List<IMultipartFormSection>(); // WWWFormの新しいやり方
        form.Add(new MultipartFormDataSection("un", name));
        //  form.Add(new MultipartFormDataSection("did", deviceId));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(GameUtil.Const.REGISTRATION_URL, form))
        {
            webRequest.timeout = 10; // 10秒でタイムアウト
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.downloadHandler != null)
                {
                    string text = webRequest.downloadHandler.text;
                    Debug.Log(text);

                    // *** SQLiteへの保存処理 ***
                    ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
                    if (!string.IsNullOrEmpty(responseObjects.user.user_id))
                        Users.Set(responseObjects.user);
                    // 正常終了アクション実行
                    if (action != null)
                    {
                        action();
                        action = null;
                    }
                    loginChecker.OnLoginFlag(); // 登録完了
                }
            }
        }

    }
}
