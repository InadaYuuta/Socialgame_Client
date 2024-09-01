using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetPresentBoxData : MonoBehaviour
{
    List<PresentBoxModel> unReceiptPresents = new();   // ���O�̃v���[���g�f�[�^
    List<PresentBoxModel> receiptedPresents = new();   // ���ς̃v���[���g�f�[�^

    PresentBoxManager presentBoxManager;

    private void Awake()
    {
        presentBoxManager = GetComponent<PresentBoxManager>();
    }

    // ���������ꍇ�ɌĂԊ֐�
    void SuccessGetPresentBoxData()
    {
        ResultPanelController.HideCommunicationPanel();
        Debug.Log("�v���[���g�f�[�^�̎擾�ɐ������܂����B");
        GetPresentData();
    }

    // �v���[���g�{�b�N�X�f�[�^���擾����
    public void CheckUpdatePresentBox()
    {
        ResultPanelController.DisplayCommunicationPanel();
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
            if (CheckDuplication(data)) { continue; }
            string display = data.display;
            // �\���\���Ԃ��󂯎���Ă��Ȃ���Ύ��\�Ȕz��ɕۑ��A�����łȂ���Ύ��ς݂̔z��ɕۑ�
            if (presentBoxManager.CheckCanDisplay(display) && data.receipt != 1)
            {
                unReceiptPresents.Add(data);
            }
            else
            {
                receiptedPresents.Add(data);
            }
        }
        presentBoxManager.SetPresentData(unReceiptPresents, receiptedPresents);
    }

    /// <summary>
    /// �f�[�^��ID���d�����Ă��邩�ǂ������m�F����A�d�����Ă�����True��Ԃ�
    /// </summary>
    /// <param name="target">�m�F���郂�f��</param>
    /// <returns></returns>
    bool CheckDuplication(PresentBoxModel target)
    {
        bool result = false;

        bool isReceipt = target.receipt > 0 ? true : false;

        if (isReceipt)
        {
            foreach (var receiptedData in receiptedPresents)
            {
                if (receiptedData.present_id == target.present_id) { result = true; }
            }
        }
        else
        {
            foreach (var unReceiptData in unReceiptPresents)
            {
                if (unReceiptData.present_id == target.present_id) { result = true; }
            }
        }

        return result;
    }

}
