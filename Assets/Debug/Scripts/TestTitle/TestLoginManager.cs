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
using Unity.VisualScripting;

public class TestLoginManager : UsersBase
{
    [SerializeField] string userId;


    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        userId = usersModel.user_id;
    }

    public void TestLogin()
    {
        StartCoroutine(Login());
    }

    // �o�^����
    IEnumerator Login(Action action = null)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>(); // WWWForm�̐V��������
        form.Add(new MultipartFormDataSection("uid", userId));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(GameUtil.Const.LOGIN_URL, form))
        {
            webRequest.timeout = 10; // 10�b�Ń^�C���A�E�g
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

                    //// *** SQLite�ւ̕ۑ����� ***
                    //ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
                    //if (!string.IsNullOrEmpty(responseObjects.usersModel.user_id))
                    //    Users.Set(responseObjects.usersModel);
                    // ����I���A�N�V�������s
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
