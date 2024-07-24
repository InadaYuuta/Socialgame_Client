using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionCloneManager : MissionCloneBase
{
    [SerializeField] TextMeshProUGUI achievementConditionText, progressText, rewardNumText, termText;
    [SerializeField] Image rewardImage, receiveButtonImage, missionImage, sliderImage;
    [SerializeField] Slider progressSlider;
    [SerializeField] GameObject receiveButton;

    GameObject receivePanel;

    int mission_id = -1;
    public int Mission_id { get { return mission_id; } }

    int updateProgress = 0;
    int oldProgress = -1;

    int rewardCategory; // ��V�̃J�e�S��
    int achieved;       // �B���������ǂ���
    int achievement_condition; // �B������(�ڕW�l)
    int progress;       // �i��

    string achievementConditionStr, // �B������(�ڕW)������
        numStr,  // ��V�̐�
        termStr; // ��V����\����

    string multiStr = "�~{0}";
    string termStrBeforeStr = "����I����\n{0}";

    bool canReceive = false;
    bool isReceipt = false; // �󂯎��ꂽ��Ԃ��ǂ���

    UpdateMission updateMission;
    ReceiveMission receiveMission;

    void Awake()
    {
        base.Awake();
        updateMission = FindAnyObjectByType<UpdateMission>();
        receiveMission = FindAnyObjectByType<ReceiveMission>();
        receivePanel = receiveMission.ReceivePanel;
    }

    void Start()
    {
        CheckCondition(mission_id);
        if (mission_id > -1)
        {
            UpdateProgress();
        }
        receiveMission = FindAnyObjectByType<ReceiveMission>();
    }

    void OnEnable()
    {
        if (mission_id > -1)
        {
            UpdateProgress();
        }
    }

    private void Update()
    {
        if (isReceipt) { return; }
        if (achieved > 0) // �󂯎��\��ԂȂ�F��ς��Ď�惁�\�b�h���Ăׂ�悤�ɂ���
        {
            receiveButtonImage.color = new Color(1, 0.6572623f, 0);
            canReceive = true;
        }

        if (mission_id > -1 && Missions.GetPresentBoxData(mission_id).receipt > 0)
        {
            isReceipt = true;
            MissionReceivedAfterMove();
        }
    }

    // �e�L�X�g��f�[�^�̐ݒ�
    public void SetMissionObjParameter(MissionsModel setTarget)
    {
        if (isReceipt) { return; }
        UpdateMissionObjParameter(setTarget);
        // �X���C�_�[�̏���ݒ�
        progressSlider.minValue = 0;
        progressSlider.maxValue = achievement_condition;
        // ������̐ݒ�
        MissionMasterModel master = MissionMaster.GetMissionMasterData(mission_id);
        UpdateStrings(master);
        CheckCondition(mission_id);
        progressSlider.value = progress; // �X���C�_�[�̍X�V
    }

    // �e�L�X�g��f�[�^�̍X�V
    public void UpdateMissionObjParameter(MissionsModel setTarget)
    {
        if (isReceipt) { return; }
        mission_id = setTarget.mission_id;
        MissionMasterModel masterData = MissionMaster.GetMissionMasterData(mission_id);

        rewardCategory = masterData.reward_category;
        achieved = setTarget.achieved;
        achievement_condition = GetAchievementConditionNum(masterData.achievement_condition);
        progress = setTarget.progress;


        rewardImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
        oldProgress = progress;

        progressSlider.value = progress; // �X���C�_�[�̍X�V
    }

    // �B�������̐����݂̂��擾
    int GetAchievementConditionNum(string target)
    {
        if (isReceipt) { return 0; }
        if (target != null && !string.IsNullOrEmpty(target))
        {
            string escapeTarget = target.Remove(0, target.IndexOf("/") + 1); // �n�߂�/���o��ӏ����O���폜
            return int.Parse(escapeTarget);
        }
        return 0;
    }

    // �X�V��ɌĂԃ��\�b�h
    void MissionUpdateAfterMove()
    {
        if (isReceipt) { return; }
        if (mission_id == -1) { return; }
        MissionsModel updateModel = Missions.GetPresentBoxData(mission_id);
        SetMissionObjParameter(updateModel);
    }

    // ����ɌĂԃ��\�b�h
    void MissionReceivedAfterMove()
    {
        if (mission_id == -1) { return; }
        MissionsModel updateModel = Missions.GetPresentBoxData(mission_id);
        SetMissionObjParameter(updateModel);

        // �{�^���Ǝ����Ԃ��\���ɂ��Ď󂯎��Ȃ��悤�ɂ���
        termText.gameObject.SetActive(false);
        receiveButton.SetActive(false);
        // �C���[�W�̐F���D�F�ɂ��Ď󂯎�������Ƃ��ڎ��Ŋm�F�ł���悤�ɂ���
        missionImage.color = new Color(0.5f, 0.5f, 0.5f);
        sliderImage.color = new Color(0.7f, 0.4f, 0.4f);

        //�@�q�G�����L�[�ň�ԉ��Ɉړ����A��ԑO�ɕ\�������
        transform.SetAsLastSibling();

        receivePanel.SetActive(false);
    }

    // �i�����m�F���čX�V(Start��OnEnable�ŌĂԁA����K�`�����̃~�b�V�����̐i�����i�ލۂɍX�V����悤�ɕς��Ă�������������Ȃ�)
    protected void UpdateProgress()
    {
        if (isReceipt) { return; }
        if (mission_id == -1 || oldProgress == -1) { return; }
        switch (thisCondition)
        {
            case ConditionOfAchievement.Gacha:
                updateProgress = updateData.PullGachaCount;
                break;
            case ConditionOfAchievement.Login:
                updateProgress = updateData.LoginCount;
                break;
            case ConditionOfAchievement.GetWeapon:
                updateProgress = updateData.GetWeaponCount;
                break;
            case ConditionOfAchievement.LevelUp:
                updateProgress = updateData.TotalLevelCount;
                break;
            case ConditionOfAchievement.Evolution:
                updateProgress = updateData.EvolutionCount;
                break;
        }

        if (updateProgress > oldProgress)
        {
            updateMission.StartUpdateMission(mission_id, updateProgress, MissionUpdateAfterMove);
        }

        oldProgress = progress;
    }

    // ������̍X�V
    void UpdateStrings(MissionMasterModel setTarget)
    {
        if (isReceipt) { return; }
        achievementConditionStr = setTarget.mission_content;
        string reward = setTarget.mission_reward;
        if (reward != null)
        {
            numStr = string.Format(multiStr, reward.Remove(0, reward.IndexOf("/") + 1)); // /���O���폜
        }
        if (setTarget.period_end != null)
        {
            termStr = string.Format(termStrBeforeStr, setTarget.period_end);
        }

        // �i�����B���l�𒴂��Ă�����A�B���l��\������
        int displayProgress = progress > achievement_condition ? achievement_condition : progress;

        achievementConditionText.text = achievementConditionStr;
        progressText.text = string.Format("{0}/{1}", displayProgress, achievement_condition);
        termText.text = termStr;
        rewardNumText.text = numStr;
    }

    // �󂯎��
    public void OnPushReceiveButton()
    {
        if (isReceipt) { return; }
        if (canReceive)
        {
            receivePanel.SetActive(true);
            receiveMission.StartReceiveMission(mission_id, MissionReceivedAfterMove);
        }
    }

    // ----------------------����������A�󂯎��������σv���[���g���쐬���鏈���̒ǉ��Ǝ󂯎���Ă���Œ��N���b�N�ł��Ȃ��悤�Ƀp�l���̓W�J�A�󂯎��ł��邩�̊m�F-------------------
}
