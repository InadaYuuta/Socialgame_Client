using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReceivePresent : MonoBehaviour
{
    [SerializeField] GameObject receivePanel, rewardPanel, receivePresent;
    [SerializeField] Image beforeItemImage, afterItemImage;
    [SerializeField] TextMeshProUGUI beforeItemNumText, afterItemNumText;

    int receive_present_id = -1;�@// �󂯎��v���[���g��ID
    int beforeNum, afterNum, rewardCategory;

    GetPresentBoxData getPresentBoxData;
    CreatePresentObj createPresentObj;

    void Awake()
    {
        receivePanel.SetActive(false);
        rewardPanel.SetActive(false);
        getPresentBoxData = GetComponent<GetPresentBoxData>();
        createPresentObj = GetComponent<CreatePresentObj>();
    }

    // �X�V�O�ƍX�V��̃A�C�e���̐����o��
    void GetTargetItemNum(int rewardNum)
    {
        int target = RewardCategories.GetRewardCategoryData(rewardCategory).reward_category;

        switch (target)
        {
            case 1: // �ʉ�
                beforeNum = Wallets.Get().free_amount + Wallets.Get().paid_amount;
                break;
            case 2: // �X�^�~�i�񕜃A�C�e��
                beforeNum = Items.GetItemData(10001).item_num;
                break;
            case 3: // �����|�C���g
                beforeNum = Items.GetItemData(20001).item_num;
                break;
            case 4: // �����A�C�e��
                beforeNum = Items.GetItemData(30001).item_num;
                break;
            case 5: // �ʃA�C�e��
                break;
            case 6: // ����
                break;
            default:
                break;
        }
        afterNum = beforeNum + rewardNum;
        UpdateRewardPanel();
    }

    void UpdateRewardPanel()
    {
        beforeItemNumText.text = beforeNum.ToString();
        afterItemNumText.text = afterNum.ToString();

        beforeItemImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
        afterItemImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
    }

    // �󂯎��v���[���gID��ݒ肷��
    public void SetReceivePresentId(GameObject target, int setId, int category, int rewardNum)
    {
        receivePresent = target;
        receive_present_id = setId;
        rewardCategory = category;
        GetTargetItemNum(rewardNum);
    }



    // �󂯎��m�F��ʂ�\������
    public void DisplayCheckReceivePanel() => receivePanel.SetActive(true);

    // ���������ꍇ�ɌĂԊ֐�
    void SuccessReceivePresent()
    {
        Debug.Log("�v���[���g�󂯎��");
        rewardPanel.SetActive(true);
        getPresentBoxData.CheckUpdatePresentBox(); // �v���[���g�{�b�N�X�̏�Ԃ��X�V
        GameObject[] test = new GameObject[1];
        test[0] = receivePresent;
        createPresentObj.ReceiptedPresent(test);
    }

    // �󂯎��{�^���������ꂽ��
    public void OnpushReceiveButton()
    {
        List<IMultipartFormSection> receiveForm = new();
        receiveForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        receiveForm.Add(new MultipartFormDataSection("pid", receive_present_id.ToString()));
        Action afterAction = new(() => SuccessReceivePresent());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.RECEIVE_PRESENT_BOX_URL, receiveForm, afterAction));
    }

    // ��߂�{�^���������ꂽ��
    public void OnPushCancelButton() => receivePanel.SetActive(false);

    // ��V�l����ʂ̖߂�{�^���������ꂽ��
    public void OnPushRewardBackButton()
    {
        rewardPanel.SetActive(false);
        receivePanel.SetActive(false);
    }
}
