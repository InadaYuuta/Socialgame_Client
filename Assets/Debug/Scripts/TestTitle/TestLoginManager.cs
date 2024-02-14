using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestLoginManager : UsersBase
{
    [SerializeField] string userId;
    bool isFinish = false;

    void Awake() => base.Awake();

    void Start() => userId = usersModel.user_id;

    public void TestLogin()
    {
        if (!isFinish)
        {
            List<IMultipartFormSection> loginForm = new(); // WWWFormの新しいやり方
            loginForm.Add(new MultipartFormDataSection("uid", userId));
            StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.LOGIN_URL, loginForm, null));
            isFinish = true;
            FadeManager.Instance.LoadScene("MyPageScene"); // マイページに飛ぶ
        }
    }
}
