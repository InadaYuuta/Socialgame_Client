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
    // �}�X�^�[�f�[�^
    public int master_data_version;
    public ItemMasterModel[] item_master;
    public ItemCategoryModel[] item_category;
    public ExchangeItemCategoryModel[] exchange_item_category;
    public PaymentShopModel[] payment_shop;
    public ExchangeShopModel[] exchange_item_shop;
    // ����f�[�^
    public WeaponsMasterModel[] weapon_master;
    public WeaponCategoryModel[] weapon_category;
    public WeaponRarityModel[] weapon_rarity;
    public GachaWeaponModel[] gacha_weapon;
}

public class CommunicationManager : MonoBehaviour
{
    public static CommunicationManager Instance { get; private set; }

    private void Awake()
    {
        // �V���O���g���ɂ���
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

    /// <summary>
    /// ���[�U�[���X�V
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
    /// �E�H���b�g���X�V
    /// </summary>
    /// <param name="responseObjects"></param>
    static void UpdateWalletData(ResponseObjects responseObjects)
    {
        if (responseObjects.wallets != null)
        {
            UsersModel usersModel = Users.Get();
            Wallets.Set(responseObjects.wallets, usersModel.user_id);
        }
    }

    /// <summary>
    /// �A�C�e�����X�V
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
    /// �}�X�^�[�f�[�^�̍X�V
    /// </summary>
    /// <param name="requireComponent"></param>
    static void UpdateMasterData(ResponseObjects responseObjects)
    {
        // �o�[�W�����ۑ�
        if (responseObjects.master_data_version != null)
        {
            SaveManager.Instance.SetMasterDataVersion(responseObjects.master_data_version);
        }
        // �J�e�S���[�ۑ�
        if (responseObjects.item_category != null)
        {
            ItemCategories.Set(responseObjects.item_category);
        }
        // �����V���b�v�̃J�e�S���[�ۑ�
        if (responseObjects.exchange_item_category != null)
        {
            ExchangeShopCategories.Set(responseObjects.exchange_item_category);
        }
        // �A�C�e���}�X�^�̃f�[�^�ۑ�
        if (responseObjects.item_master != null)
        {
            ItemsMaster.Set(responseObjects.item_master);
        }
        // �ʉ݃V���b�v���ۑ�
        if (responseObjects.payment_shop != null)
        {
            PaymentShops.Set(responseObjects.payment_shop);
        }
        // �����V���b�v���ۑ�
        if (responseObjects.exchange_item_shop != null)
        {
            ExchangeShops.Set(responseObjects.exchange_item_shop);
        }
        // ����}�X�^�f�[�^���ۑ�
        if (responseObjects.weapon_master != null)
        {
            WeaponsMaster.Set(responseObjects.weapon_master);
        }
        // ����J�e�S���[�ۑ�
        if (responseObjects.weapon_category != null)
        {
            WeaponCategories.Set(responseObjects.weapon_category);
        }
        // ���탌�A���e�B�ۑ�
        if (responseObjects.weapon_rarity != null)
        {
            WeaponRarities.Set(responseObjects.weapon_rarity);
        }
        // �K�`������f�[�^�ۑ�
        if (responseObjects.gacha_weapon != null)
        {
            GachaWeapons.Set(responseObjects.gacha_weapon);
        }
    }

    /// <summary>
    /// �����N���Ƃɏ��̍X�V�A�ۑ����s��
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
            case GameUtil.Const.BUY_CURRENCY_URL:
                UpdateWalletData(responseObjects);
                break;
            case GameUtil.Const.ITEM_REGISTRATION_URL:
            case GameUtil.Const.ITEM_UPDATE_URL:
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
            default:
                break;
        }
    }

    public static IEnumerator ConnectServer(string connectURL, List<IMultipartFormSection> parameter, Action action = null)
    {
        // *** ���N�G�X�g�̑��t ***
        using (UnityWebRequest webRequest = UnityWebRequest.Post(connectURL, parameter))
        {
            webRequest.timeout = 10;
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // �G���[�̏ꍇ
                if (!string.IsNullOrEmpty(webRequest.error))
                {
                    Debug.LogError(webRequest.error);
                    yield break;
                }
            }
            else
            {
                // *** ���X�|���X�̎擾 ***
                string text = webRequest.downloadHandler.text;
                Debug.Log("���X�|���X : " + text);
                // �G���[�̏ꍇ
                if (text.All(char.IsNumber))
                {
                    switch (text)
                    {
                        case GameUtil.Const.ERROR_MASTER_DATA_UPDATE:
                            Debug.LogError("�}�X�^�̏�Ԃ��Â��悤�ł��B[�}�X�^�o�[�W�����s����]");
                            // �����ɃA�b�v�f�[�g���ł������Ƃ�\������C���[�W��\�����鏈����ǋL
                            break;
                        case GameUtil.Const.ERROR_DB_UPDATE:
                            Debug.LogError("�T�[�o�[�ŃG���[���������܂����B[�f�[�^�x�[�X�X�V�G���[]");
                            break;
                        default:
                            string debugTex = string.Format("�e�L�X�g�̓��e:{0}", text);
                            Debug.LogError(debugTex);
                            // Debug.LogError("�T�[�o�[�ŃG���[���������܂����B[�V�X�e���G���[]");
                            break;
                    }
                    yield break;
                }

                // *** SQLite�ւ̕ۑ����� ***
                ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
                ConnectMove(connectURL, responseObjects);

                // ����I���A�N�V�������s
                if (action != null)
                {
                    action();
                    action = null;
                }
            }
        }
    }
}