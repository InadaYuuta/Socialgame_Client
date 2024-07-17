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

    int unReceiptNum = 0; // ���O�̃~�b�V�����̌�
    int receiptedNum = 0; // ���ς̃~�b�V�����̌�

    MissionManager missionManager;

    private void Awake()
    {
        generatePos = new(0, 0, 0);
        missionManager = GetComponent<MissionManager>();
    }

    /// <summary>
    /// �w�肳�ꂽ�������~�b�V�����𐶐�����
    /// </summary>
    /// <param name="prefab">�쐬����~�b�V�����̃v���n�u</param>
    /// <param name="clones">�쐬����~�b�V�����̃N���[��</param>
    /// <param name="missions">�쐬����~�b�V�����̃��f��</param>
    /// <param name="isReceipt">�쐬����~�b�V���������ς��ǂ���(true������)</param>
    void GenerateMissions(GameObject prefab, List<GameObject> clones, List<MissionsModel> missions, bool isReceipt)
    {
        int i = 0;
        // �n���ꂽ���f���̕������~�b�V�������쐬����
        foreach (var mission in missions)
        {
            if (missions.Count - 1 < i) { break; }
            if (missions[i] == null) { break; }
            if (mission == null) { continue; }

            clones.Add(Instantiate(prefab, generatePos, Quaternion.identity));

            clones[i].transform.parent = constancyContext.transform; // TODO: �Ƃ肠�����P��̂ݐ����A����bat�Ƃ��̗p�ӂ��ł�����f�C���[��E�B�[�N���[�������ł���悤�ɂ���

            // �쐬����~�b�V�����̏���ݒ�
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
    /// �w��̍��W�Ƀ~�b�V�������쐬
    /// </summary>
    /// <param name="unReceiptMissionModels">�󂯎��O�̃~�b�V�����f�[�^</param>
    /// <param name="receiptedMissionModels">���ς̃~�b�V�����f�[�^</param>
    public void CreateMissions(List<MissionsModel> unReceiptMissionModels, List<MissionsModel> receiptedMissionModels)
    {
        if (constancyContext == null || unReceiptMissionPrefab == null || receiptedMissionPrefab == null) { return; } // �K�v�ȃI�u�W�F�N�g��null�Ȃ�Ԃ�
        if (receiptedMissionModels.Count != 0)
        {
            receiptedNum = receiptedMissionModels.Count;
            // ���ς̃~�b�V�����N���[���쐬
            GenerateMissions(receiptedMissionPrefab, receiptedMissionClones, receiptedMissionModels, true);
        }
        if (unReceiptMissionModels.Count != 0)
        {
            unReceiptNum = unReceiptMissionModels.Count;
            // ���O�̃~�b�V�����N���[���쐬
            GenerateMissions(unReceiptMissionPrefab, unReceiptMissionClones, unReceiptMissionModels, false);
        }
        missionManager.SetMissionClones(unReceiptMissionClones, receiptedMissionClones);
    }

    // �~�b�V�������󂯎��ꂽ�Ƃ��ɁA�󂯎��O�̃~�b�V�������폜���ė���p�̃~�b�V�������쐬����
    public void ReceiptedPresent(GameObject[] targets)
    {
        foreach (var target in targets)
        {
            foreach (var clone in unReceiptMissionClones)
            {
                if (clone == target)
                {
                    unReceiptMissionClones.Remove(clone); //�@�폜
                    DisplayPresentsObj dis = GetComponent<DisplayPresentsObj>();
                    Destroy(target);
                    dis.RemoveListItem(target);
                    break;
                }
            }
        }
        //CreateReceiptedPresent(missionManager.ReceiptedMissions);
    }

    // �~�b�V�������󂯎�������Ɏ�旚���ɒǉ�
    void CreateReceiptedPresent(List<MissionsModel> receiptedPresents)
    {
        receiptedNum = receiptedPresents.Count;

        // ���ς̃~�b�V�����N���[���쐬
        GenerateMissions(receiptedMissionPrefab, receiptedMissionClones, receiptedPresents, true);
        missionManager.SetMissionClones(unReceiptMissionClones, receiptedMissionClones);

    }

    // ���O�Ȃ�����������ςȂ������ݒ肷��
    void SetDateParameter(string date)
    {
        receiptDate = date;
    }
}
