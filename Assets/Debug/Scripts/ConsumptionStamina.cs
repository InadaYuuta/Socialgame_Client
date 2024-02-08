using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConsumptionStamina : MonoBehaviour
{
    int currentStamina;
    string user_id;
    string consumptionStr = "�X�^�~�i��5����܂����B";
    string cantConsumptionStr = "�X�^�~�i������܂���";

    void Start() => user_id = Users.Get().user_id;

    private void Update() => currentStamina = Users.Get().last_stamina;

    // �N�G�X�g���ł���܂ł̉������A�X�^�~�i��5�����
    public void TestConsumptionStamina()
    {
        // ���݂̃X�^�~�i��5�ȏ�Ȃ�X�^�~�i������A�����łȂ���΃X�^�~�i������Ȃ����Ƃ������C���[�W�\��
        if (currentStamina > 5)
        {
            List<IMultipartFormSection> consumptionStaminaForm = new();
            consumptionStaminaForm.Add(new MultipartFormDataSection("uid", user_id));
            StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.STAMINA_CONSUMPTION, consumptionStaminaForm, null));
            StartCoroutine(ResultPanelController.DisplayResultPanel(consumptionStr));
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel(cantConsumptionStr));
        }
    }
}
