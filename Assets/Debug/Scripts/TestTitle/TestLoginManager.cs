using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestLoginManager : UsersBase
{
    [SerializeField] string userId;

    void Awake() => base.Awake();

    void Start() => userId = usersModel.user_id;

    public void TestLogin()
    {
        List<IMultipartFormSection> loginForm = new(); // WWWFormの新しいやり方
        loginForm.Add(new MultipartFormDataSection("uid", userId));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.LOGIN_URL, loginForm, null));
        FadeManager.Instance.LoadScene("MyPageScene"); // マイページに飛ぶ
    }

    // ログイン処理
    //IEnumerator Login(Action action = null)
    //{
    //    List<IMultipartFormSection> loginForm = new(); // WWWFormの新しいやり方
    //    loginForm.Add(new MultipartFormDataSection("uid", userId));
    //    using (UnityWebRequest webRequest = UnityWebRequest.Post(GameUtil.Const.LOGIN_URL, loginForm))
    //    {
    //        webRequest.timeout = 10; // 10秒でタイムアウト
    //        yield return webRequest.SendWebRequest();
    //        if (webRequest.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(webRequest.error);
    //        }
    //        else
    //        {
    //            if (webRequest.downloadHandler != null)
    //            {
    //                FadeManager.Instance.LoadScene("MyPageScene"); // マイページに飛ぶ
    //                // 正常終了アクション実行
    //                if (action != null)
    //                {
    //                    action();
    //                    action = null;
    //                }
    //            }
    //        }
    //    }

    //}
}
