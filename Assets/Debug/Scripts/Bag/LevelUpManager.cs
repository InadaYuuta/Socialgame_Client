using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelUpManager : WeaponBase
{
    [SerializeField] GameObject reinforcePanel;
    [SerializeField] TextMeshProUGUI changeLevelText, consumptionReinforcePointText;
    [SerializeField] TextMeshProUGUI haveReinforcePointText;
    [SerializeField] Image LevelUpButton;

    string beforeLevelStr = "Lv";
    string afterLevelStr = "Lv";
    string arrowStr = " -> ";
    string necessaryPointStr = "�K�v P ";
    string slashStr = " / ";
    int currentLevel, afterLevel, consumptionRPoint, currentRPoint;
    string currentRPointStr;
    int levelUpWeaponId;

    bool isPush = false; // �{�^���������邩

    ChoiceWeaponDataManager weaponData;
    UserProfileGetManager userProfile;
    ChangeImageColor changeImageColor;

    enum UnPushReason
    {
        NONE = 0, // ������    
        SHORTAGE, // �������s��
        MAX       // ����ɒB���Ă���
    }
    UnPushReason currentState = UnPushReason.NONE; // �{�^���������Ȃ����R

    void Start()
    {
        if (reinforcePanel != null)
        {
            reinforcePanel.SetActive(false);
        }
        weaponData = FindObjectOfType<ChoiceWeaponDataManager>();
        userProfile = FindObjectOfType<UserProfileGetManager>();
        changeImageColor = FindObjectOfType<ChangeImageColor>();
    }

    private void Update()
    {
        if (!reinforcePanel.activeInHierarchy) { return; }
        // ����⏊�����Ă��鋭���A�C�e�����̃e�L�X�g�X�V
        SetLevelUpWeaponData(levelUpWeaponId);
        // �����|�C���g�̕\��
        currentRPointStr = userProfile.GetCurrentRPointStr();
        haveReinforcePointText.text = currentRPointStr;

        CheckCanLevelUp();
    }

    // ���x���A�b�v��ʂ����镐��̏����擾����
    public void SetLevelUpWeaponParameter(int weapon_id)
    {
        levelUpWeaponId = weapon_id;
        SetLevelUpWeaponData(levelUpWeaponId);
    }

    // �������镐���ID���畐��摜�ƌ��݃��x���A�����ʃA�C�e���Ȃǂ�ݒ肷��
    public void SetLevelUpWeaponData(int weapon_id)
    {
        currentLevel = Weapons.GetWeaponData(weapon_id).level;
        afterLevel = currentLevel >= 50 ? 50 : currentLevel + 1; // ���݂̃��x����50�ȉ��Ȃ炻�̒l�Ɂ{�P�A�T�O�Ȃ�T�O
        consumptionRPoint = WeaponExps.GetWeaponExpData(weapon_id).use_reinforce_point;
        currentRPoint = Users.Get().has_reinforce_point;
        currentRPointStr = userProfile.GetCurrentRPointStr();
        changeLevelText.text = string.Format("{0}{1}{2}{3}{4}", beforeLevelStr, currentLevel, arrowStr, afterLevelStr, afterLevel);
        consumptionReinforcePointText.text = string.Format("{0}{1}{2}{3}", necessaryPointStr, consumptionRPoint, slashStr, currentRPointStr);
    }

    // �����|�C���g������Ă��邩�m�F���ă{�^�����������Ԃ��ǂ������ʂ���
    void CheckCanLevelUp()
    {
        consumptionRPoint = WeaponExps.GetWeaponExpData(levelUpWeaponId).use_reinforce_point;
        currentRPoint = Users.Get().has_reinforce_point;
        currentLevel = Weapons.GetWeaponData(levelUpWeaponId).level;

        ChangeImageColor.ChangeMode changeMode = ChangeImageColor.ChangeMode.UNSELECT;

        if (currentLevel < 50)
        {
            if (currentRPoint >= consumptionRPoint)
            {
                changeMode = ChangeImageColor.ChangeMode.REINFORCE;
                currentState = UnPushReason.NONE;
            }
            else { currentState = UnPushReason.SHORTAGE; } // �����|�C���g������Ȃ�������I���ł��Ȃ�����
        }
        else { currentState = UnPushReason.MAX; } // ���x������Ȃ�I���ł��Ȃ��悤�ɂ���

        changeImageColor.ChangeTargetColor(LevelUpButton, changeMode);
    }

    // ����̃��x���A�b�v���s��
    public void LevelUp()
    {
        // �{�^���������Ȃ����R������΂����\�����ă��^�[��
        switch (currentState)
        {
            case UnPushReason.NONE:
                isPush = true;
                break;
            case UnPushReason.SHORTAGE:
                StartCoroutine(ResultPanelController.DisplayResultPanel("�����|�C���g������܂���"));
                isPush = false;
                break;
            case UnPushReason.MAX:
                StartCoroutine(ResultPanelController.DisplayResultPanel("���x������ł�"));
                isPush = false;
                break;
        }
        if (!isPush) { return; }
        List<IMultipartFormSection> levelUpForm = new();
        levelUpForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        levelUpForm.Add(new MultipartFormDataSection("wid", levelUpWeaponId.ToString()));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.WEAPON_LEVEL_UP_URL, levelUpForm));
    }
}
