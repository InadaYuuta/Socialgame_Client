using UnityEngine;

public class ReceivePresent : MonoBehaviour
{
    [SerializeField] GameObject receivePanel;

    int receive_present_id = -1;

    void Awake()
    {
        receivePanel.SetActive(false);
    }

    void Update()
    {

    }

    // 受け取るプレゼントIDを設定する
    public void SetReceivePresentId(int setId)
    {
        receive_present_id = setId;
    }

    // 受け取り確認画面を表示する
    public void DisplayCheckReceivePanel()
    {
        receivePanel.SetActive(true);
    }

    // 受け取りボタンが押された時
    public void OnpushReceiveButton()
    {
        Debug.Log("プレゼントNo" + receive_present_id + "　を受け取った");
    }
    // やめるボタンが押された時
    public void OnPushCancelButton()
    {
        receivePanel.SetActive(false);
    }
}
