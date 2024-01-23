using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class UserProfileGetManager : UsersBase
{
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] TextMeshProUGUI userRankText;
    [SerializeField] TextMeshProUGUI userRankExpText;
    [SerializeField] TextMeshProUGUI walletText;

    string userName;

    int lastStamina;
    int maxStamina;
    int userRank;
    int userRankExp;

    int freeAmount;
    int paidAmount;
    int maxAmount;

    void Start()
    {
        base.Awake();
        DisplayUI();
    }

    void Update()
    {
        DisplayUI();
    }

    // 名前取得
    void GetUserName()
    {
        if (string.IsNullOrEmpty(usersModel.user_name)) { return; }
        string setUserName = userName != usersModel.user_name ? userName = usersModel.user_name : " "; // ユーザーネームが変わっていたら反映
        userNameText.text = string.Format("UserName:{0}", userName);
    }
    // ランク取得
    void GetUserRank()
    {
        if (usersModel.user_rank == null) { return; }
        int setUserRank = userRank != usersModel.user_rank ? userRank = usersModel.user_rank : 0; // ユーザーランクが変わっていたら反映
        userRankText.text = string.Format("UserRank:{0}", userRank.ToString());
    }
    // ランク経験値取得
    void GetUserRankExp()
    {
        if (usersModel.user_rank_exp == null) { return; }
        int setUserRankExp = userRankExp != usersModel.user_rank_exp ? userRankExp = usersModel.user_rank_exp : 0; // ユーザーランクの経験値が変わっていたら反映
        userRankExpText.text = string.Format("{0}Exp", userRankExp.ToString());
    }
    // スタミナ取得
    void GetStamina()
    {
        if (usersModel.last_stamina == null || usersModel.max_stamina == null) { return; }
        int setLastStamina = lastStamina != usersModel.last_stamina ? lastStamina = usersModel.last_stamina : 0; // 最終スタミナが変わっていたら反映
        int setMaxStamina = maxStamina != usersModel.max_stamina ? maxStamina = usersModel.max_stamina : 0;      // 最大スタミナが変わっていたら反映
        staminaText.text = string.Format("<color=#FF9D00>Stamina:</color>{0}/{1}", lastStamina.ToString(), maxStamina.ToString());
    }

    // ウォレット取得
    void GetWallet()
    {
        if (walletsModel.free_amount == null || walletsModel.paid_amount == null || walletsModel.max_amount == null) { return; }
        int setFreeAmount = freeAmount != walletsModel.free_amount ? freeAmount = walletsModel.free_amount : 0; // 無償通貨が変わっていたら反映
        int setPaidAmount = paidAmount != walletsModel.paid_amount ? paidAmount = walletsModel.paid_amount : 0; // 有償通貨が変わっていたら反映
        int setMaxAmount = maxAmount != walletsModel.max_amount ? maxAmount = walletsModel.max_amount : 0;      // 最大所持通貨が変わっていたら反映
        int totalAmount = freeAmount + paidAmount;
        walletText.text = string.Format("total:{0}(free:{1}/paid:{2})", totalAmount, freeAmount, paidAmount);
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
}
