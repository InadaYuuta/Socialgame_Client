using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RegistrationManager : UsersBase
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

        string deviceId = string.Format("�f�o�C�X�̃��j�[�NID:{0}", SystemInfo.deviceUniqueIdentifier);
        Debug.Log(deviceId);

        // SQLite��DB�t�@�C���쐬
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }

        if (usersModel == null)
        {
            // �e�[�u���쐬����
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


    // ���O�C�����Ă���΃X�^�[�g�p��UI�����Ă��Ȃ���Γo�^�p��UI��\��
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
            Debug.Log("���o�^");
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
            Debug.Log("���O����������");
        }
    }

    // �o�^����
    IEnumerator Resist(Action action = null)
    {
        // var deviceId = SystemInfo.deviceUniqueIdentifier;

        List<IMultipartFormSection> form = new List<IMultipartFormSection>(); // WWWForm�̐V��������
        form.Add(new MultipartFormDataSection("un", name));
        //  form.Add(new MultipartFormDataSection("did", deviceId));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(GameUtil.Const.REGISTRATION_URL, form))
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

                    // *** SQLite�ւ̕ۑ����� ***
                    ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
                    if (!string.IsNullOrEmpty(responseObjects.users.user_id))
                        Users.Set(responseObjects.users);
                    // ����I���A�N�V�������s
                    if (action != null)
                    {
                        action();
                        action = null;
                    }
                    loginChecker.OnLoginFlag(); // �o�^����
                }
            }
        }

    }
}
