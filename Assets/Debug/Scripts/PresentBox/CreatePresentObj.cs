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

    int unReceiptNum = 0; // ���O�̃v���[���g�̌�
    int receiptedNum = 0; // ���ς̃v���[���g�̌�

    PresentBoxManager presentBoxManager;

    private void Awake()
    {
        generatePos = new(10000, 10000, 0);
        presentBoxManager = GetComponent<PresentBoxManager>();
    }

    /// <summary>
    /// �w�肳�ꂽ�������v���[���g�𐶐�����
    /// </summary>
    /// <param name="prefab">�쐬����v���[���g�̃v���n�u</param>
    /// <param name="clones">�쐬����v���[���g�̃N���[��</param>
    /// <param name="presents">�쐬����v���[���g�̃��f��</param>
    /// <param name="isReceipt">�쐬����v���[���g�����ς��ǂ���(true������)</param>
    void GeneratePresents(GameObject prefab, List<GameObject> clones, List<PresentBoxModel> presents, bool isReceipt)
    {
        int i = 0;
        // �n���ꂽ���f���̕������v���[���g���쐬����
        foreach (var present in presents)
        {
            if (presents.Count - 1 < i) { break; }
            if (presents[i] == null) { break; }
            if (present == null) { continue; }

            clones.Add(Instantiate(prefab, generatePos, Quaternion.identity));
            clones[i].transform.parent = presentsParent.transform;

            // �쐬����v���[���g�̏���ݒ�
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
    /// �w��̍��W�Ƀv���[���g���쐬
    /// </summary>
    /// <param name="unReceiptPresentModels">�󂯎��O�̃v���[���g�f�[�^</param>
    /// <param name="receiptedPresetModels">���ς̃v���[���g�f�[�^</param>
    public void CreatePresents(List<PresentBoxModel> unReceiptPresentModels, List<PresentBoxModel> receiptedPresetModels)
    {
        if (presentsParent == null || unReceiptPresentPrefab == null || receiptedPresentPrefab == null) { return; } // �K�v�ȃI�u�W�F�N�g��null�Ȃ�Ԃ�
        if (receiptedPresetModels.Count != 0)
        {
            receiptedNum = receiptedPresetModels.Count;
            // ���O�̃v���[���g�N���[���쐬
            GeneratePresents(receiptedPresentPrefab, receiptedPresentClones, receiptedPresetModels, true);
        }
        if (unReceiptPresentModels.Count != 0)
        {
            unReceiptNum = unReceiptPresentModels.Count;
            // ���ς̃v���[���g�N���[���쐬
            GeneratePresents(unReceiptPresentPrefab, unReceiptPresentClones, unReceiptPresentModels, false);
        }
        presentBoxManager.SetPresentClones(unReceiptPresentClones, receiptedPresentClones);
    }

    // �v���[���g���󂯎��ꂽ�Ƃ��ɁA�󂯎��O�̃v���[���g���폜���ė���p�̃v���[���g���쐬����
    public void ReceiptedPresent(GameObject[] targets)
    {
        foreach (var target in targets)
        {
            foreach (var clone in unReceiptPresentClones)
            {
                if (clone == target)
                {
                    unReceiptPresentClones.Remove(clone); //�@�폜
                    DisplayPresentsObj dis = GetComponent<DisplayPresentsObj>();
                    Destroy(target);
                    dis.RemoveListItem(target);
                    break;
                }
            }
        }
        CreateReceiptedPresent(presentBoxManager.ReceiptedPresents);
    }

    // �v���[���g���󂯎�������Ɏ�旚���ɒǉ�
    void CreateReceiptedPresent(List<PresentBoxModel> receiptedPresents)
    {
        receiptedNum = receiptedPresents.Count;

        // ���ς̃v���[���g�N���[���쐬
        GeneratePresents(receiptedPresentPrefab, receiptedPresentClones, receiptedPresents, true);
        presentBoxManager.SetPresentClones(unReceiptPresentClones, receiptedPresentClones);

    }

    // �e��ϐ��ɑΉ�����l��ݒ肷��
    void SetPresentParameter(int id, string reason, string rewardNumber, int category)
    {
        presentId = id;
        receiptReason = reason;
        rewardNum = rewardNumber;
        rewardCategory = category;
    }

    // ���O�Ȃ�����������ςȂ������ݒ肷��
    void SetDateParameter(string date)
    {
        receiptDate = date;
    }
}
