using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] GameObject missionPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    [SerializeField] List<MissionsModel> unReceiptMissions = new();   // ���O�̃v���[���g�f�[�^
    List<MissionsModel> receiptedMissions = new();   // ���ς̃v���[���g�f�[�^

    public List<MissionsModel> ReceiptedMissions { get { return receiptedMissions; } }

    [SerializeField] List<GameObject> unReceiptMissionClones = new();
    List<GameObject> receiptedMissionClones = new();

    public List<GameObject> UnReceiptMissionClones { get { return unReceiptMissionClones; } }
    public List<GameObject> ReceiptedMissionClones { get { return receiptedMissionClones; } }

    CreateMissionObj createMissions;
    DisplayPresentsObj displayPresent;

    int pageNum = 1;
    [SerializeField] int totalPresentsPageNum = 0;
    int totalMissionsNum = 0;
    public int TotalMissionsNum { get { return totalMissionsNum; } }

    bool isSet = false;
    bool displayLog = false; // �\������̂��������ǂ���(true������)

    private void Awake()
    {
        createMissions = GetComponent<CreateMissionObj>();
        displayPresent = GetComponent<DisplayPresentsObj>();

        //  missionPanel.SetActive(false);

        GetMissionData getMissions = GetComponent<GetMissionData>();
        getMissions.CheckUpdateMission(); // �f�[�^�擾
    }

    // �J�����Ƃ��ɖ��񗚗��ł͂Ȃ����\�ȃv���[���g���\�������悤�ɂ���
    private void OnEnable()
    {
        displayLog = false;
    }

    void Update()
    {
        totalMissionsNum = unReceiptMissionClones.Count; // ���݂̎󂯎���v���[���g�̐����X�V
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

    // �v���[���g�f�[�^��ݒ�
    public void SetMissionData(List<MissionsModel> unReceiptMissionsData, List<MissionsModel> receiptedMissionData)
    {
        unReceiptMissions = unReceiptMissionsData;
        receiptedMissions = receiptedMissionData;

        // �ŏ��̈��̂ݐ���
        if (!isSet)
        {
            createMissions.CreateMissions(unReceiptMissions, receiptedMissions);
        }
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
        // �ŏ��̈��̂݃v���[���g�{�b�N�X�̂P�y�[�W�ڂ�\��
        //if (!isSet)
        //{
        //    pageNum = 1;
        //    displayPresent.DisplayPresents(pageNum, false);
        //    isSet = true;
        //}
    }

    // ���ւ̃{�^���������ꂽ�玟�̃y�[�W��
    public void OnPushNextButton()
    {
        if (pageNum < totalPresentsPageNum)
        {
            pageNum++;
            displayPresent.DisplayPresents(pageNum, displayLog);
        }
    }

    // �O�ւ̃{�^���������ꂽ��O�̃y�[�W��
    public void OnPushPreviousButton()
    {
        if (pageNum > 1)
        {
            pageNum--;
            displayPresent.DisplayPresents(pageNum, displayLog);
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

    // �����{�^���������ꂽ��
    public void OnPushLogButton()
    {
        if (displayLog)
        {
            displayLog = false;
            modeText.text = "��旚��";
        }
        else
        {
            displayLog = true;
            modeText.text = "������";
        }
        pageNum = 1;
        displayPresent.DisplayPresents(pageNum, displayLog);
    }
}
