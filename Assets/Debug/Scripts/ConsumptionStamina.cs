using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConsumptionStamina : MonoBehaviour
{
    int currentStamina;
    string user_id;
    string consumptionStr = "スタミナを5消費しました。";
    string cantConsumptionStr = "スタミナがたりません";

    void Start() => user_id = Users.Get().user_id;

    private void Update() => currentStamina = Users.Get().last_stamina;

    // クエストができるまでの仮処理、スタミナを5消費する
    public void TestConsumptionStamina()
    {
        // 現在のスタミナが5以上ならスタミナを消費、そうでなければスタミナが足りないことを示すイメージ表示
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
