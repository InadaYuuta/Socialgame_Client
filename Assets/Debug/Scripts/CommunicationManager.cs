using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ResponseObjects
{
    public UsersModel usersModel;
}

public class CommunicationManager : MonoBehaviour
{
   public static IEnumerator ConnectServer(string endpoint, string paramater,Action action = null)
    {
        // *** リクエストの送付 ***
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(GameUtil.Const.SERVER_URL + endpoint + paramater);
        yield return unityWebRequest.SendWebRequest();
        // エラーの場合
        if (!string.IsNullOrEmpty(unityWebRequest.error))
        {
            Debug.LogError(unityWebRequest.error);
            yield break;
        }

        // *** レスポンスの取得 ***
        string text = unityWebRequest.downloadHandler.text;
        Debug.Log("レスポンス : " + text);
        // エラーの場合
        if (text.All(char.IsNumber))
        {
            switch (text)
            {
                case GameUtil.Const.ERROR_DB_UPDATE:
                    Debug.LogError("サーバーでエラーが発生しました。[データベース更新エラー]");
                    break;
                default:
                    Debug.LogError("サーバーでエラーが発生しました。[システムエラー]");
                    break;
            }
            yield break;
        }

        // *** SQLiteへの保存処理 ***
        ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
        if (!string.IsNullOrEmpty(responseObjects.usersModel.user_id))
            Users.Set(responseObjects.usersModel);
        // 正常終了アクション実行
        if (action != null)
        {
            action();
            action = null;
        }
    }
}