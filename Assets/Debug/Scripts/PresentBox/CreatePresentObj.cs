using UnityEditor.Presets;
using UnityEngine;

public class CreatePresentObj : MonoBehaviour
{
    [SerializeField] GameObject unReceiptPresentPrefab, receiptedPresentPrefab;
    [SerializeField] GameObject presentsParent;
    GameObject[] unReceiptPresentClones, receiptedPresentClones;

    Vector3 generatePos;

    int presentId, rewardCategory;
    string receiptReason, rewardNum, receiptTerm;

    PresentBoxManager presentBoxManager;
    DisplayPresentsObj displayPresents;

    private void Awake()
    {
        generatePos = new(10000, 10000, 0);
        presentBoxManager = GetComponent<PresentBoxManager>();
        displayPresents = GetComponent<DisplayPresentsObj>();
    }

    // �w�肳�ꂽ�������v���[���g�𐶐�����
    void GeneratePresents(GameObject prefab, GameObject[] clones, PresentBoxModel[] presents)
    {
        int i = 0;
        // �n���ꂽ���f���̕������v���[���g���쐬����
        foreach (var present in presents)
        {
            if (presents.Length - 1 < i) { break; }
            if (presents[i] == null) { break; }
            if (present == null) { continue; }
            SetPresentParameter(present.present_id, present.receive_reason, present.present_box_reward, present.display, present.reward_category);

            clones[i] = Instantiate(prefab, generatePos, Quaternion.identity);
            clones[i].transform.parent = presentsParent.transform;

            PresentManager presentManager = clones[i].GetComponent<PresentManager>();
            presentManager.SetPresentParameter(presentId, receiptReason, rewardNum, receiptTerm, rewardCategory);
            clones[i].SetActive(false);
            i++;
        }
    }

    /// <summary>
    /// �w��̍��W�Ƀv���[���g���쐬
    /// </summary>
    /// <param name="unReceiptPresentModels">�󂯎��O�̃v���[���g�f�[�^</param>
    /// <param name="receiptedPresetModels">���ς̃v���[���g�f�[�^</param>
    public void CreatePresents(PresentBoxModel[] unReceiptPresentModels, PresentBoxModel[] receiptedPresetModels)
    {
        if (presentsParent == null || unReceiptPresentPrefab == null || receiptedPresentPrefab == null) { return; } // �K�v�ȃI�u�W�F�N�g��null�Ȃ�Ԃ�
        if (receiptedPresetModels.Length != 0)
        {
            // �󂯎��O�̃v���[���g�N���[���쐬
            receiptedPresentClones = new GameObject[receiptedPresetModels.Length];
            GeneratePresents(receiptedPresentPrefab, receiptedPresentClones, receiptedPresetModels);
        }
        if (unReceiptPresentModels.Length != 0)
        {
            // ���ς̃v���[���g�N���[���쐬
            unReceiptPresentClones = new GameObject[unReceiptPresentModels.Length]; // �w������쐬
            GeneratePresents(unReceiptPresentPrefab, unReceiptPresentClones, unReceiptPresentModels);
        }
        presentBoxManager.SetPresentClones(unReceiptPresentClones, receiptedPresentClones);
    }

    // �e��ϐ��ɑΉ�����l��ݒ肷��
    void SetPresentParameter(int id, string reason, string rewardNumber, string Term, int category)
    {
        presentId = id;
        receiptReason = reason;
        rewardNum = rewardNumber;
        receiptTerm = Term;
        rewardCategory = category;
    }

}
