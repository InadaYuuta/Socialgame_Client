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

    // �󂯎��v���[���gID��ݒ肷��
    public void SetReceivePresentId(int setId)
    {
        receive_present_id = setId;
    }

    // �󂯎��m�F��ʂ�\������
    public void DisplayCheckReceivePanel()
    {
        receivePanel.SetActive(true);
    }

    // �󂯎��{�^���������ꂽ��
    public void OnpushReceiveButton()
    {
        Debug.Log("�v���[���gNo" + receive_present_id + "�@���󂯎����");
    }
    // ��߂�{�^���������ꂽ��
    public void OnPushCancelButton()
    {
        receivePanel.SetActive(false);
    }
}
