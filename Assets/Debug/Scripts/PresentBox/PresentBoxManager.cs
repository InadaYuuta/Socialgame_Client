using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentBoxManager : MonoBehaviour
{
    [SerializeField] GameObject PresentPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    PresentBoxModel[] unReceiptPresents;   // ���O�̃v���[���g�f�[�^
    PresentBoxModel[] receiptedPresents;   // ���ς̃v���[���g�f�[�^

    GameObject[] unReceiptPresentClones;
    GameObject[] receiptedPresentClones;

    public GameObject[] UnReceiptPresentClones { get { return unReceiptPresentClones; } }
    public GameObject[] ReceiptedPresentClones { get { return receiptedPresentClones; } }

    GetPresentBoxData getPresent;
    CreatePresentObj createPresent;
    DisplayPresentsObj displayPresent;

    int pageNum = 1;
    [SerializeField] int totalPresentNum = 0;

    bool isSet = false;
    bool displayLog = false;

    private void Awake()
    {
        unReceiptPresents = new PresentBoxModel[PresentBoxes.GetPresentBoxDataAll().Length];
        receiptedPresents = new PresentBoxModel[PresentBoxes.GetPresentBoxDataAll().Length];
        getPresent = GetComponent<GetPresentBoxData>();
        createPresent = GetComponent<CreatePresentObj>();
        displayPresent = GetComponent<DisplayPresentsObj>();

        getPresent.CheckUpdatePresentBox(); // �f�[�^�擾
    }

    void Start()
    {
        totalPresentNum = unReceiptPresents.Length / 5; // TODO:����ǉ�����Ƃ��ɂ��������ǂ���
        pageText.text = string.Format("{0}/{1}", pageNum, totalPresentNum);
    }

    void Update()
    {
        if (displayLog) { totalPresentNum = receiptedPresents.Length / 5; /* TODO:����ǉ�����Ƃ��ɂ��������ǂ���*/ }
        else { totalPresentNum = unReceiptPresents.Length / 5; /* TODO:����ǉ�����Ƃ��ɂ��������ǂ��� */}
        if (totalPresentNum == 0) { pageNum = 0; }
        pageText.text = string.Format("{0}/{1}", pageNum, totalPresentNum);
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
    public void SetPresentData(PresentBoxModel[] unReceiptPresentsData, PresentBoxModel[] receiptedPresentData)
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
    public void SetPresentClones(GameObject[] unreceipts, GameObject[] receipteds)
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
        if (pageNum < totalPresentNum)
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
