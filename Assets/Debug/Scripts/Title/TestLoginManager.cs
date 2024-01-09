using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestLoginManager : MonoBehaviour
{
    [SerializeField] string loginId = "01HKH8946BC1QDQPQ25ZEJRN22";

    public void TestLogin()
    {
        StartCoroutine(Login());
    }

    // ログイン処理
    IEnumerator Login()
    {
        List<IMultipartFormSection> loginForm = new List<IMultipartFormSection>();
        loginForm.Add(new MultipartFormDataSection("lid", loginId));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(GameUtil.Const.LOGIN_URL,loginForm))
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
                }
            }
        }
    }
}
