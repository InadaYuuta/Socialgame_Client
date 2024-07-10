using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePresentObj : MonoBehaviour
{
    [SerializeField] GameObject unReceiptPresentPrefab, receiptedPresentPrefab;
    [SerializeField] GameObject presentsParent;
    List<GameObject> unReceiptPresentClones = new List<GameObject>();
    List<GameObject> receiptedPresentClones = new List<GameObject>();

    Vector3 generatePos;

    int presentId, rewardCategory;
    string receiptReason, rewardNum, receiptDate;

    int unReceiptNum = 0; // 受取前のプレゼントの個数
    int receiptedNum = 0; // 受取済のプレゼントの個数

    PresentBoxManager presentBoxManager;

    private void Awake()
    {
        generatePos = new(10000, 10000, 0);
        presentBoxManager = GetComponent<PresentBoxManager>();
    }

    /// <summary>
    /// 指定された分だけプレゼントを生成する
    /// </summary>
    /// <param name="prefab">作成するプレゼントのプレハブ</param>
    /// <param name="clones">作成するプレゼントのクローン</param>
    /// <param name="presents">作成するプレゼントのモデル</param>
    /// <param name="isReceipt">作成するプレゼントが受取済かどうか(trueが受取済)</param>
    void GeneratePresents(GameObject prefab, List<GameObject> clones, List<PresentBoxModel> presents, bool isReceipt)
    {
        int i = 0;
        // 渡されたモデルの分だけプレゼントを作成する
        foreach (var present in presents)
        {
            if (presents.Count - 1 < i) { break; }
            if (presents[i] == null) { break; }
            if (present == null) { continue; }

            clones.Add(Instantiate(prefab, generatePos, Quaternion.identity));
            clones[i].transform.parent = presentsParent.transform;

            // 作成するプレゼントの情報を設定
            SetPresentParameter(present.present_id, present.receive_reason, present.present_box_reward, present.reward_category);
            if (isReceipt)
            {
                SetDateParameter(present.receipt_date);
                ReceiptedPresentManager receiptedPresentManager = clones[i].GetComponent<ReceiptedPresentManager>();
                receiptedPresentManager.SetPresentParameter(presentId, receiptReason, rewardNum, receiptDate, rewardCategory);
            }
            else
            {
                SetDateParameter(present.display);
                PresentManager presentManager = clones[i].GetComponent<PresentManager>();
                presentManager.SetPresentParameter(presentId, receiptReason, rewardNum, receiptDate, rewardCategory);
            }

            clones[i].SetActive(false);
            i++;
        }
    }

    /// <summary>
    /// 指定の座標にプレゼントを作成
    /// </summary>
    /// <param name="unReceiptPresentModels">受け取り前のプレゼントデータ</param>
    /// <param name="receiptedPresetModels">受取済のプレゼントデータ</param>
    public void CreatePresents(List<PresentBoxModel> unReceiptPresentModels, List<PresentBoxModel> receiptedPresetModels)
    {
        if (presentsParent == null || unReceiptPresentPrefab == null || receiptedPresentPrefab == null) { return; } // 必要なオブジェクトがnullなら返す
        if (receiptedPresetModels.Count != 0)
        {
            receiptedNum = receiptedPresetModels.Count;
            // 受取前のプレゼントクローン作成
            GeneratePresents(receiptedPresentPrefab, receiptedPresentClones, receiptedPresetModels, true);
        }
        if (unReceiptPresentModels.Count != 0)
        {
            unReceiptNum = unReceiptPresentModels.Count;
            // 受取済のプレゼントクローン作成
            GeneratePresents(unReceiptPresentPrefab, unReceiptPresentClones, unReceiptPresentModels, false);
        }
        presentBoxManager.SetPresentClones(unReceiptPresentClones, receiptedPresentClones);
    }

    // プレゼントが受け取られたときに、受け取り前のプレゼントを削除して履歴用のプレゼントを作成する
    public void ReceiptedPresent(GameObject[] targets)
    {
        foreach (var target in targets)
        {
            foreach (var clone in unReceiptPresentClones)
            {
                if (clone == target)
                {
                    unReceiptPresentClones.Remove(clone); //　削除
                    DisplayPresentsObj dis = GetComponent<DisplayPresentsObj>();
                    Destroy(target);
                    dis.RemoveListItem(target);
                    break;
                }
            }
        }
        CreateReceiptedPresent(presentBoxManager.ReceiptedPresents);
    }

    // プレゼントを受け取った時に受取履歴に追加
    void CreateReceiptedPresent(List<PresentBoxModel> receiptedPresents)
    {
        receiptedNum = receiptedPresents.Count;

        // 受取済のプレゼントクローン作成
        GeneratePresents(receiptedPresentPrefab, receiptedPresentClones, receiptedPresents, true);
        presentBoxManager.SetPresentClones(unReceiptPresentClones, receiptedPresentClones);

    }

    // 各種変数に対応する値を設定する
    void SetPresentParameter(int id, string reason, string rewardNumber, int category)
    {
        presentId = id;
        receiptReason = reason;
        rewardNum = rewardNumber;
        rewardCategory = category;
    }

    // 受取前なら受取期限を受取済なら受取日を設定する
    void SetDateParameter(string date)
    {
        receiptDate = date;
    }
}
