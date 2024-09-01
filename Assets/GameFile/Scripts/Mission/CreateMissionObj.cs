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

    // �d���`�F�b�N
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
            if (mission == null) { continue; }
            if (missions.Count - 1 < i) { break; }
            if (missions[i] == null) { break; }

            var addClone = Instantiate(prefab, generatePos, Quaternion.identity);

            if (CheckDuplication(addClone, isReceipt)) { continue; }

            clones.Add(addClone);

            clones[i].transform.parent = constancyContext.transform; // TODO: �Ƃ肠�����P��̂ݐ����A����bat�Ƃ��̗p�ӂ��ł�����f�C���[��E�B�[�N���[�������ł���悤�ɂ���

            // �쐬����~�b�V�����̏���ݒ�
            MissionCloneManager mCM = clones[i].GetComponent<MissionCloneManager>();
            mCM.SetMissionObjParameter(mission);

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
        if (constancyContext == null || missionPrefab == null) { return; } // �K�v�ȃI�u�W�F�N�g��null�Ȃ�Ԃ�
        if (receiptedMissionModels.Count != 0)
        {
            // ���ς̃~�b�V�����N���[���쐬
            GenerateMissions(missionPrefab, receiptedMissionClones, receiptedMissionModels, true);
        }
        if (unReceiptMissionModels.Count != 0)
        {
            // ���O�̃~�b�V�����N���[���쐬
            GenerateMissions(missionPrefab, unReceiptMissionClones, unReceiptMissionModels, false);
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
    }
}
