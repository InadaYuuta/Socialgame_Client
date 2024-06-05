using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LevelUpManager : WeaponBase
{
    [SerializeField] GameObject reinforcePanel;
    [SerializeField] TextMeshProUGUI changeLevelText, consumptionReinforcePointText;
    [SerializeField] TextMeshProUGUI haveReinforcePointText;

    string beforeLevelStr = "Lv";
    string afterLevelStr = "Lv";
    string arrowStr = " -> ";
    string necessaryPointStr = "�K�v P ";
    string slashStr = " / ";
    int currentLevel, afterLevel, consumptionRPoint, currentRPoint;
    string currentRPointStr;
    int levelUpWeaponId;

    ChoiceWeaponDataManager weaponData;
    UserProfileGetManager userProfile;

    void Start()
    {
        if (reinforcePanel != null)
        {
            reinforcePanel.SetActive(false);
        }
        weaponData = FindObjectOfType<ChoiceWeaponDataManager>();
        userProfile = FindObjectOfType<UserProfileGetManager>();
    }

    private void Update()
    {
        if (!reinforcePanel.activeInHierarchy) { return; }
        // ����⏊�����Ă��鋭���A�C�e�����̃e�L�X�g�X�V
        SetLevelUpWeaponData(levelUpWeaponId);
        // �����|�C���g�̕\��
        currentRPointStr = userProfile.GetCurrentRPointStr();
        haveReinforcePointText.text = currentRPointStr;
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
        afterLevel = currentLevel + 1;
        consumptionRPoint = WeaponExps.GetWeaponExpData(weapon_id).use_reinforce_point;
        currentRPoint = Users.Get().has_reinforce_point;
        currentRPointStr = userProfile.GetCurrentRPointStr();
        changeLevelText.text = string.Format("{0}{1}{2}{3}{4}", beforeLevelStr, currentLevel, arrowStr, afterLevelStr, afterLevel);
        consumptionReinforcePointText.text = string.Format("{0}{1}{2}{3}", necessaryPointStr, consumptionRPoint, slashStr, currentRPointStr);
    }

    // TODO : ����������
    public void LevelUp()
    {
        List<IMultipartFormSection> levelUpForm = new();
        levelUpForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        levelUpForm.Add(new MultipartFormDataSection("wid", levelUpWeaponId.ToString()));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.WEAPON_LEVEL_UP_URL, levelUpForm));
    }

    // �p�l���\����\�� -----
    public void OnClickOpenPanelButton()
    {
        if (reinforcePanel == null) { return; }
        reinforcePanel.SetActive(true);
        SetLevelUpWeaponParameter(weaponData.WeaponId);
    }

    public void OnClickClosePanelButton()
    {
        if (reinforcePanel == null) { return; }
        reinforcePanel.SetActive(false);
    }
}
