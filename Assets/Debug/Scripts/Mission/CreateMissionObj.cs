using System.Collections.Generic;
using UnityEngine;

public class CreateMissionObj : MonoBehaviour
{
    [SerializeField] GameObject missionPrefab;
    [SerializeField] GameObject constancyContext, dailyContext, weeklyContext;
    [SerializeField] List<GameObject> unReceiptMissionClones = new();
    [SerializeField] List<GameObject> receiptedMissionClones = new();

    Vector3 generatePos;

    MissionManager missionManager;

    private void Awake()
    {
        generatePos = new(0, 0, 0);
        missionManager = GetComponent<MissionManager>();
    }

    // 重複チェック
    bool CheckDuplication(GameObject target, bool isReceipt)
    {
        if (isReceipt)
        {
            foreach (var check in receiptedMissionClones)
            {
                if (target == check)
                {
                    Destroy(target);
                    return true;
                }
            }
        }
        else
        {
            foreach (var check in unReceiptMissionClones)
            {
                if (target == check)
                {
                    Destroy(target);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 指定された分だけミッションを生成する
    /// </summary>
    /// <param name="prefab">作成するミッションのプレハブ</param>
    /// <param name="clones">作成するミッションのクローン</param>
    /// <param name="missions">作成するミッションのモデル</param>
    /// <param name="isReceipt">作成するミッションが受取済かどうか(trueが受取済)</param>
    void GenerateMissions(GameObject prefab, List<GameObject> clones, List<MissionsModel> missions, bool isReceipt)
    {
        int i = 0;
        // 渡されたモデルの分だけミッションを作成する
        foreach (var mission in missions)
        {
            if (mission == null) { continue; }
            if (missions.Count - 1 < i) { break; }
            if (missions[i] == null) { break; }

            var addClone = Instantiate(prefab, generatePos, Quaternion.identity);

            if (CheckDuplication(addClone, isReceipt)) { continue; }

            clones.Add(addClone);

            clones[i].transform.parent = constancyContext.transform; // TODO: とりあえず恒常のみ生成、今後batとかの用意ができ次第デイリーやウィークリーも生成できるようにする

            // 作成するミッションの情報を設定
            MissionCloneManager mCM = clones[i].GetComponent<MissionCloneManager>();
            mCM.SetMissionObjParameter(mission);

            i++;
        }
    }

    /// <summary>
    /// 指定の座標にミッションを作成
    /// </summary>
    /// <param name="unReceiptMissionModels">受け取り前のミッションデータ</param>
    /// <param name="receiptedMissionModels">受取済のミッションデータ</param>
    public void CreateMissions(List<MissionsModel> unReceiptMissionModels, List<MissionsModel> receiptedMissionModels)
    {
        if (constancyContext == null || missionPrefab == null) { return; } // 必要なオブジェクトがnullなら返す
        if (receiptedMissionModels.Count != 0)
        {
            // 受取済のミッションクローン作成
            GenerateMissions(missionPrefab, receiptedMissionClones, receiptedMissionModels, true);
        }
        if (unReceiptMissionModels.Count != 0)
        {
            // 受取前のミッションクローン作成
            GenerateMissions(missionPrefab, unReceiptMissionClones, unReceiptMissionModels, false);
        }
        missionManager.SetMissionClones(unReceiptMissionClones, receiptedMissionClones);
    }

    // ミッションが受け取られたときに、受け取り前のミッションを削除して履歴用のミッションを作成する
    public void ReceiptedPresent(GameObject[] targets)
    {
        foreach (var target in targets)
        {
            foreach (var clone in unReceiptMissionClones)
            {
                if (clone == target)
                {
                    unReceiptMissionClones.Remove(clone); //　削除
                    DisplayPresentsObj dis = GetComponent<DisplayPresentsObj>();
                    Destroy(target);
                    dis.RemoveListItem(target);
                    break;
                }
            }
        }
    }
}
