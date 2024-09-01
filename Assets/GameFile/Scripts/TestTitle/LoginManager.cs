using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoginManager : UsersBase
{
    [SerializeField] string userId;
    bool isFinish = false;

    void Awake() => base.Awake();

    void Start()
    {
        ResultPanelController.HideCommunicationPanel();
    }

    void Update()
    {
        base.Update();
        if (usersModel != null)
        {
            userId = usersModel.user_id;
        }
    }

    void SuccessLogin()
    {
        ResultPanelController.HideCommunicationPanel();
        isFinish = true;
        FadeManager.Instance.LoadScene("MyPageScene"); // �}�C�y�[�W�ɔ��
    }

    public void Login()
    {
        if (!isFinish)
        {
            ResultPanelController.DisplayCommunicationPanel();
            List<IMultipartFormSection> loginForm = new(); // WWWForm�̐V��������
            loginForm.Add(new MultipartFormDataSection("uid", userId));
            Action afterAction = new(() => SuccessLogin());
            StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.LOGIN_URL, loginForm, afterAction));
        }
    }
}
