using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetMissionData : MonoBehaviour
{
    [SerializeField] List<MissionsModel> unReciptMissions = new();  // 受取前のミッションデータ
    [SerializeField] List<MissionsModel> receiptedMissions = new(); // 受取済のミッションデータ

    MissionManager missionManager;

    void Start()
    {
        missionManager = GetComponent<MissionManager>();
        CheckUpdateMission();
    }

    // 成功した場合に呼ぶ関数
    void SuccessGetMissionData()
    {
        Debug.Log("ミッションデータの取得に成功しました。");
        GetPresentData();
    }

    // プレゼントボックスデータを取得する
    public void CheckUpdateMission()
    {
        List<IMultipartFormSection> getMissionsForm = new();
        getMissionsForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        Action afterAction = new(() => SuccessGetMissionData());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.GET_MISSION_URL, getMissionsForm, afterAction));
    }

    // プレゼントのデータを受取済と受取前に分けて取得
    void GetPresentData()
    {
        MissionsModel[] allData = Missions.GetMissionDataAll();

        foreach (var data in allData)
        {
            if (CheckDuplication(data)) { continue; }
            string term = data.term;
            // 表示可能期間かつ受け取っていなければ受取可能な配列に保存、そうでなければ受取済みの配列に保存
            if (missionManager.CheckCanDisplay(term) && data.receipt != 1)
            {
                unReciptMissions.Add(data);
            }
            else
            {
                receiptedMissions.Add(data);
            }
        }
         missionManager.SetMissionData(unReciptMissions, receiptedMissions);
    }

    /// <summary>
    /// データのIDが重複しているかどうかを確認する、重複していたらTrueを返す
    /// </summary>
    /// <param name="target">確認するモデル</param>
    /// <returns></returns>
    bool CheckDuplication(MissionsModel target)
    {
        bool result = false;

        bool isReceipt = target.receipt > 0 ? true : false;

        if (isReceipt)
        {
            foreach (var receiptedData in receiptedMissions)
            {
                if (receiptedData.mission_id == target.mission_id) { result = true; }
            }
        }
        else
        {
            foreach (var unReceiptData in unReciptMissions)
            {
                if (unReceiptData.mission_id == target.mission_id) { result = true; }
            }
        }

        return result;
    }
}
