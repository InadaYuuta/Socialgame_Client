using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class GachaLogManager : WeaponBase
{
    [SerializeField] GameObject gachaLogPanel;
    [SerializeField] TextMeshProUGUI gachaLogText, gachaLogTitleText;
    [SerializeField] TextMeshProUGUI pageText;

    string[] weaponNames, gachaNames, createds;
    int[] weaponIds;
    string gachaLogTitleString = "�K�`������\n\n";
    string gachaLogString = " ";
    string user_id;

    int count = 0;     // �\�����镪�̃J�E���g
    int pageCount = 1; // �������ڂ̃y�[�W��
    int pageMax;

    GachaLogModel[] gachaLogModel;

    void Start()
    {
        GetGachaLog();
        gachaLogPanel.SetActive(false);
        user_id = Users.Get().user_id;
    }

    void SuccessGetLog()
    {
        ResultPanelController.HideCommunicationPanel();
        Invoke("UpdateText", 1.0f);
    }

    // �K�`�����O���擾
    public void GetGachaLog()
    {
        ResultPanelController.DisplayCommunicationPanel();
        List<IMultipartFormSection> gachaLogForm = new();
        gachaLogForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        Action afterAction = new(() => SuccessGetLog());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.GET_GACHA_LOG, gachaLogForm, afterAction));
    }

    // �߂�{�^���������ꂽ��
    public void PushBackButton()
    {
        pageCount = 1;
        gachaLogPanel.SetActive(false);
    }

    // ���̃y�[�W�ɍs���{�^���������ꂽ��
    public void PushNextButton()
    {
        gachaLogModel = GachaLogs.GetGacaLogDataAll();
        pageMax = gachaLogModel.Length / 10;

        if (pageCount < pageMax)
        {
            pageCount++;
            UpdateText();
        }
    }

    // �O�̃y�[�W�ɍs���{�^���������ꂽ��
    public void PushPreviousButton()
    {
        if (pageCount > 1)
        {
            pageCount--;
            UpdateText();
        }
    }

    void GetData()
    {
        gachaLogModel = GachaLogs.GetGacaLogDataAll();
        if (gachaLogModel.Length == 0)
        {
            gachaLogTitleString = "�K�`�������͂Ȃ�";
        }
        else
        {
            int displayMin = (pageCount - 1) * 10; // �\������Œ�l
            int displayMax = pageCount * 10;       // �\������ō��l
            pageMax = gachaLogModel.Length / 10;

            gachaLogString = "";
            gachaLogTitleString = "�K�`������";
            foreach (GachaLogModel gachaLogData in gachaLogModel)
            {
                weaponIds[count] = gachaLogData.weapon_id;
                gachaNames[count] = "�e�X�g�K�`��"; // TODO: �K�`���̎�ނ���������K�`���̃e�[�u���ǉ����Ă�������擾�ł���悤�ɂ���
                weaponNames[count] = WeaponMaster.GetWeaponMasterData(weaponIds[count]).weapon_name;
                createds[count] = gachaLogData.created;
                if (count > displayMin && count <= displayMax)
                {
                    int rarity = GetNthDigitNum(weaponIds[count], 7);
                    switch (rarity)
                    {
                        case 1:
                            gachaLogString = string.Format("{0}{1}.{2}-<color=\"blue\">{3}</color>-{4}\n", gachaLogString, count, gachaNames[count], weaponNames[count], createds[count]);
                            break;
                        case 2:
                            gachaLogString = string.Format("{0}{1}.{2}-<color=\"red\">{3}</color>-{4}\n", gachaLogString, count, gachaNames[count], weaponNames[count], createds[count]);
                            break;
                        case 3:
                            gachaLogString = string.Format("{0}{1}.{2}-<color=\"yellow\">{3}</color>-{4}\n", gachaLogString, count, gachaNames[count], weaponNames[count], createds[count]);
                            break;
                        default:
                            Debug.Log("�͈͊O�̃��A���e�B");
                            break;
                    }
                }
                count++;
            }
            count = 0;
        }
    }

    // �e�L�X�g���A�b�v�f�[�g�����胂�f�����擾�����肷��
    public void UpdateText()
    {
        gachaLogModel = GachaLogs.GetGacaLogDataAll();
        if (gachaLogModel != null)
        {
            weaponNames = new string[gachaLogModel.Length];
            gachaNames = new string[gachaLogModel.Length];
            createds = new string[gachaLogModel.Length];
            weaponIds = new int[gachaLogModel.Length];
        }
        GetData();
        gachaLogText.text = gachaLogString;
        gachaLogTitleText.text = gachaLogTitleString;
        pageText.text = string.Format("{0}/{1}�y�[�W", pageCount, pageMax);
    }
}
