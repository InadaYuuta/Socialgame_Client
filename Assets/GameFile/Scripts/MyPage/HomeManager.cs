using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class HomeManager : UsersBase
{
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] TextMeshProUGUI userRankText;
    [SerializeField] TextMeshProUGUI userRankExpText;
    [SerializeField] TextMeshProUGUI walletText;

    string userName;

    int lastStamina = 0;
    int maxStamina = 0;
    int userRank = 0;
    int userRankExp = 0;

    int freeAmount = 0;
    int paidAmount = 0;
    int maxAmount = 0;

    GetMissionData getMission;

    void Awake()
    {
        base.Awake();
        getMission = FindObjectOfType<GetMissionData>();
    }

    void Start() => GetHomeData();

    void Update()
    {
        base.Update();
        DisplayUI();
    }

    // ミッション情報を更新
    void UpdateMission()
    {
        ResultPanelController.HideCommunicationPanel();
        DisplayUI();
        MissionCloneManager[] all = FindObjectsOfType<MissionCloneManager>();
        foreach (MissionCloneManager clone in all)
        {
            clone.UpdateMission();
        }
    }

    // ホームの情報をまとめて更新
    public void GetHomeData()
    {
        ResultPanelController.DisplayCommunicationPanel();
        List<IMultipartFormSection> homeForm = new(); // WWWFormの新しいやり方
        string user_id = Users.Get().user_id;
        homeForm.Add(new MultipartFormDataSection("uid", user_id));
        Action afterAction = new(() => UpdateMission());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.HOME_URL, homeForm, afterAction));
    }

    // 名前取得
    void GetUserName()
    {
        if (string.IsNullOrEmpty(usersModel.user_name)) { return; }
        string setUserName = userName != usersModel.user_name ? userName = usersModel.user_name : " "; // ユーザーネームが変わっていたら反映
        userNameText.text = string.Format("{0}", userName);
    }
    // ランク取得
    void GetUserRank()
    {
        if (usersModel.user_rank == null) { return; }
        int setUserRank = userRank != usersModel.user_rank ? userRank = usersModel.user_rank : 0; // ユーザーランクが変わっていたら反映
        userRankText.text = string.Format("ユーザーランク:{0}", userRank.ToString());
    }
    // ランク経験値取得
    void GetUserRankExp()
    {
        if (usersModel.user_rank_exp == null) { return; }
        int setUserRankExp = userRankExp != usersModel.user_rank_exp ? userRankExp = usersModel.user_rank_exp : 0; // ユーザーランクの経験値が変わっていたら反映
        userRankExpText.text = string.Format("次のレベルまで:{0}Exp", userRankExp.ToString());
    }
    // スタミナ取得
    void GetStamina()
    {
        if (usersModel.last_stamina == null || usersModel.max_stamina == null) { return; }
        int setLastStamina = lastStamina != usersModel.last_stamina ? lastStamina = usersModel.last_stamina : 0; // 最終スタミナが変わっていたら反映
        int setMaxStamina = maxStamina != usersModel.max_stamina ? maxStamina = usersModel.max_stamina : 0;      // 最大スタミナが変わっていたら反映
        staminaText.text = string.Format("<color=#FF9D00>スタミナ:</color>{0}/{1}", lastStamina.ToString(), maxStamina.ToString());
    }

    // ウォレット取得
    void GetWallet()
    {
        if (walletsModel.free_amount == null || walletsModel.paid_amount == null || walletsModel.max_amount == null) { return; }
        int setFreeAmount = freeAmount != walletsModel.free_amount ? freeAmount = walletsModel.free_amount : freeAmount; // 無償通貨が変わっていたら反映
        int setPaidAmount = paidAmount != walletsModel.paid_amount ? paidAmount = walletsModel.paid_amount : paidAmount; // 有償通貨が変わっていたら反映
        int setMaxAmount = maxAmount != walletsModel.max_amount ? maxAmount = walletsModel.max_amount : maxAmount;      // 最大所持通貨が変わっていたら反映
        int totalAmount = freeAmount + paidAmount;
        walletText.text = string.Format("合計通貨{0}個\r\n(無償分:{1}個/有償分:{2}個)", totalAmount, setFreeAmount, setPaidAmount);
    }

    // 各々表示
    void DisplayUI()
    {
        GetUserName();
        GetUserRank();
        GetUserRankExp();
        GetStamina();
        GetWallet();
    }

    // 指定された数値が何桁か
    int CheckDigitNum(int currentRPoint)
    {
        int count = 1;
        while (currentRPoint / 10 > 0)
        {
            currentRPoint /= 10;
            count++;
        }
        return count;
    }

    // 指定された数字を桁分けした配列に変換
    int[] ConvertNumToList(int target)
    {
        int digit = CheckDigitNum(target);
        int[] result = new int[digit];

        for (int i = 0; i < digit; i++)
        {
            int overNum = target % 10;
            if (overNum != 0) { result[i] = overNum; }

            target /= 10;
        }
        Array.Reverse(result); // ここで配列を逆順にする
        return result;
    }

    // 渡された数字の三桁目に,を挿入した文字列を返す
    string PutCommaInThaThirdDigit(int currentRPoint)
    {
        string resultStr = "";

        int[] listNum = ConvertNumToList(currentRPoint); // ここで数値をリスト化する

        for (int i = 0; i < listNum.Length; i++)
        {
            if (listNum.Length > 3 && i == 1) { resultStr += ","; }
            resultStr += listNum[i].ToString();
        }
        return resultStr;
    }

    // 強化ポイントを,を付けた形で取得
    public string GetCurrentRPointStr()
    {
        int currntRPoint = Users.Get().has_reinforce_point;
        return PutCommaInThaThirdDigit(currntRPoint);
    }
}
