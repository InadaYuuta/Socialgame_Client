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

    string recoveryStr = "スタミナを回復した\r\n通貨を5消費した";
    string unRecoveryStr = "これ以上スタミナを回復できない";

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
            unRecoveryStr = "これ以上スタミナを回復できない";
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

    // 通貨でのスタミナ回復
    public void StaminaRecoveryCurrency()
    {
        int wallet = Wallets.Get().free_amount + Wallets.Get().paid_amount;
        if (wallet > 5)
        {
            recoveryStr = "スタミナを回復した\r\n通貨を5消費した";
            StaminaRecovery();
        }
        else
        {
            unRecoveryStr = "通貨が足りない";
            StartCoroutine(ResultPanelController.DisplayResultPanel(unRecoveryStr));
        }
    }

    // アイテムでのスタミナ回復
    public void StaminaRecoveryItem()
    {
        int staminaItem = Items.GetItemData(10001).item_num;
        if (staminaItem > 0)
        {
            recoveryStr = "スタミナを回復した\r\nスタミナアイテムを１消費した";
            StaminaRecovery();
        }
        else
        {
            unRecoveryStr = "スタミナ回復アイテムが足りない";
            StartCoroutine(ResultPanelController.DisplayResultPanel(unRecoveryStr));
        }

    }
}
