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
    int rewardCategory, // 報酬のカテゴリ
        rewardNum,      // 幾つ報酬を受け取ったか
        achieved,       // 達成したかどうか
        achievement_condition, // 達成条件(目標値)
        receipt,        // 受け取ったかどうか
        progress;       // 進捗

    string achievementConditionStr, // 達成条件(目標)が何か
        numStr,
        termStr;

    string multiStr = "×{0}";
    string termStrBeforeStr = "挑戦終了日\n{0}";

    // テキストやデータの設定
    public void SetMissionObjParameter(MissionsModel setTarget)
    {
        UpdateMissionObjParameter(setTarget);
        // スライダーの上限設定
        progressSlider.maxValue = achievement_condition;
        // 文字列の設定
        MissionMasterModel master = MissionMaster.GetMissionMasterData(mission_id);
        UpdateStrings(master);
    }

    // テキストやデータの更新
    public void UpdateMissionObjParameter(MissionsModel setTarget)
    {
        mission_id = setTarget.mission_id;
        MissionMasterModel masterData = MissionMaster.GetMissionMasterData(mission_id);

        rewardCategory = masterData.reward_category;
        achieved = setTarget.achieved;
        achievement_condition = GetAchievementConditionNum(masterData.achievement_condition);
        receipt = setTarget.receipt;
        progress = setTarget.progress;

        progressSlider.value = progress; // スライダーの更新

        rewardImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resourcesフォルダの中の特定の画像を取得して入れる
    }

    // 達成条件の数字のみを取得
    int GetAchievementConditionNum(string target)
    {
        string escapeTarget = target.Remove(0, target.IndexOf("/") + 1); // 始めに/が出る箇所より前を削除
        return int.Parse(escapeTarget);
    }


    // 文字列の更新
    void UpdateStrings(MissionMasterModel setTarget)
    {
        achievementConditionStr = setTarget.mission_content;
        string reward = setTarget.mission_reward;
        numStr = string.Format(multiStr, reward.Remove(0, reward.IndexOf("/") + 1)); // /より前を削除
        termStr = string.Format(termStrBeforeStr, setTarget.period_end);

        achievementConditionText.text = achievementConditionStr;
        progressText.text = string.Format("{0}/{1}", progress, achievement_condition);
        termText.text = termStr;
    }

}
