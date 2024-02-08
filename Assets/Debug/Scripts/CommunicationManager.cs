using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ResponseObjects
{
    public int master_data_version;
    public UsersModel users;
    public WalletsModel wallets;
    public ItemsModel items;
    public ItemCategoryModel[] item_category;
    public PaymentShopModel[] payment_shop;
}

public class CommunicationManager : MonoBehaviour
{
    public static CommunicationManager Instance { get; private set; }

    private void Awake()
    {
        // シングルトンにする
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public static IEnumerator ConnectServer(string connectURL, List<IMultipartFormSection> parameter, Action action = null)
    {
        // *** リクエストの送付 ***
        using (UnityWebRequest webRequest = UnityWebRequest.Post(connectURL, parameter))
        {
            webRequest.timeout = 10;
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // エラーの場合
                if (!string.IsNullOrEmpty(webRequest.error))
                {
                    Debug.LogError(webRequest.error);
                    yield break;
                }
            }
            else
            {
                // *** レスポンスの取得 ***
                string text = webRequest.downloadHandler.text;
                Debug.Log("レスポンス : " + text);
                // エラーの場合
                if (text.All(char.IsNumber))
                {
                    switch (text)
                    {
                        case GameUtil.Const.ERROR_MASTER_DATA_UPDATE:
                            Debug.LogError("マスタの状態が古いようです。[マスタバージョン不整合]");
                            // ここにアップデートができたことを表示するイメージを表示する処理を追記
                            break;
                        case GameUtil.Const.ERROR_DB_UPDATE:
                            Debug.LogError("サーバーでエラーが発生しました。[データベース更新エラー]");
                            break;
                        default:
                            string debugTex = string.Format("テキストの内容:{0}", text);
                            Debug.LogError(debugTex);
                            // Debug.LogError("サーバーでエラーが発生しました。[システムエラー]");
                            break;
                    }
                    yield break;
                }

                // *** SQLiteへの保存処理 ***
                ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);

                switch (connectURL)
                {
                    case GameUtil.Const.RESISTRATION_URL:
                    case GameUtil.Const.STAMINA_RECOVERY_URL:
                        // ユーザー情報保存
                        if (responseObjects.users != null && !string.IsNullOrEmpty(responseObjects.users.user_id))
                        {
                            Users.Set(responseObjects.users);
                        }
                        // 通貨情報保存
                        if (responseObjects.wallets != null)
                        {
                            UsersModel usersModel = Users.Get();
                            Wallets.Set(responseObjects.wallets, usersModel.user_id);
                        }
                        break;
                    case GameUtil.Const.LOGIN_URL:
                    case GameUtil.Const.STAMINA_CONSUMPTION:
                        // ユーザー情報保存
                        if (responseObjects.users != null && !string.IsNullOrEmpty(responseObjects.users.user_id))
                        {
                            Users.Set(responseObjects.users);
                        }
                        break;
                    case GameUtil.Const.BUY_CURRENCY_URL:
                        // 通貨情報保存
                        if (responseObjects.wallets != null)
                        {
                            UsersModel usersModel = Users.Get();
                            Wallets.Set(responseObjects.wallets, usersModel.user_id);
                        }
                        break;
                    // TODO:アイテムのテーブルができたら、スタミナ回復用のケースを用意する
                    default:
                        // バージョン保存
                        if (responseObjects.master_data_version != null)
                        {
                            SaveManager.Instance.SetMasterDataVersion(responseObjects.master_data_version);
                        }
                        // カテゴリー保存
                        if (responseObjects.item_category != null)
                        {
                            ItemCategories.Set(responseObjects.item_category);
                        }
                        // 通貨ショップ情報保存
                        if (responseObjects.payment_shop != null)
                        {
                            PaymentShops.Set(responseObjects.payment_shop);
                        }
                        break;
                }

                // 正常終了アクション実行
                if (action != null)
                {
                    action();
                    action = null;
                }
            }
        }
    }
}
