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

    // �~�b�V���������X�V
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

    // �z�[���̏����܂Ƃ߂čX�V
    public void GetHomeData()
    {
        ResultPanelController.DisplayCommunicationPanel();
        List<IMultipartFormSection> homeForm = new(); // WWWForm�̐V��������
        string user_id = Users.Get().user_id;
        homeForm.Add(new MultipartFormDataSection("uid", user_id));
        Action afterAction = new(() => UpdateMission());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.HOME_URL, homeForm, afterAction));
    }

    // ���O�擾
    void GetUserName()
    {
        if (string.IsNullOrEmpty(usersModel.user_name)) { return; }
        string setUserName = userName != usersModel.user_name ? userName = usersModel.user_name : " "; // ���[�U�[�l�[�����ς���Ă����甽�f
        userNameText.text = string.Format("{0}", userName);
    }
    // �����N�擾
    void GetUserRank()
    {
        if (usersModel.user_rank == null) { return; }
        int setUserRank = userRank != usersModel.user_rank ? userRank = usersModel.user_rank : 0; // ���[�U�[�����N���ς���Ă����甽�f
        userRankText.text = string.Format("���[�U�[�����N:{0}", userRank.ToString());
    }
    // �����N�o���l�擾
    void GetUserRankExp()
    {
        if (usersModel.user_rank_exp == null) { return; }
        int setUserRankExp = userRankExp != usersModel.user_rank_exp ? userRankExp = usersModel.user_rank_exp : 0; // ���[�U�[�����N�̌o���l���ς���Ă����甽�f
        userRankExpText.text = string.Format("���̃��x���܂�:{0}Exp", userRankExp.ToString());
    }
    // �X�^�~�i�擾
    void GetStamina()
    {
        if (usersModel.last_stamina == null || usersModel.max_stamina == null) { return; }
        int setLastStamina = lastStamina != usersModel.last_stamina ? lastStamina = usersModel.last_stamina : 0; // �ŏI�X�^�~�i���ς���Ă����甽�f
        int setMaxStamina = maxStamina != usersModel.max_stamina ? maxStamina = usersModel.max_stamina : 0;      // �ő�X�^�~�i���ς���Ă����甽�f
        staminaText.text = string.Format("<color=#FF9D00>�X�^�~�i:</color>{0}/{1}", lastStamina.ToString(), maxStamina.ToString());
    }

    // �E�H���b�g�擾
    void GetWallet()
    {
        if (walletsModel.free_amount == null || walletsModel.paid_amount == null || walletsModel.max_amount == null) { return; }
        int setFreeAmount = freeAmount != walletsModel.free_amount ? freeAmount = walletsModel.free_amount : freeAmount; // �����ʉ݂��ς���Ă����甽�f
        int setPaidAmount = paidAmount != walletsModel.paid_amount ? paidAmount = walletsModel.paid_amount : paidAmount; // �L���ʉ݂��ς���Ă����甽�f
        int setMaxAmount = maxAmount != walletsModel.max_amount ? maxAmount = walletsModel.max_amount : maxAmount;      // �ő及���ʉ݂��ς���Ă����甽�f
        int totalAmount = freeAmount + paidAmount;
        walletText.text = string.Format("���v�ʉ�{0}��\r\n(������:{1}��/�L����:{2}��)", totalAmount, setFreeAmount, setPaidAmount);
    }

    // �e�X�\��
    void DisplayUI()
    {
        GetUserName();
        GetUserRank();
        GetUserRankExp();
        GetStamina();
        GetWallet();
    }

    // �w�肳�ꂽ���l��������
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

    // �w�肳�ꂽ�����������������z��ɕϊ�
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
        Array.Reverse(result); // �����Ŕz����t���ɂ���
        return result;
    }

    // �n���ꂽ�����̎O���ڂ�,��}�������������Ԃ�
    string PutCommaInThaThirdDigit(int currentRPoint)
    {
        string resultStr = "";

        int[] listNum = ConvertNumToList(currentRPoint); // �����Ő��l�����X�g������

        for (int i = 0; i < listNum.Length; i++)
        {
            if (listNum.Length > 3 && i == 1) { resultStr += ","; }
            resultStr += listNum[i].ToString();
        }
        return resultStr;
    }

    // �����|�C���g��,��t�����`�Ŏ擾
    public string GetCurrentRPointStr()
    {
        int currntRPoint = Users.Get().has_reinforce_point;
        return PutCommaInThaThirdDigit(currntRPoint);
    }
}