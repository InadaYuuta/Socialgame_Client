using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RegistrationController : UsersBase
{
    [SerializeField] TMP_InputField input;

    [SerializeField] string name = "tanaka";

    [SerializeField] TextMeshProUGUI uuidTex;
    string uuid;

    LoginChecker loginChecker;

    [SerializeField] GameObject registerUI;
    [SerializeField] GameObject startUI;

    ItemsModel[] itemsModel;

    private void Awake()
    {
        base.Awake();
        loginChecker = FindObjectOfType<LoginChecker>();

        string deviceId = string.Format("�f�o�C�X�̃��j�[�NID:{0}", SystemInfo.deviceUniqueIdentifier);
        Debug.Log(deviceId);

        //CreateSQLiteFile();
        //CheckCreateTables();

        if (usersModel != null && !string.IsNullOrEmpty(usersModel.user_id))
        {
            loginChecker.OnLoginFlag();
        }

        registerUI = GameObject.Find("RegisterUI");
        startUI = GameObject.Find("StartUI");

        if (Items.GetItemDataAll() != null && Items.GetItemData(10001).item_id != null)
        {
            itemsModel = Items.GetItemDataAll();
        }
    }

    private void Start()
    {
        if (Users.Get() == null) { return; }
        GetUserID();
        DisplayUI();
    }

    private void Update()
    {
        if (Users.Get() == null) { return; }
        if (usersModel != null && !string.IsNullOrEmpty(usersModel.user_id))
        {
            loginChecker.OnLoginFlag();
        }
        base.Update();
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
            List<IMultipartFormSection> registForm = new(); // WWWForm�̐V��������
            registForm.Add(new MultipartFormDataSection("un", name));
            StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.REGISTRATION_URL, registForm, null));
            if (Users.Get().user_id != null)
            {
                loginChecker.OnLoginFlag(); // �o�^����
            }
        }
        else
        {
            Debug.Log("���O����������");
        }
    }

    void CreateSQLiteFile()
    {
        // SQLite��DB�t�@�C���쐬
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }
    }

    void CheckCreateTables()
    {
        // ���[�U�[�e�[�u��
        if (usersModel == null)
        {
            Users.CreateTable();
            usersModel = Users.Get();
        }
        // �E�H���b�g�e�[�u��
        if (walletsModel == null)
        {
            Wallets.CreateTable();
            walletsModel = Wallets.Get();
        }
        // �A�C�e���e�[�u��
        if (itemsModel == null)
        {
            Items.CreateTable();
            itemsModel = Items.GetItemDataAll();
        }

        // TODO:���󂾂Ə��߂ăQ�[�����J������Ԃ��ƁAGet�̒��g���Ȃ�����G���[�o��A�����Set���Ă���擾���鏈���ɕς���
    }
}
