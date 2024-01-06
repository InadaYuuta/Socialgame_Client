using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    private const string REGIST_USER_NAME_TEXT = "RegistUserNameText";
    private const string START_USER_NAME_TEXT = "StartUserNameText";
    private const string REGIST_MSG_TEXT = "RegistMsg";
    private const string START_CANVAS = "StartCanvas";
    private const string REGIST_CANVAS = "RegistCanvas";
    [SerializeField] private InputField registUserNameText;
    [SerializeField] private Text startUserNameText;
    [SerializeField] private Text registMsgText;
    [SerializeField] private GameObject StartCanvas;
    [SerializeField] private GameObject RegistCanvas;

    /** DB���f��*/
    private UsersModel usersModel;

    private void Awake()
    {
        // SQLite��DB�t�@�C���쐬
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }
        // �e�[�u���쐬����
        Users.CreateTable();
    }

    private void Start()
    {
        StartCanvas = GameObject.Find(START_CANVAS);
        RegistCanvas = GameObject.Find(REGIST_CANVAS);
        registUserNameText = GameObject.Find(REGIST_USER_NAME_TEXT).GetComponent<InputField>();
        startUserNameText = GameObject.Find(START_USER_NAME_TEXT).GetComponent<Text>();
        registMsgText = GameObject.Find(REGIST_MSG_TEXT).GetComponent<Text>();

        // UserProfile�̎擾
        usersModel = Users.Get();
        if (!string.IsNullOrEmpty(usersModel.user_id))
        {
            // ���[�U�[�o�^��:StartCanvas�\��
            StartCanvas.SetActive(true);
            RegistCanvas.SetActive(false);
            startUserNameText.text = "User:" + usersModel.user_name;
        }
        else
        {
            // ���[�U�[���o�^:RegistCanvas�\��
            StartCanvas.SetActive(false);
            RegistCanvas.SetActive(true);
        }
    }

    // ---------------�{�^������������-------------------
    public void PushRegistButton()
    {
        if (string.IsNullOrEmpty(registUserNameText.text))
        {
            // ���[�U�[�����o�^�̏ꍇ
            registMsgText.text = "���͂��ĉ�����";
        }
        else if (registUserNameText.text.Length > 5)
        {
            // ���[�U�[�����T�����ȏ�̏ꍇ
            registMsgText.text = "5�����ȓ��œ��͂��ĉ�����";
        }
        else
        {
            // ���[�U�[�o�^����
            Action action = () =>
            {
                StartCanvas.SetActive(true);
                RegistCanvas.SetActive(false);
                startUserNameText.text = "User:" + registUserNameText.text;
            };
            StartCoroutine(CommunicationManager.ConnectServer("registration", "?user_name=" + registUserNameText.text, action));
        }
    }
}
