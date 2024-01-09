using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;



public class CommunicationManager : MonoBehaviour
{
   public static IEnumerator ConnectServer(string endpoint, string paramater,Action action = null)
    {
        // *** ���N�G�X�g�̑��t ***
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(GameUtil.Const.RESISTRATION_URL + endpoint + paramater);
        yield return unityWebRequest.SendWebRequest();
        // �G���[�̏ꍇ
        if (!string.IsNullOrEmpty(unityWebRequest.error))
        {
            Debug.LogError(unityWebRequest.error);
            yield break;
        }

        // *** ���X�|���X�̎擾 ***
        string text = unityWebRequest.downloadHandler.text;
        Debug.Log("���X�|���X : " + text);
        // �G���[�̏ꍇ
        if (text.All(char.IsNumber))
        {
            switch (text)
            {
                case GameUtil.Const.ERROR_DB_UPDATE:
                    Debug.LogError("�T�[�o�[�ŃG���[���������܂����B[�f�[�^�x�[�X�X�V�G���[]");
                    break;
                default:
                    Debug.LogError("�T�[�o�[�ŃG���[���������܂����B[�V�X�e���G���[]");
                    break;
            }
            yield break;
        }

        // *** SQLite�ւ̕ۑ����� ***
        ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
        if (!string.IsNullOrEmpty(responseObjects.usersModel.user_id))
            Users.Set(responseObjects.usersModel);
        // ����I���A�N�V�������s
        if (action != null)
        {
            action();
            action = null;
        }
    }
}