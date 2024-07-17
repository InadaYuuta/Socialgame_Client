using System.Collections.Generic;
using UnityEngine;

public class CreateMissionObj : MonoBehaviour
{
    [SerializeField] GameObject unReceiptMissionPrefab, receiptedMissionPrefab;
    [SerializeField] GameObject constancyContext, dailyContext, weeklyContext;
    [SerializeField] List<GameObject> unReceiptMissionClones = new();
    [SerializeField] List<GameObject> receiptedMissionClones = new();

    Vector3 generatePos;

    int missionId, rewardCategory;
    string receiptReason, rewardNum, receiptDate;

    int unReceiptNum = 0; // 受取前のミッションの個数
    int receiptedNum = 0; // 受取済のミッションの個数

    MissionManager missionManager;

    private void Awake()
    {
        generatePos = new(0, 0, 0);
        missionManager = GetComponent<MissionManager>();
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
            if (missions.Count - 1 < i) { break; }
            if (missions[i] == null) { break; }
            if (mission == null) { continue; }

            clones.Add(Instantiate(prefab, generatePos, Quaternion.identity));

            clones[i].transform.parent = constancyContext.transform; // TODO: とりあえず恒常のみ生成、今後batとかの用意ができ次第デイリーやウィークリーも生成できるようにする

            // 作成するミッションの情報を設定
            if (!isReceipt)
            {
                MissionCloneManager mCM = clones[i].GetComponent<MissionCloneManager>();
                mCM.SetMissionObjParameter(mission);
                //SetDateParameter(present.receipt_date);
                //ReceiptedPresentManager receiptedPresentManager = clones[i].GetComponent<ReceiptedPresentManager>();
                //receiptedPresentManager.SetPresentParameter(missionId, receiptReason, rewardNum, receiptDate, rewardCategory);
            }
            else
            {
                //  SetDateParameter(present.display);
                //PresentManager presentManager = clones[i].GetComponent<PresentManager>();
                //presentManager.SetPresentParameter(missionId, receiptReason, rewardNum, receiptDate, rewardCategory);
            }

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
        if (constancyContext == null || unReceiptMissionPrefab == null || receiptedMissionPrefab == null) { return; } // 必要なオブジェクトがnullなら返す
        if (receiptedMissionModels.Count != 0)
        {
            receiptedNum = receiptedMissionModels.Count;
            // 受取済のミッションクローン作成
            GenerateMissions(receiptedMissionPrefab, receiptedMissionClones, receiptedMissionModels, true);
        }
        if (unReceiptMissionModels.Count != 0)
        {
            unReceiptNum = unReceiptMissionModels.Count;
            // 受取前のミッションクローン作成
            GenerateMissions(unReceiptMissionPrefab, unReceiptMissionClones, unReceiptMissionModels, false);
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
        //CreateReceiptedPresent(missionManager.ReceiptedMissions);
    }

    // ミッションを受け取った時に受取履歴に追加
    void CreateReceiptedPresent(List<MissionsModel> receiptedPresents)
    {
        receiptedNum = receiptedPresents.Count;

        // 受取済のミッションクローン作成
        GenerateMissions(receiptedMissionPrefab, receiptedMissionClones, receiptedPresents, true);
        missionManager.SetMissionClones(unReceiptMissionClones, receiptedMissionClones);

    }

    // 受取前なら受取期限を受取済なら受取日を設定する
    void SetDateParameter(string date)
    {
        receiptDate = date;
    }
}
