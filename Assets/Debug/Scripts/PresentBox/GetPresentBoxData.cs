using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetPresentBoxData : MonoBehaviour
{
    [SerializeField] PresentBoxModel[] unReceiptPresents;   // ���O�̃v���[���g�f�[�^
    [SerializeField] PresentBoxModel[] receiptedPresents;   // ���ς̃v���[���g�f�[�^

    PresentBoxManager presentBoxManager;

    private void Awake()
    {
        presentBoxManager = GetComponent<PresentBoxManager>();
        unReceiptPresents = new PresentBoxModel[0];
        receiptedPresents = new PresentBoxModel[0];
    }

    void Start()
    {
    }

    void Update()
    {

    }

    // ���������ꍇ�ɌĂԊ֐�
    void SuccessGetPresentBoxData()
    {
        Debug.Log("�v���[���g�f�[�^�̎擾�ɐ������܂����B");
        GetPresentData();
    }

    // �v���[���g�{�b�N�X�f�[�^���擾����
    public void CheckUpdatePresentBox()
    {
        List<IMultipartFormSection> getPresentBoxForm = new();
        getPresentBoxForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        Action afterAction = new(() => SuccessGetPresentBoxData());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.GET_PRESENT_BOX_URL, getPresentBoxForm, afterAction));
    }

    // �v���[���g�̃f�[�^�����ςƎ��O�ɕ����Ď擾
    void GetPresentData()
    {
        PresentBoxModel[] allData = PresentBoxes.GetPresentBoxDataAll();

        foreach (var data in allData)
        {
            string display = data.display;
            // �\���\���Ԃ��󂯎���Ă��Ȃ���Ύ��\�Ȕz��ɕۑ��A�����łȂ���Ύ��ς݂̔z��ɕۑ�
            if (presentBoxManager.CheckCanDisplay(display) && data.receipt != 1)
            {
                Array.Resize(ref unReceiptPresents, unReceiptPresents.Length + 1);
                unReceiptPresents[unReceiptPresents.Length - 1] = data;
            }
            else
            {
                Array.Resize(ref receiptedPresents, receiptedPresents.Length + 1);
                receiptedPresents[receiptedPresents.Length - 1] = data;
            }
        }
        presentBoxManager.SetPresentData(unReceiptPresents, receiptedPresents);
    }


}
