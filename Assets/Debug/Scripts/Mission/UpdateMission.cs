using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateMission : MonoBehaviour
{
    /// <summary>
    /// �X�V�����̌Ăяo��
    /// </summary>
    /// <param name="mission_id">�X�V����~�b�V������ID</param>
    /// <param name="prog">�~�b�V�����̐i���x</param>
    /// <param name="afterAction">�X�V������ɌĂяo�������֐�</param>
    public void StartUpdateMission(int mission_id, int prog, Action afterAction)
    {
        List<IMultipartFormSection> updateMissionsForm = new();
        updateMissionsForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        updateMissionsForm.Add(new MultipartFormDataSection("mid", mission_id.ToString()));
        updateMissionsForm.Add(new MultipartFormDataSection("prog", prog.ToString()));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.UPDATE_MISSION_URL, updateMissionsForm, afterAction));
    }
}
