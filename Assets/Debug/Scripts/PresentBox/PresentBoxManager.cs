using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PresentBoxManager : MonoBehaviour
{
    [SerializeField] GameObject PresentPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    List<PresentBoxModel> unReceiptPresents = new();   // ���O�̃v���[���g�f�[�^
    List<PresentBoxModel> receiptedPresents = new();   // ���ς̃v���[���g�f�[�^

    public List<PresentBoxModel> ReceiptedPresents { get { return receiptedPresents; } }

    [SerializeField] List<GameObject> unReceiptPresentClones = new();
    List<GameObject> receiptedPresentClones = new();

    public List<GameObject> UnReceiptPresentClones { get { return unReceiptPresentClones; } }
    public List<GameObject> ReceiptedPresentClones { get { return receiptedPresentClones; } }

    CreatePresentObj createPresent;
    DisplayPresentsObj displayPresent;

    int pageNum = 1;
    [SerializeField] int totalPresentsPageNum = 0;
    int totalPresentsNum = 0;
    public int TotalPresentsNum { get { return totalPresentsNum; } }

    bool isSet = false;
    bool displayLog = false; // �\������̂��������ǂ���(true������)

    private void Awake()
    {
        unReceiptPresents = new List<PresentBoxModel>();
        receiptedPresents = new List<PresentBoxModel>();
        createPresent = GetComponent<CreatePresentObj>();
        displayPresent = GetComponent<DisplayPresentsObj>();

        PresentPanel.SetActive(false);

        GetPresentBoxData getPresent = GetComponent<GetPresentBoxData>();
        getPresent.CheckUpdatePresentBox(); // �f�[�^�擾
    }

    void Start()
    {
        totalPresentsPageNum = unReceiptPresents.Count / 5;
        if (unReceiptPresents.Count % 5 > 0) { totalPresentsPageNum++; }
        pageText.text = string.Format("{0}/{1}", pageNum, totalPresentsPageNum);
    }

    // �J�����Ƃ��ɖ��񗚗��ł͂Ȃ����\�ȃv���[���g���\�������悤�ɂ���
    private void OnEnable()
    {
        displayLog = false;
    }

    void Update()
    {
        totalPresentsNum = unReceiptPresentClones.Count; // ���݂̎󂯎���v���[���g�̐����X�V
        ChangePageText();
    }

    // �\������v���[���g�̃y�[�W����ύX
    void ChangePageText()
    {
        if (displayLog)
        {
            totalPresentsPageNum = receiptedPresents.Count / 5;
            if (receiptedPresents.Count % 5 > 0) { totalPresentsPageNum++; }
        }
        else
        {
            totalPresentsPageNum = unReceiptPresents.Count / 5;
            if (unReceiptPresents.Count % 5 > 0) { totalPresentsPageNum++; }
        }
        if (totalPresentsPageNum == 0) { pageNum = 0; }
        pageText.text = string.Format("{0}/{1}", pageNum, totalPresentsPageNum);
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
    public void SetPresentData(List<PresentBoxModel> unReceiptPresentsData, List<PresentBoxModel> receiptedPresentData)
    {
        unReceiptPresents = unReceiptPresentsData;
        receiptedPresents = receiptedPresentData;

        // �ŏ��̈��̂ݐ���
        if (!isSet)
        {
            createPresent.CreatePresents(unReceiptPresents, receiptedPresents);
        }
    }

    // �v���[���g�N���[���ݒ�
    public void SetPresentClones(List<GameObject> unreceipts, List<GameObject> receipteds)
    {
        if (unreceipts != null)
        {
            unReceiptPresentClones = unreceipts;
        }
        if (receipteds != null)
        {
            receiptedPresentClones = receipteds;
        }
        // �ŏ��̈��̂݃v���[���g�{�b�N�X�̂P�y�[�W�ڂ�\��
        if (!isSet)
        {
            pageNum = 1;
            displayPresent.DisplayPresents(pageNum, false);
            isSet = true;
        }
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
        PresentPanel.SetActive(true);
    }

    // �߂�{�^���������ꂽ��p�l�������
    public void OnPushBackButton()
    {
        PresentPanel.SetActive(false);
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
