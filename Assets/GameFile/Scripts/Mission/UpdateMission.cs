using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateMission : MonoBehaviour
{
    /// <summary>
    /// 更新処理の呼び出し
    /// </summary>
    /// <param name="mission_id">更新するミッションのID</param>
    /// <param name="prog">ミッションの進捗度</param>
    /// <param name="afterAction">更新処理後に呼び出したい関数</param>
    public void StartUpdateMission(int mission_id, int prog, Action afterAction)
    {
        List<IMultipartFormSection> updateMissionsForm = new();
        updateMissionsForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        updateMissionsForm.Add(new MultipartFormDataSection("mid", mission_id.ToString()));
        updateMissionsForm.Add(new MultipartFormDataSection("prog", prog.ToString()));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.UPDATE_MISSION_URL, updateMissionsForm, afterAction));
    }
}
