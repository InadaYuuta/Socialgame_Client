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

    public void StaminaRecovery()
    {
        if (currentStamina >= maxStamina)
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel(unRecoveryStr));
        }
        else
        {
            List<IMultipartFormSection> recoveryForm = new();
            recoveryForm.Add(new MultipartFormDataSection("uid", user_id));
            recoveryForm.Add(new MultipartFormDataSection("remode", recoveryMode));
            StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.STAMINA_RECOVERY_URL, recoveryForm, null));
            StartCoroutine(ResultPanelController.DisplayResultPanel(recoveryStr));
        }

    }
}
