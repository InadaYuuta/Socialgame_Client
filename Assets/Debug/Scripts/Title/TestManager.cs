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

    /** DBモデル*/
    private UsersModel usersModel;

    private void Awake()
    {
        // SQLiteのDBファイル作成
        string DBPath = Application.persistentDataPath + "/" + GameUtil.Const.SQLITE_FILE_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }
        // テーブル作成処理
        Users.CreateTable();
    }

    private void Start()
    {
        StartCanvas = GameObject.Find(START_CANVAS);
        RegistCanvas = GameObject.Find(REGIST_CANVAS);
        registUserNameText = GameObject.Find(REGIST_USER_NAME_TEXT).GetComponent<InputField>();
        startUserNameText = GameObject.Find(START_USER_NAME_TEXT).GetComponent<Text>();
        registMsgText = GameObject.Find(REGIST_MSG_TEXT).GetComponent<Text>();

        // UserProfileの取得
        usersModel = Users.Get();
        if (!string.IsNullOrEmpty(usersModel.user_id))
        {
            // ユーザー登録済:StartCanvas表示
            StartCanvas.SetActive(true);
            RegistCanvas.SetActive(false);
            startUserNameText.text = "User:" + usersModel.user_name;
        }
        else
        {
            // ユーザー未登録:RegistCanvas表示
            StartCanvas.SetActive(false);
            RegistCanvas.SetActive(true);
        }
    }

    // ---------------ボタン押下時処理-------------------
    public void PushRegistButton()
    {
        if (string.IsNullOrEmpty(registUserNameText.text))
        {
            // ユーザー名未登録の場合
            registMsgText.text = "入力して下さい";
        }
        else if (registUserNameText.text.Length > 5)
        {
            // ユーザー名が５文字以上の場合
            registMsgText.text = "5文字以内で入力して下さい";
        }
        else
        {
            // ユーザー登録処理
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
