using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionCloneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI achievementConditionText, progressText, rewradNumText, termText;
    [SerializeField] Image rewardImage;
    [SerializeField] Slider progressSlider;

    int mission_id = -1;
    public int Mission_id { get { return mission_id; } }
    int rewardCategory, // ��V�̃J�e�S��
        rewardNum,      // ���V���󂯎������
        achieved,       // �B���������ǂ���
        achievement_condition, // �B������(�ڕW�l)
        receipt,        // �󂯎�������ǂ���
        progress;       // �i��

    string achievementConditionStr, // �B������(�ڕW)������
        numStr,
        termStr;

    string multiStr = "�~{0}";
    string termStrBeforeStr = "����I����\n{0}";

    // �e�L�X�g��f�[�^�̐ݒ�
    public void SetMissionObjParameter(MissionsModel setTarget)
    {
        UpdateMissionObjParameter(setTarget);
        // �X���C�_�[�̏���ݒ�
        progressSlider.maxValue = achievement_condition;
        // ������̐ݒ�
        MissionMasterModel master = MissionMaster.GetMissionMasterData(mission_id);
        UpdateStrings(master);
    }

    // �e�L�X�g��f�[�^�̍X�V
    public void UpdateMissionObjParameter(MissionsModel setTarget)
    {
        mission_id = setTarget.mission_id;
        MissionMasterModel masterData = MissionMaster.GetMissionMasterData(mission_id);

        rewardCategory = masterData.reward_category;
        achieved = setTarget.achieved;
        achievement_condition = GetAchievementConditionNum(masterData.achievement_condition);
        receipt = setTarget.receipt;
        progress = setTarget.progress;

        progressSlider.value = progress; // �X���C�_�[�̍X�V

        rewardImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
    }

    // �B�������̐����݂̂��擾
    int GetAchievementConditionNum(string target)
    {
        string escapeTarget = target.Remove(0, target.IndexOf("/") + 1); // �n�߂�/���o��ӏ����O���폜
        return int.Parse(escapeTarget);
    }


    // ������̍X�V
    void UpdateStrings(MissionMasterModel setTarget)
    {
        achievementConditionStr = setTarget.mission_content;
        string reward = setTarget.mission_reward;
        numStr = string.Format(multiStr, reward.Remove(0, reward.IndexOf("/") + 1)); // /���O���폜
        termStr = string.Format(termStrBeforeStr, setTarget.period_end);

        achievementConditionText.text = achievementConditionStr;
        progressText.text = string.Format("{0}/{1}", progress, achievement_condition);
        termText.text = termStr;
    }

}
