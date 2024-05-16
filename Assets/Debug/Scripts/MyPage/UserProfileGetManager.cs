using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UserProfileGetManager : UsersBase
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

    void Start()
    {
        base.Awake();
        List<IMultipartFormSection> homeForm = new(); // WWWForm�̐V��������
        string user_id = Users.Get().user_id;
        homeForm.Add(new MultipartFormDataSection("uid", user_id));
        Debug.Log("Homecontroller");
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.HOME_URL, homeForm, null));
        DisplayUI();
    }

    void Update()
    {
        base.Update();
        DisplayUI();
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
}
