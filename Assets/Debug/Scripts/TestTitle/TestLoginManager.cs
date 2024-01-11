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

public class TestLoginManager : MonoBehaviour
{
    [SerializeField] string userId;

    UsersModel usersModel;

    private void Awake()
    {
        usersModel = Users.Get();
    }

    private void Start()
    {
        userId = usersModel.user_id;
    }

    public void TestLogin()
    {
        StartCoroutine(Login());
    }

    // 登録処理
    IEnumerator Login(Action action = null)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>(); // WWWFormの新しいやり方
        form.Add(new MultipartFormDataSection("uid", userId));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(GameUtil.Const.LOGIN_URL, form))
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

                    //// *** SQLiteへの保存処理 ***
                    //ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
                    //if (!string.IsNullOrEmpty(responseObjects.usersModel.user_id))
                    //    Users.Set(responseObjects.usersModel);
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
