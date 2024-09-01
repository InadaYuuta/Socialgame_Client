using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class EvolutionManager : WeaponBase
{
    [SerializeField] GameObject EvolutionPanel;
    [SerializeField] TextMeshProUGUI necessaryLevelText, consumptionRPointText, weaponDataEvolutionText, haveReinforcePointText;
    [SerializeField] Image EvolutionButton;

    string convexStr = "��";
    string levelStr = "Lv";
    string necessaryRPointStr = "�K�vP";
    string slashStr = " / ";
    int currentLimitBreak, currentLevel, maxLevel = 50, consumptionRPoint, currentRPoint;
    [SerializeField] int EvolutionWeaponId;

    bool isPush = false; // �{�^���������邩
    enum UnPushReason
    {
        NONE = 0, // ������    
        SHORTAGE, // �������s��
        LEVELNOTENOUGH,      // ����ɒB���Ă���
        EVOLUTED, // �i���ς�
    }
    UnPushReason currentState = UnPushReason.NONE; // �{�^���������Ȃ����R

    BagSortManager bagSortManager;
    ChoiceWeaponDataManager choiceWeaponDataManager;
    ChangeImageColor changeImageColor;

    void Start()
    {
        if (EvolutionPanel != null)
        {
            EvolutionPanel.SetActive(false);
        }
        bagSortManager = FindObjectOfType<BagSortManager>();
        choiceWeaponDataManager = FindObjectOfType<ChoiceWeaponDataManager>();
        changeImageColor = FindObjectOfType<ChangeImageColor>();
    }

    private void Update()
    {
        if (!EvolutionPanel.activeInHierarchy) { return; }
        if (EvolutionWeaponId == 0) { return; }
        // ����⏊�����Ă��鋭���A�C�e�����̃e�L�X�g�X�V
        SetEvolutionWeaponData();
        CheckCanEvolution();
        haveReinforcePointText.text = currentRPoint.ToString();
    }

    // �ʂ����镐��̏����擾����
    public void SetEvolutionWeaponParameter(int weapon_id)
    {
        EvolutionWeaponId = weapon_id;
        SetEvolutionWeaponData();
    }

    // ���E�˔j���镐���ID���畐��摜�ƌ��݂̌��E�˔j�A�����ʃA�C�e���Ȃǂ�ݒ肷��
    public void SetEvolutionWeaponData()
    {
        currentLimitBreak = Weapons.GetWeaponData(EvolutionWeaponId).limit_break;
        currentLevel = Weapons.GetWeaponData(EvolutionWeaponId).level;
        consumptionRPoint = GetTheReinforcePointsNeededForEvolution(EvolutionWeaponId);
        currentRPoint = Users.Get().has_reinforce_point;

        // �e�e�L�X�g�̒��g����������
        necessaryLevelText.text = string.Format("{0} {1} {2} {3}", levelStr, currentLevel, slashStr, maxLevel);
        consumptionRPointText.text = string.Format("{0}{1}{2}{3}", necessaryRPointStr, consumptionRPoint, slashStr, currentRPoint);
        weaponDataEvolutionText.text = string.Format("{0}{1}", convexStr, currentLimitBreak);
    }

    // �����|�C���g������Ă��邩�m�F���ă{�^�����������Ԃ��ǂ������ʂ���
    void CheckCanEvolution()
    {
        currentLevel = Weapons.GetWeaponData(EvolutionWeaponId).level;
        consumptionRPoint = GetTheReinforcePointsNeededForEvolution(EvolutionWeaponId);
        currentRPoint = Users.Get().has_reinforce_point;
        currentLimitBreak = Weapons.GetWeaponData(EvolutionWeaponId).limit_break;

        ChangeImageColor.ChangeMode changeMode = ChangeImageColor.ChangeMode.UNSELECT;

        int evolut_weapon_id = WeaponMaster.GetWeaponMasterData(EvolutionWeaponId).evolution_weapon_id; // �i�����ID�擾

        int evoluted = Weapons.GetWeaponData(evolut_weapon_id).weapon_id; // ��x�ł����̕����i�����Ă�����i���s��
        int evolution = Weapons.GetWeaponData(EvolutionWeaponId).evolution;
        if (evoluted != 0 || evolution > 0)
        {
            currentState = UnPushReason.EVOLUTED;
        }
        else if (currentLevel < maxLevel)
        {
            currentState = UnPushReason.LEVELNOTENOUGH;
        }
        else if (currentRPoint < consumptionRPoint)
        {
            currentState = UnPushReason.SHORTAGE;
        }
        else
        {
            changeMode = ChangeImageColor.ChangeMode.EVOLUTION;
            currentState = UnPushReason.NONE;
        }
        changeImageColor.ChangeTargetColor(EvolutionButton, changeMode);
    }

    // �i��������������Ă�
    void SuccessEvolution()
    {
        var evolutionWeapon = WeaponMaster.GetWeaponMasterData(EvolutionWeaponId).evolution_weapon_id;

        if (Weapons.GetWeaponData(EvolutionWeaponId) != null) { Weapons.DeleteWeapon(EvolutionWeaponId); } // �i���O�̕���̍폜
        choiceWeaponDataManager.SetDetailData(evolutionWeapon);
        bagSortManager.UpdateBag();
        ResultPanelController.HideCommunicationPanel();
        StartCoroutine(ResultPanelController.DisplayResultPanel("�i������"));
    }

    // ����̐i�����s��
    public void Evolution()
    {
        isPush = false;
        // �{�^���������Ȃ����R������΂����\�����ă��^�[��
        switch (currentState)
        {
            case UnPushReason.NONE:
                isPush = true;
                break;
            case UnPushReason.SHORTAGE:
                StartCoroutine(ResultPanelController.DisplayResultPanel("�����|�C���g������܂���"));
                break;
            case UnPushReason.LEVELNOTENOUGH:
                StartCoroutine(ResultPanelController.DisplayResultPanel("���x��������܂���"));
                break;
            case UnPushReason.EVOLUTED:
                StartCoroutine(ResultPanelController.DisplayResultPanel("�i���ς݂ł�"));
                break;
        }
        if (!isPush) { return; }
        ResultPanelController.DisplayCommunicationPanel();
        List<IMultipartFormSection> evolutionForm = new();
        evolutionForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        evolutionForm.Add(new MultipartFormDataSection("wid", EvolutionWeaponId.ToString()));
        Action afterAction = new(() => SuccessEvolution());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.WEAPON_EVOLUTION_URL, evolutionForm, afterAction));
    }
}
