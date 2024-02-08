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
        List<IMultipartFormSection> loginForm = new(); // WWWForm�̐V��������
        loginForm.Add(new MultipartFormDataSection("uid", userId));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.LOGIN_URL, loginForm, null));
        FadeManager.Instance.LoadScene("MyPageScene"); // �}�C�y�[�W�ɔ��
    }

    // ���O�C������
    //IEnumerator Login(Action action = null)
    //{
    //    List<IMultipartFormSection> loginForm = new(); // WWWForm�̐V��������
    //    loginForm.Add(new MultipartFormDataSection("uid", userId));
    //    using (UnityWebRequest webRequest = UnityWebRequest.Post(GameUtil.Const.LOGIN_URL, loginForm))
    //    {
    //        webRequest.timeout = 10; // 10�b�Ń^�C���A�E�g
    //        yield return webRequest.SendWebRequest();
    //        if (webRequest.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(webRequest.error);
    //        }
    //        else
    //        {
    //            if (webRequest.downloadHandler != null)
    //            {
    //                FadeManager.Instance.LoadScene("MyPageScene"); // �}�C�y�[�W�ɔ��
    //                // ����I���A�N�V�������s
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
