using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetMissionData : MonoBehaviour
{
    [SerializeField] List<MissionsModel> unReciptMissions = new();  // ���O�̃~�b�V�����f�[�^
    [SerializeField] List<MissionsModel> receiptedMissions = new(); // ���ς̃~�b�V�����f�[�^

    MissionManager missionManager;

    void Start()
    {
        missionManager = GetComponent<MissionManager>();
        CheckUpdateMission();
    }

    // ���������ꍇ�ɌĂԊ֐�
    void SuccessGetMissionData()
    {
        Debug.Log("�~�b�V�����f�[�^�̎擾�ɐ������܂����B");
        GetPresentData();
    }

    // �v���[���g�{�b�N�X�f�[�^���擾����
    public void CheckUpdateMission()
    {
        List<IMultipartFormSection> getMissionsForm = new();
        getMissionsForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        Action afterAction = new(() => SuccessGetMissionData());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.GET_MISSION_URL, getMissionsForm, afterAction));
    }

    // �v���[���g�̃f�[�^�����ςƎ��O�ɕ����Ď擾
    void GetPresentData()
    {
        MissionsModel[] allData = Missions.GetMissionDataAll();

        foreach (var data in allData)
        {
            if (CheckDuplication(data)) { continue; }
            string term = data.term;
            // �\���\���Ԃ��󂯎���Ă��Ȃ���Ύ��\�Ȕz��ɕۑ��A�����łȂ���Ύ��ς݂̔z��ɕۑ�
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
    /// �f�[�^��ID���d�����Ă��邩�ǂ������m�F����A�d�����Ă�����True��Ԃ�
    /// </summary>
    /// <param name="target">�m�F���郂�f��</param>
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
