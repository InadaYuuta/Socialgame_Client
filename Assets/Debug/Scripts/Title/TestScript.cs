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

[Serializable]
public class ResponseObjects
{
    public UsersModel usersModel;
}

public class TestScript : MonoBehaviour
{
    [SerializeField] TMP_InputField input;

    [SerializeField] string name = "ユウタ";

    // DBモデル
    UsersModel usersModel;

    [SerializeField] TextMeshProUGUI uuidTex;
    string uuid;

    private void Awake()
    {
        Debug.Log("ユニークID" + SystemInfo.deviceUniqueIdentifier);
        // SQLiteのDBファイル作成
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }
        // テーブル作成処理
        Users.CreateTable();
    }

    private void Start()
    {
        // Usersの取得
        usersModel = Users.Get();
        if (!string.IsNullOrEmpty(usersModel.user_id))
        {
            string debugStr = string.Format("User:{0}", usersModel.user_name);
            Debug.Log(debugStr);
            uuid = string.Format("UUID:{0}", usersModel.user_id);
            uuidTex.text = uuid;
        }
        else
        {
            Debug.Log("未登録");
        }
    }

    private void Update()
    {
        var a = Keyboard.current;
        var b = a.digit1Key.wasPressedThisFrame;
        if (b)
        {
            // Usersの取得
            usersModel = Users.Get();
            if (!string.IsNullOrEmpty(usersModel.user_id))
            {
                string debugStr = string.Format("User:{0}", usersModel.user_name);
                Debug.Log(debugStr);
            }
            else
            {
                Debug.Log("未登録");
            }
        }
    }

    public void TestResist()
    {
        name = input.text;
        StartCoroutine(Resist());
    }

    // 登録処理
    IEnumerator Resist(Action action = null)
    {
        // var deviceId = SystemInfo.deviceUniqueIdentifier;

        List<IMultipartFormSection> form = new List<IMultipartFormSection>(); // WWWFormの新しいやり方
        form.Add(new MultipartFormDataSection("un", name));
        //  form.Add(new MultipartFormDataSection("did", deviceId));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(GameUtil.Const.RESISTRATION_URL, form))
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
                    if (!string.IsNullOrEmpty(responseObjects.usersModel.user_id))
                        Users.Set(responseObjects.usersModel);
                    // 正常終了アクション実行
                    if (action != null)
                    {
                        action();
                        action = null;
                    }
                }
            }
        }

    }
}
