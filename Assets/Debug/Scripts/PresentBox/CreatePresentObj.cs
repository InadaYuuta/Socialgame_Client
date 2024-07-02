using UnityEditor.Presets;
using UnityEngine;

public class CreatePresentObj : MonoBehaviour
{
    [SerializeField] GameObject unReceiptPresentPrefab, receiptedPresentPrefab;
    [SerializeField] GameObject presentsParent;
    GameObject[] unReceiptPresentClones, receiptedPresentClones;

    Vector3 generatePos;

    string receiptReason, rewardNum, receiptTerm;
    int rewardCategory;

    PresentBoxManager presentBoxManager;
    DisplayPresentsObj displayPresents;

    private void Awake()
    {
        generatePos = new(10000, 10000, 0);
        presentBoxManager = GetComponent<PresentBoxManager>();
        displayPresents = GetComponent<DisplayPresentsObj>();
    }

    // 指定された分だけプレゼントを生成する
    void GeneratePresents(GameObject prefab, GameObject[] clones, PresentBoxModel[] presents)
    {
        int i = 0;
        // 渡されたモデルの分だけプレゼントを作成する
        foreach (var present in presents)
        {
            if (presents.Length - 1 < i) { break; }
            if (presents[i] == null) { break; }
            if (present == null) { continue; }
            SetPresentParameter(present.receive_reason, present.present_box_reward, present.display, present.reward_category);

            clones[i] = Instantiate(prefab, generatePos, Quaternion.identity);
            clones[i].transform.parent = presentsParent.transform;

            PresentManager presentManager = clones[i].GetComponent<PresentManager>();
            presentManager.SetPresentParameter(receiptReason, rewardNum, receiptTerm, rewardCategory);
            clones[i].SetActive(false);
            i++;
        }
    }

    /// <summary>
    /// 指定の座標にプレゼントを作成
    /// </summary>
    /// <param name="unReceiptPresentModels">受け取り前のプレゼントデータ</param>
    /// <param name="receiptedPresetModels">受取済のプレゼントデータ</param>
    public void CreatePresents(PresentBoxModel[] unReceiptPresentModels, PresentBoxModel[] receiptedPresetModels)
    {
        if (presentsParent == null || unReceiptPresentPrefab == null || receiptedPresentPrefab == null) { return; } // 必要なオブジェクトがnullなら返す
        if (receiptedPresetModels.Length != 0)
        {
            // 受け取り前のプレゼントクローン作成
            receiptedPresentClones = new GameObject[receiptedPresetModels.Length];
            GeneratePresents(receiptedPresentPrefab, receiptedPresentClones, receiptedPresetModels);
        }
        if (unReceiptPresentModels.Length != 0)
        {
            // 受取済のプレゼントクローン作成
            unReceiptPresentClones = new GameObject[unReceiptPresentModels.Length]; // 指定個数分作成
            GeneratePresents(unReceiptPresentPrefab, unReceiptPresentClones, unReceiptPresentModels);
        }
        presentBoxManager.SetPresentClones(unReceiptPresentClones, receiptedPresentClones);
    }

    // 各種変数に対応する値を設定する
    void SetPresentParameter(string reason, string rewardNumber, string Term, int category)
    {
        receiptReason = reason;
        rewardNum = rewardNumber;
        receiptTerm = Term;
        rewardCategory = category;
    }

}
