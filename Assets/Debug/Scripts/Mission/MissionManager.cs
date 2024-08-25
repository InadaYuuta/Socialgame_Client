using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : DisplayTotalTargetNum
{
    [SerializeField] GameObject missionPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    [SerializeField] List<MissionsModel> unReceiptMissions = new();    // ���O�̃v���[���g�f�[�^
    [SerializeField] List<MissionsModel> canReceiptMissions = new();   // ���\�ȃv���[���g�f�[�^
    List<MissionsModel> receiptedMissions = new();    // ���ς̃v���[���g�f�[�^

    public List<MissionsModel> ReceiptedMissions { get { return receiptedMissions; } }

    [SerializeField] List<GameObject> unReceiptMissionClones = new();
    List<GameObject> receiptedMissionClones = new();

    public List<GameObject> UnReceiptMissionClones { get { return unReceiptMissionClones; } }
    public List<GameObject> ReceiptedMissionClones { get { return receiptedMissionClones; } }

    CreateMissionObj createMissions;

    int totalMissionsNum = 0;
    public int TotalMissionsNum { get { return totalMissionsNum; } }

    bool isSet = false;

    private void Awake()
    {
        createMissions = GetComponent<CreateMissionObj>();

        missionPanel.SetActive(false);

        GetMissionData getMissions = GetComponent<GetMissionData>();
        getMissions.CheckUpdateMission(); // �f�[�^�擾
    }

    void Update()
    {
        UpdateCanReceiveMission();
        totalTargetNum = canReceiptMissions.Count;
        DisplayText();
    }

    // ���ݎ󂯎���~�b�V�������X�V
    void UpdateCanReceiveMission()
    {
        DeleteCanReceiveMissions(); // ���ς̂��̂�����΍폜

        if (unReceiptMissionClones.Count == 0) { return; }

        // ���\�ȃ~�b�V�������擾
        foreach (var target in unReceiptMissions)
        {
            if (target.achieved == 1 && target.receipt == 0)
            {
                bool isDuplication = CheckDuplication(target);
                if (!isDuplication)
                {
                    canReceiptMissions.Add(target);
                }
            }
        }
    }

    // �\���ł��邩���m�F
    public bool CheckCanDisplay(string dateString)
    {
        string rePlaceDate = dateString.Replace("-", "/");
        DateTime checkDateTime = DateTime.Parse(rePlaceDate);
        DateTime currentTime = DateTime.Now;
        if (checkDateTime > currentTime)
        {
            return true;
        }
        return false;
    }

    // �d���`�F�b�N
    bool CheckDuplication(MissionsModel target)
    {
        foreach (var canReceiptMission in canReceiptMissions)
        {
            if (canReceiptMission.mission_id == target.mission_id)
            {
                return true;
            }
        }
        return false;
    }

    // ���ς̂��̂�����΂�����폜
    void DeleteCanReceiveMissions()
    {
        foreach (var target in canReceiptMissions)
        {
            if (target.receipt != 0)
            {
                canReceiptMissions.Remove(target);
            }
        }
    }

    // �v���[���g�f�[�^��ݒ�
    public void SetMissionData(List<MissionsModel> unReceiptMissionsData, List<MissionsModel> receiptedMissionData)
    {
        unReceiptMissions = unReceiptMissionsData;
        receiptedMissions = receiptedMissionData;

        // �ŏ��̈��̂ݐ���
        if (!isSet)
        {
            createMissions.CreateMissions(unReceiptMissions, receiptedMissions);
            isSet = true;
        }

        DeleteCanReceiveMissions();
    }

    // �v���[���g�N���[���ݒ�
    public void SetMissionClones(List<GameObject> unreceipts, List<GameObject> receipteds)
    {
        if (unreceipts != null)
        {
            unReceiptMissionClones = unreceipts;
        }
        if (receipteds != null)
        {
            receiptedMissionClones = receipteds;
        }
    }

    // �p�l�����J��
    public void OnPushOpenButton()
    {
        missionPanel.SetActive(true);
    }

    // �߂�{�^���������ꂽ��p�l�������
    public void OnPushBackButton()
    {
        missionPanel.SetActive(false);
    }
}
