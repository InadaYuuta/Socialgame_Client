using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TestLoginManager : UsersBase
{
    [SerializeField] string userId;

    void Awake() => base.Awake();

    void Start() => userId = usersModel.user_id;

    public void TestLogin() => StartCoroutine(Login());

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
                    FadeManager.Instance.LoadScene("MyPageScene"); // �}�C�y�[�W�ɔ��
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
