using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : DisplayTotalTargetNum
{
    [SerializeField] GameObject missionPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    [SerializeField] List<MissionsModel> unReceiptMissions = new();    // 受取前のプレゼントデータ
    [SerializeField] List<MissionsModel> canReceiptMissions = new();   // 受取可能なプレゼントデータ
    List<MissionsModel> receiptedMissions = new();    // 受取済のプレゼントデータ

    public List<MissionsModel> ReceiptedMissions { get { return receiptedMissions; } }

    [SerializeField] List<GameObject> unReceiptMissionClones = new();
    List<GameObject> receiptedMissionClones = new();

    public List<GameObject> UnReceiptMissionClones { get { return unReceiptMissionClones; } }
    public List<GameObject> ReceiptedMissionClones { get { return receiptedMissionClones; } }

    CreateMissionObj createMissions;

    GetMissionData getMissions;

    int totalMissionsNum = 0;
    public int TotalMissionsNum { get { return totalMissionsNum; } }

    bool isSet = false;

    private void Awake()
    {
        createMissions = GetComponent<CreateMissionObj>();

        missionPanel.SetActive(false);

        getMissions = GetComponent<GetMissionData>();
        getMissions.CheckUpdateMission(); // データ取得
    }

    void Update()
    {
        UpdateCanReceiveMission();
        CheckDisplayCount();
    }

    void CheckDisplayCount()
    {
        var total = Missions.GetMissionDataAll();
        int count = 0;
        foreach (var mission in total)
        {
            if (mission.achieved > 0 && mission.receipt == 0)
            {
                count++;
            }
        }
        totalTargetNum = count;
        DisplayText();
    }

    // 現在受け取れるミッションを更新
    void UpdateCanReceiveMission()
    {
        DeleteCanReceiveMissions(); // 受取済のものがあれば削除

        if (unReceiptMissionClones.Count == 0) { return; }

        // 受取可能なミッションを取得
        foreach (var target in unReceiptMissions)
        {
            if (target.achieved == 1 && target.receipt == 0)
            {
                bool isDuplication = CheckDuplication(target);
                if (!isDuplication)
                {
                    canReceiptMissions.Add(target);
                }
            }
        }
    }

    // 表示できるかを確認
    public bool CheckCanDisplay(string dateString)
    {
        string rePlaceDate = dateString.Replace("-", "/");
        DateTime checkDateTime = DateTime.Parse(rePlaceDate);
        DateTime currentTime = DateTime.Now;
        if (checkDateTime > currentTime)
        {
            return true;
        }
        return false;
    }

    // 重複チェック
    bool CheckDuplication(MissionsModel target)
    {
        foreach (var canReceiptMission in canReceiptMissions)
        {
            if (canReceiptMission.mission_id == target.mission_id)
            {
                return true;
            }
        }
        return false;
    }

    // 受取済のものがあればそれを削除
    public void DeleteCanReceiveMissions()
    {
        foreach (var target in canReceiptMissions)
        {
            if (target.receipt != 0)
            {
                canReceiptMissions.Remove(target);
            }
        }
    }

    // プレゼントデータを設定
    public void SetMissionData(List<MissionsModel> unReceiptMissionsData, List<MissionsModel> receiptedMissionData)
    {
        unReceiptMissions = unReceiptMissionsData;
        receiptedMissions = receiptedMissionData;

        // 最初の一回のみ生成
        if (!isSet)
        {
            isSet = true;
            createMissions.CreateMissions(unReceiptMissions, receiptedMissions);
        }

        DeleteCanReceiveMissions();
    }

    // プレゼントクローン設定
    public void SetMissionClones(List<GameObject> unreceipts, List<GameObject> receipteds)
    {
        if (unreceipts != null)
        {
            unReceiptMissionClones = unreceipts;
        }
        if (receipteds != null)
        {
            receiptedMissionClones = receipteds;
        }
    }

    // パネルを開く
    public void OnPushOpenButton()
    {
        missionPanel.SetActive(true);
        getMissions.CheckUpdateMission(); // データ取得
    }

    // 戻るボタンが押されたらパネルを閉じる
    public void OnPushBackButton()
    {
        missionPanel.SetActive(false);
        getMissions.CheckUpdateMission(); // データ取得
    }
}
