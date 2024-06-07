using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LimitBreakManager : MonoBehaviour
{
    [SerializeField] GameObject reinforcePanel;
    [SerializeField] TextMeshProUGUI changeLimitBreaklText, consumptionConvexItemText, weaponDataLimitBreakText;
    [SerializeField] Image LimitBreakButton;

    string ConvexStr = "��";
    string arrowStr = " -> ";
    string necessaryImteStr = "�K�v";
    string spaceStr = "   ";
    string slashStr = " / ";
    int currentLimitBreak, afterLimitBreak, consumptionItem, currentItem;
    int limitBreakpWeaponId;

    bool isPush = false; // �{�^���������邩
    enum UnPushReason
    {
        NONE = 0, // ������    
        SHORTAGE, // �������s��
        MAX       // ����ɒB���Ă���
    }
    UnPushReason currentState = UnPushReason.NONE; // �{�^���������Ȃ����R

    ChoiceWeaponDataManager weaponData;
    ChangeImageColor changeImageColor;

    void Start()
    {
        if (reinforcePanel != null)
        {
            reinforcePanel.SetActive(false);
        }
        weaponData = FindObjectOfType<ChoiceWeaponDataManager>();
        changeImageColor = FindObjectOfType<ChangeImageColor>();
    }

    private void Update()
    {
        if (!reinforcePanel.activeInHierarchy) { return; }
        if (limitBreakpWeaponId == 0) { return; }
        // ����⏊�����Ă��鋭���A�C�e�����̃e�L�X�g�X�V
        SetLimitBreakUpWeaponData();
        CheckCanLimitBreak();
    }

    // �ʂ����镐��̏����擾����
    public void SetLimitBreakWeaponParameter(int weapon_id)
    {
        limitBreakpWeaponId = weapon_id;
        SetLimitBreakUpWeaponData();
    }

    // ���E�˔j���镐���ID���畐��摜�ƌ��݂̌��E�˔j�A�����ʃA�C�e���Ȃǂ�ݒ肷��
    public void SetLimitBreakUpWeaponData()
    {
        currentLimitBreak = Weapons.GetWeaponData(limitBreakpWeaponId).limit_break;
        if (currentLimitBreak < 5) { afterLimitBreak = currentLimitBreak + 1; }
        else { afterLimitBreak = 5; }
        consumptionItem = 1; // TODO: ����ʂɕK�v�ȃA�C�e�����������瑝�₷

        ItemsModel test = Items.GetWeaponItemData(limitBreakpWeaponId);

        currentItem = Items.GetWeaponItemData(limitBreakpWeaponId).item_num;
        // �e�e�L�X�g�̒��g����������
        changeLimitBreaklText.text = string.Format("{0}{1}{2}{3}{4}", ConvexStr, currentLimitBreak, arrowStr, ConvexStr, afterLimitBreak);
        consumptionConvexItemText.text = string.Format("{0}{1}{2}{3}{4}", necessaryImteStr, spaceStr, consumptionItem, slashStr, currentItem);
        weaponDataLimitBreakText.text = string.Format("{0}{1}", ConvexStr, currentLimitBreak);
    }

    // �����|�C���g������Ă��邩�m�F���ă{�^�����������Ԃ��ǂ������ʂ���
    void CheckCanLimitBreak()
    {
        consumptionItem = 1;
        currentItem = Items.GetWeaponItemData(limitBreakpWeaponId).item_num;
        currentLimitBreak = Weapons.GetWeaponData(limitBreakpWeaponId).limit_break;

        ChangeImageColor.ChangeMode changeMode = ChangeImageColor.ChangeMode.UNSELECT;

        if (currentLimitBreak < 5)
        {
            if (currentItem >= consumptionItem)
            {
                changeMode = ChangeImageColor.ChangeMode.REINFORCE;
                currentState = UnPushReason.NONE;
            }
            else { currentState = UnPushReason.SHORTAGE; } // �����A�C�e��������Ȃ�������I���ł��Ȃ�����
        }
        else
        { currentState = UnPushReason.MAX; } // ���E�˔j������܂ōs���Ă�����I���ł��Ȃ�����

        changeImageColor.ChangeTargetColor(LimitBreakButton, changeMode);
    }

    // ����̌��E�˔j���s��
    public void LimitBreak()
    {
        // �{�^���������Ȃ����R������΂����\�����ă��^�[��
        switch (currentState)
        {
            case UnPushReason.NONE:
                isPush = true;
                break;
            case UnPushReason.SHORTAGE:
                StartCoroutine(ResultPanelController.DisplayResultPanel("�ʃA�C�e��������܂���"));
                isPush = false;
                break;
            case UnPushReason.MAX:
                StartCoroutine(ResultPanelController.DisplayResultPanel("���E�˔j����ł�"));
                isPush = false;
                break;
        }
        if (!isPush) { return; }
        List<IMultipartFormSection> levelUpForm = new();
        levelUpForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        levelUpForm.Add(new MultipartFormDataSection("wid", limitBreakpWeaponId.ToString()));
        Debug.Log("���E�˔j����");
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.WEAPON_LIMIT_BREAK_URL, levelUpForm));
    }
}
