using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReceiveMission : MonoBehaviour
{

    [SerializeField] GameObject receivePanel;
    public GameObject ReceivePanel { get { return receivePanel; } }

    private void Awake()
    {
        receivePanel.SetActive(false);
    }

    /// <summary>
    /// ��揈���̌Ăяo��
    /// </summary>
    /// <param name="mission_id">��悷��~�b�V������ID</param>
    /// <param name="afterAction">�X�V������ɌĂяo�������֐�</param>
    public void StartReceiveMission(int mission_id, Action afterAction)
    {
        List<IMultipartFormSection> receiveMissionsForm = new();
        receiveMissionsForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        receiveMissionsForm.Add(new MultipartFormDataSection("mid", mission_id.ToString()));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.RECEIVE_MISSION_URL, receiveMissionsForm, afterAction));
    }
}
