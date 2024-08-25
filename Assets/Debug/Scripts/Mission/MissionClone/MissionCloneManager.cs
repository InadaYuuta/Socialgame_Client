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

    int rewardCategory; // 報酬のカテゴリ
    int achieved;       // 達成したかどうか
    int achievement_condition; // 達成条件(目標値)
    int progress;       // 進捗

    string achievementConditionStr, // 達成条件(目標)が何か
        numStr,  // 報酬の数
        termStr; // 報酬挑戦可能期間

    string multiStr = "×{0}";
    string termStrBeforeStr = "挑戦終了日\n{0}";

    bool canReceive = false;
    bool isReceipt = false; // 受け取られた状態かどうか

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
        if (mission_id > -1)
        {
            UpdateProgress();
        }
        if (achieved > 0) // 受け取り可能状態なら色を変えて受取メソッドを呼べるようにする
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

    // テキストやデータの設定
    public void SetMissionObjParameter(MissionsModel setTarget)
    {
        if (isReceipt) { return; }
        UpdateMissionObjParameter(setTarget);
        // スライダーの上限設定
        progressSlider.minValue = 0;
        progressSlider.maxValue = achievement_condition;
        // 文字列の設定
        MissionMasterModel master = MissionMaster.GetMissionMasterData(mission_id);
        UpdateStrings(master);
        CheckCondition(mission_id);
        progressSlider.value = progress; // スライダーの更新
    }

    // テキストやデータの更新
    public void UpdateMissionObjParameter(MissionsModel setTarget)
    {
        if (isReceipt) { return; }
        mission_id = setTarget.mission_id;
        MissionMasterModel masterData = MissionMaster.GetMissionMasterData(mission_id);

        rewardCategory = masterData.reward_category;
        achieved = setTarget.achieved;
        achievement_condition = GetAchievementConditionNum(masterData.achievement_condition);
        progress = setTarget.progress;


        rewardImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resourcesフォルダの中の特定の画像を取得して入れる
        oldProgress = progress;

        progressSlider.value = progress; // スライダーの更新
    }

    // 達成条件の数字のみを取得
    int GetAchievementConditionNum(string target)
    {
        if (isReceipt) { return 0; }
        if (target != null && !string.IsNullOrEmpty(target))
        {
            string escapeTarget = target.Remove(0, target.IndexOf("/") + 1); // 始めに/が出る箇所より前を削除
            return int.Parse(escapeTarget);
        }
        return 0;
    }

    // 更新後に呼ぶメソッド
    void MissionUpdateAfterMove()
    {
        if (isReceipt) { return; }
        if (mission_id == -1) { return; }
        MissionsModel updateModel = Missions.GetPresentBoxData(mission_id);
        SetMissionObjParameter(updateModel);
    }

    // 受取後に呼ぶメソッド
    void MissionReceivedAfterMove()
    {
        if (mission_id == -1) { return; }
        MissionsModel updateModel = Missions.GetPresentBoxData(mission_id);
        SetMissionObjParameter(updateModel);

        // ボタンと受取期間を非表示にして受け取れないようにする
        termText.gameObject.SetActive(false);
        receiveButton.SetActive(false);
        // イメージの色を灰色にして受け取ったことが目視で確認できるようにする
        missionImage.color = new Color(0.5f, 0.5f, 0.5f);
        sliderImage.color = new Color(0.7f, 0.4f, 0.4f);

        //　ヒエラルキーで一番下に移動し、一番前に表示される
        transform.SetAsLastSibling();

        receivePanel.SetActive(false);
    }

    // 進捗を確認して更新(StartとOnEnableで呼ぶ、今後ガチャ等のミッションの進捗が進む際に更新するように変えてもいいかもしれない)
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

    // 文字列の更新
    void UpdateStrings(MissionMasterModel setTarget)
    {
        if (isReceipt) { return; }
        achievementConditionStr = setTarget.mission_content;
        string reward = setTarget.mission_reward;
        if (reward != null)
        {
            numStr = string.Format(multiStr, reward.Remove(0, reward.IndexOf("/") + 1)); // /より前を削除
        }
        if (setTarget.period_end != null)
        {
            termStr = string.Format(termStrBeforeStr, setTarget.period_end);
        }

        // 進捗が達成値を超えていたら、達成値を表示する
        int displayProgress = progress > achievement_condition ? achievement_condition : progress;

        achievementConditionText.text = achievementConditionStr;
        progressText.text = string.Format("{0}/{1}", displayProgress, achievement_condition);
        termText.text = termStr;
        rewardNumText.text = numStr;
    }

    // 受け取り
    public void OnPushReceiveButton()
    {
        if (isReceipt) { return; }
        if (canReceive)
        {
            receivePanel.SetActive(true);
            receiveMission.StartReceiveMission(mission_id, MissionReceivedAfterMove);
        }
    }

    // ----------------------次ここから、受け取った後受取済プレゼントを作成する処理の追加と受け取っている最中クリックできないようにパネルの展開、受け取りできるかの確認-------------------
}
