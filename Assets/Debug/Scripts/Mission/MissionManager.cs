using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : DisplayTotalTargetNum
{
    [SerializeField] GameObject missionPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    [SerializeField] List<MissionsModel> unReceiptMissions = new();   // 受取前のプレゼントデータ
    [SerializeField] List<MissionsModel> canReceiptMissions = new();   // 受取可能なプレゼントデータ
    List<MissionsModel> receiptedMissions = new();    // 受取済のプレゼントデータ

    public List<MissionsModel> ReceiptedMissions { get { return receiptedMissions; } }

    [SerializeField] List<GameObject> unReceiptMissionClones = new();
    List<GameObject> receiptedMissionClones = new();

    public List<GameObject> UnReceiptMissionClones { get { return unReceiptMissionClones; } }
    public List<GameObject> ReceiptedMissionClones { get { return receiptedMissionClones; } }

    CreateMissionObj createMissions;

    int totalMissionsNum = 0;
    public int TotalMissionsNum { get { return totalMissionsNum; } }

    bool isSet = false;

    private void Awake()
    {
        createMissions = GetComponent<CreateMissionObj>();

        missionPanel.SetActive(false);

        GetMissionData getMissions = GetComponent<GetMissionData>();
        getMissions.CheckUpdateMission(); // データ取得
    }

    void Update()
    {
        totalTargetNum = canReceiptMissions.Count;
        DisplayText();
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
            if (canReceiptMission.mission_id != target.mission_id)
            {
                return true;
            }
        }
        return false;
    }

    // 受取済のものがあればそれを削除
    void DeleteCanReceiveMissions()
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
            createMissions.CreateMissions(unReceiptMissions, receiptedMissions);
            isSet = true;
        }

        DeleteCanReceiveMissions();

        // 受取可能なものを保存
        foreach (var target in unReceiptMissionsData)
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
    }

    // 戻るボタンが押されたらパネルを閉じる
    public void OnPushBackButton()
    {
        missionPanel.SetActive(false);
    }
}
