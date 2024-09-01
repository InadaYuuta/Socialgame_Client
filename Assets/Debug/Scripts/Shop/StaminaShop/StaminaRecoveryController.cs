using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StaminaRecoveryController : MonoBehaviour
{
    int currentStamina;
    int maxStamina;

    [SerializeField] string user_id;
    [SerializeField] bool modeFlag;
    [SerializeField] string recoveryMode;

    string recoveryStr = "�X�^�~�i���񕜂���\r\n�ʉ݂�5�����";
    string unRecoveryStr = "����ȏ�X�^�~�i���񕜂ł��Ȃ�";

    void Start()
    {
        currentStamina = Users.Get().last_stamina;
        maxStamina = Users.Get().max_stamina;
        if (modeFlag)
        {
            recoveryMode = "currency";
        }
        else { recoveryMode = "item"; }
        user_id = Users.Get().user_id;
    }

    void Update()
    {
        currentStamina = Users.Get().last_stamina;
        maxStamina = Users.Get().max_stamina;
    }

    void StaminaRecovery()
    {
        if (currentStamina >= maxStamina)
        {
            unRecoveryStr = "����ȏ�X�^�~�i���񕜂ł��Ȃ�";
            StartCoroutine(ResultPanelController.DisplayResultPanel(unRecoveryStr));
        }
        else
        {
            ResultPanelController.DisplayCommunicationPanel();
            List<IMultipartFormSection> recoveryForm = new();
            recoveryForm.Add(new MultipartFormDataSection("uid", user_id));
            recoveryForm.Add(new MultipartFormDataSection("remode", recoveryMode));
            Action afterAction = new(() => ResultPanelController.HideCommunicationPanel());
            StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.STAMINA_RECOVERY_URL, recoveryForm, afterAction));
            StartCoroutine(ResultPanelController.DisplayResultPanel(recoveryStr));
        }

    }

    // �ʉ݂ł̃X�^�~�i��
    public void StaminaRecoveryCurrency()
    {
        int wallet = Wallets.Get().free_amount + Wallets.Get().paid_amount;
        if (wallet > 5)
        {
            recoveryStr = "�X�^�~�i���񕜂���\r\n�ʉ݂�5�����";
            StaminaRecovery();
        }
        else
        {
            unRecoveryStr = "�ʉ݂�����Ȃ�";
            StartCoroutine(ResultPanelController.DisplayResultPanel(unRecoveryStr));
        }
    }

    // �A�C�e���ł̃X�^�~�i��
    public void StaminaRecoveryItem()
    {
        int staminaItem = Items.GetItemData(10001).item_num;
        if (staminaItem > 0)
        {
            recoveryStr = "�X�^�~�i���񕜂���\r\n�X�^�~�i�A�C�e�����P�����";
            StaminaRecovery();
        }
        else
        {
            unRecoveryStr = "�X�^�~�i�񕜃A�C�e��������Ȃ�";
            StartCoroutine(ResultPanelController.DisplayResultPanel(unRecoveryStr));
        }

    }
}
