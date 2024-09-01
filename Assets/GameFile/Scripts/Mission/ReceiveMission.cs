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
    /// 受取処理の呼び出し
    /// </summary>
    /// <param name="mission_id">受取するミッションのID</param>
    /// <param name="afterAction">更新処理後に呼び出したい関数</param>
    public void StartReceiveMission(int mission_id, Action afterAction)
    {
        List<IMultipartFormSection> receiveMissionsForm = new();
        receiveMissionsForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        receiveMissionsForm.Add(new MultipartFormDataSection("mid", mission_id.ToString()));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.RECEIVE_MISSION_URL, receiveMissionsForm, afterAction));
    }
}
