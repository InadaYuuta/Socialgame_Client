using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ResponseObjects
{
    public UsersModel users;
    public WalletsModel wallets;
    public ItemsModel[] items;
    public WeaponModel[] weapons;
    // マスターデータ
    public int master_data_version;
    public ItemMasterModel[] item_master;
    public ItemCategoryModel[] item_category;
    public PaymentShopModel[] payment_shop;
    public ExchangeItemCategoryModel[] exchange_item_category;
    public ExchangeShopModel[] exchange_item_shop;
    public RewardCategoryModel[] reward_category;

    // 武器データ
    public WeaponMasterModel[] weapon_master;
    public WeaponCategoryModel[] weapon_category;
    public WeaponRarityModel[] weapon_rarity;
    public WeaponExpModel[] weapon_exp;
    public EvolutionWeaponModel[] evolution_weapon;
    public GachaWeaponModel[] gacha_weapon;
    public Weapons evolutionWeapons; // 進化時にデータを追加する用

    // プレゼントボックスデータ
    public PresentBoxModel[] present_box;

    // ミッション
    public MissionsModel[] missions;
    public MissionMasterModel[] mission_master;
    public MissionCategoryModel[] mission_category;

    // ガチャ用
    public int[] new_weaponIds;
    public string gacha_result;
    public int fragment_num;

    // お知らせ
    public NewsMasterModel[] news;

    // ログデータ
    public GachaLogModel[] gacha_log;

    // エラーコード
    public string errcode;
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
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// ユーザー情報更新
    /// </summary>
    /// <param name="responseObjects"></param>
    static void UpdateUserData(ResponseObjects responseObjects)
    {
        if (responseObjects.users != null && !string.IsNullOrEmpty(responseObjects.users.user_id))
        {
            Users.Set(responseObjects.users);
        }
    }

    /// <summary>
    /// ウォレット情報更新
    /// </summary>
    /// <param name="responseObjects"></param>
    static void UpdateWalletData(ResponseObjects responseObjects)
    {
        if (responseObjects.wallets != null)
        {
            UsersModel usersModel = Users.Get();
            if (responseObjects.wallets.paid_amount + responseObjects.wallets.free_amount > 0)
            {
                Wallets.Set(responseObjects.wallets, usersModel.user_id);
            }
        }
    }

    /// <summary>
    /// アイテム情報更新
    /// </summary>
    /// <param name="responseObjects"></param>
    static void UpdateItemData(ResponseObjects responseObjects)
    {
        if (responseObjects.items != null)
        {
            Items.Set(responseObjects.items);
        }
    }

    /// <summary>
    /// 武器情報更新
    /// </summary>
    /// <param name="responseObjects"></param>
    static void UpdateWeaponData(ResponseObjects responseObjects)
    {
        if (responseObjects.weapons != null)
        {
            UsersModel usersModel = Users.Get();
            Weapons.Set(responseObjects.weapons, usersModel.user_id);
        }
    }

    /// <summary>
    ///  ガチャログの情報を更新
    /// </summary>
    /// <param name="responseObjects"></param>
    static void UpdateGachaLogData(ResponseObjects responseObjects)
    {
        if (responseObjects.gacha_log != null)
        {
            GachaLogs.Set(responseObjects.gacha_log);
        }
    }

    ///<summary>
    /// プレゼントボックスの情報更新
    /// </summary>
    static void UpdatePresentBoxData(ResponseObjects responseObjects)
    {
        if (responseObjects.present_box != null)
        {
            UsersModel usersModel = Users.Get();
            PresentBoxes.Set(responseObjects.present_box, usersModel.user_id);
        }
    }

    ///<summary>
    /// ミッションの情報更新
    /// </summary>
    static void UpdateMissionData(ResponseObjects responseObjects)
    {
        if (responseObjects.missions != null)
        {
            UsersModel usersModel = Users.Get();
            // TODO: 最初の一回のみSetするように処理を変更
            Missions.Set(responseObjects.missions, usersModel.user_id);
        }
    }

    /// <summary>
    /// お知らせ更新
    /// </summary>
    /// <param name="responseObjects"></param>
    static void UpdateNewsData(ResponseObjects responseObjects)
    {
        if (responseObjects.news != null)
        {
            NewsMaster.Set(responseObjects.news);
        }
    }

    /// <summary>
    /// マスターデータの更新
    /// </summary>
    /// <param name="requireComponent"></param>
    static void UpdateMasterData(ResponseObjects responseObjects)
    {
        // バージョン保存
        if (responseObjects.master_data_version != null && responseObjects.master_data_version != 0)
        {
            SaveManager.Instance.SetMasterDataVersion(responseObjects.master_data_version);
        }
        // カテゴリー保存
        if (responseObjects.item_category != null)
        {
            ItemCategories.Set(responseObjects.item_category);
        }
        // 交換ショップのカテゴリー保存
        if (responseObjects.exchange_item_category != null)
        {
            ExchangeShopCategories.Set(responseObjects.exchange_item_category);
        }
        // アイテムマスタのデータ保存
        if (responseObjects.item_master != null)
        {
            ItemsMaster.Set(responseObjects.item_master);
        }
        // 通貨ショップ情報保存
        if (responseObjects.payment_shop != null)
        {
            PaymentShops.Set(responseObjects.payment_shop);
        }
        // 交換ショップ情報保存
        if (responseObjects.exchange_item_shop != null)
        {
            ExchangeShops.Set(responseObjects.exchange_item_shop);
        }
        // 武器マスタデータ情報保存
        if (responseObjects.weapon_master != null)
        {
            WeaponMaster.Set(responseObjects.weapon_master);
        }
        // 武器カテゴリー保存
        if (responseObjects.weapon_category != null)
        {
            WeaponCategories.Set(responseObjects.weapon_category);
        }
        // 武器レアリティ保存
        if (responseObjects.weapon_rarity != null)
        {
            WeaponRarities.Set(responseObjects.weapon_rarity);
        }
        // ガチャ武器データ保存
        if (responseObjects.gacha_weapon != null)
        {
            GachaWeapons.Set(responseObjects.gacha_weapon);
        }
        // 武器必要経験値データ保存
        if (responseObjects.weapon_exp != null)
        {
            UsersModel usersModel = Users.Get();
            WeaponExps.Set(responseObjects.weapon_exp, usersModel.user_id);
        }
        // 進化後武器データ保存
        if (responseObjects.evolution_weapon != null)
        {
            EvolutionWeapons.Set(responseObjects.evolution_weapon);
        }
        // ミッションマスタデータ保存
        if (responseObjects.mission_master != null)
        {
            MissionMaster.Set(responseObjects.mission_master);
        }
        // ミッションカテゴリーデータ保存
        if (responseObjects.mission_category != null)
        {
            MissionCategories.Set(responseObjects.mission_category);
        }
        // 報酬カテゴリーデータ保存
        if (responseObjects.reward_category != null)
        {
            RewardCategories.Set(responseObjects.reward_category);
        }
    }

    /// <summary>
    /// リンクごとに情報の更新、保存を行う
    /// </summary>
    /// <param name="connectURL"></param>
    /// <param name="responseObjects"></param>
    static void ConnectMove(string connectURL, ResponseObjects responseObjects)
    {
        switch (connectURL)
        {
            case GameUtil.Const.LOGIN_URL:
            case GameUtil.Const.STAMINA_CONSUMPTION:
                UpdateUserData(responseObjects);
                break;
            case GameUtil.Const.HOME_URL:
                UpdateUserData(responseObjects);
                UpdateWalletData(responseObjects);
                UpdateWeaponData(responseObjects);
                UpdateItemData(responseObjects);
                UpdateMissionData(responseObjects);
                UpdatePresentBoxData(responseObjects);
                break;
            case GameUtil.Const.BUY_CURRENCY_URL:
                UpdateWalletData(responseObjects);
                break;
            case GameUtil.Const.ITEM_REGISTRATION_URL:
                UpdateItemData(responseObjects);
                break;
            case GameUtil.Const.REGISTRATION_URL:
                UpdateUserData(responseObjects);
                UpdateWalletData(responseObjects);
                break;
            case GameUtil.Const.STAMINA_RECOVERY_URL:
            case GameUtil.Const.BUY_EXCHANGE_SHOP_URL:
                UpdateUserData(responseObjects);
                UpdateWalletData(responseObjects);
                UpdateItemData(responseObjects);
                break;
            case GameUtil.Const.MASTER_GET_URL:
                UpdateMasterData(responseObjects);
                break;
            case GameUtil.Const.GACHA_URL:
                break;
            case GameUtil.Const.GET_GACHA_LOG:
                UpdateGachaLogData(responseObjects);
                break;
            case GameUtil.Const.WEAPON_LEVEL_UP_URL:
            case GameUtil.Const.WEAPON_EVOLUTION_URL:
                UpdateUserData(responseObjects);
                UpdateWeaponData(responseObjects);
                break;
            case GameUtil.Const.WEAPON_LIMIT_BREAK_URL:
                UpdateUserData(responseObjects);
                UpdateWeaponData(responseObjects);
                UpdateItemData(responseObjects);
                break;
            case GameUtil.Const.GET_PRESENT_BOX_URL:
                UpdatePresentBoxData(responseObjects);
                break;
            case GameUtil.Const.RECEIVE_PRESENT_BOX_URL:
                UpdateUserData(responseObjects);
                UpdateWalletData(responseObjects);
                UpdateItemData(responseObjects);
                UpdatePresentBoxData(responseObjects);
                break;
            case GameUtil.Const.GET_MISSION_URL:
            case GameUtil.Const.UPDATE_MISSION_URL:
                UpdateMissionData(responseObjects);
                break;
            case GameUtil.Const.RECEIVE_MISSION_URL:
                UpdateMissionData(responseObjects);
                UpdateUserData(responseObjects);
                UpdateWalletData(responseObjects);
                UpdateItemData(responseObjects);
                UpdatePresentBoxData(responseObjects);
                break;
            case GameUtil.Const.GET_NEWS_URL:
                UpdateNewsData(responseObjects);
                break;
            default:
                break;
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
                    // パラメータの内容をログに出力
                    foreach (var param in parameter)
                    {
                        var sectionName = param.sectionName;
                        var sectionData = param.sectionData;
                        Debug.Log($"Parameter Name: {sectionName}, Parameter Value: {System.Text.Encoding.UTF8.GetString(sectionData)}");
                    }
                    DisplayErrorTextManager.Instance.DisplayError("500\n通信エラーが発生\nしばらくしてからお試しください");
                    Debug.LogError("URL:" + connectURL + " のURLに接続した際にエラーが発生 " + webRequest.error);
                    yield break;
                }
            }
            else
            {
                // *** レスポンスの取得 ***
                string text = webRequest.downloadHandler.text;
                Debug.Log("URL:" + connectURL + "レスポンス : " + text);
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
                ConnectMove(connectURL, responseObjects);
                // エラーコードがあったらエラーを表示
                if (responseObjects.errcode != null)
                {
                    DisplayErrorTextManager.Instance.DisplayError(responseObjects.errcode);
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
