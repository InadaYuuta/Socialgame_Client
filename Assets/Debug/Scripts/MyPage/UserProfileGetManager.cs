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

    string userName;

    int lastStamina;
    int maxStamina;
    int userRank;
    int userRankExp;


    void Start()
    {
        base.Awake();
        DisplayUI();
    }

    void Update()
    {
        DisplayUI();
    }

    // ���O�擾
    void SetUserName()
    {
        if (string.IsNullOrEmpty(usersModel.user_name)) { return; }
        string setUserName = userName != usersModel.user_name ? userName = usersModel.user_name : " "; // ���[�U�[�l�[�����ς���Ă����甽�f
        userNameText.text = string.Format("UserName:{0}", userName);
    }
    // �����N�擾
    void SetUserRank()
    {
        if (usersModel.user_rank == null) { return; }
        int setUserRank = userRank != usersModel.user_rank ? userRank = usersModel.user_rank : 0; // ���[�U�[�����N���ς���Ă����甽�f
        userRankText.text = string.Format("UserRank:{0}", userRank.ToString());
    }
    // �����N�o���l�擾
    void SetUserRankExp()
    {
        if (usersModel.user_rank_exp == null) { return; }
        int setUserRankExp = userRankExp != usersModel.user_rank_exp ? userRankExp = usersModel.user_rank_exp : 0; // ���[�U�[�����N�̌o���l���ς���Ă����甽�f
        userRankExpText.text = string.Format("{0}Exp", userRankExp.ToString());
    }
    // �X�^�~�i�擾
    void SetStamina()
    {
        if (usersModel.last_stamina == null || usersModel.max_stamina == null) { return; }
        int setLastStamina = lastStamina != usersModel.last_stamina ? lastStamina = usersModel.last_stamina : 0; // �ŏI�X�^�~�i���ς���Ă����甽�f
        int setMaxStamina = maxStamina != usersModel.max_stamina ? maxStamina = usersModel.max_stamina : 0;      // �ő�X�^�~�i���ς���Ă����甽�f
        staminaText.text = string.Format("<color=#FF9D00>Stamina:</color>{0}/{1}", lastStamina.ToString(), maxStamina.ToString());
    }

    // �e�X�\��
    void DisplayUI()
    {
        SetUserName();
        SetUserRank();
        SetUserRankExp();
        SetStamina();
    }
}
