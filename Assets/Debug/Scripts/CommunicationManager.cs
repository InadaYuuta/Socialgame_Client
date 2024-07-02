using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ResponseObjects
{
    public UsersModel users;
    public WalletsModel wallets;
    public ItemsModel[] items;
    public WeaponModel[] weapons;
    // �}�X�^�[�f�[�^
    public int master_data_version;
    public ItemMasterModel[] item_master;
    public ItemCategoryModel[] item_category;
    public PaymentShopModel[] payment_shop;
    public ExchangeItemCategoryModel[] exchange_item_category;
    public ExchangeShopModel[] exchange_item_shop;
    public RewardCategoryModel[] reward_category;

    // ����f�[�^
    public WeaponMasterModel[] weapon_master;
    public WeaponCategoryModel[] weapon_category;
    public WeaponRarityModel[] weapon_rarity;
    public WeaponExpModel[] weapon_exp;
    public EvolutionWeaponModel[] evolution_weapon;
    public GachaWeaponModel[] gacha_weapon;

    // �v���[���g�{�b�N�X�f�[�^
    public PresentBoxModel[] present_box;

    // �~�b�V����
    public MissionMasterModel[] mission;
    public MissionCategoryModel[] mission_category;

    // �K�`���p
    //public string[] new_weaponIds;
    //public string[] gacha_result;
    public int[] new_weaponIds;
    // public int[] gacha_result;
    public string gacha_result;
    public int fragment_num;

    // ���O�f�[�^
    public GachaLogModel[] gacha_log;

    // �G���[�R�[�h
    public string errcode;
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
            Destroy(gameObject);
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
    /// ������X�V
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
    ///  �K�`�����O�̏����X�V
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
    /// �v���[���g�{�b�N�X�̏��X�V
    /// </summary>
    static void UpdatePresentBoxData(ResponseObjects responseObjects)
    {
        if (responseObjects.present_box != null)
        {
            UsersModel usersModel = Users.Get();
            PresentBoxes.Set(responseObjects.present_box, usersModel.user_id);
        }
    }

    /// <summary>
    /// �}�X�^�[�f�[�^�̍X�V
    /// </summary>
    /// <param name="requireComponent"></param>
    static void UpdateMasterData(ResponseObjects responseObjects)
    {
        // �o�[�W�����ۑ�
        if (responseObjects.master_data_version != null && responseObjects.master_data_version != 0)
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
            WeaponMaster.Set(responseObjects.weapon_master);
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
        // ����K�v�o���l�f�[�^�ۑ�
        if (responseObjects.weapon_exp != null)
        {
            UsersModel usersModel = Users.Get();
            WeaponExps.Set(responseObjects.weapon_exp, usersModel.user_id);
        }
        // �i���㕐��f�[�^�ۑ�
        if (responseObjects.evolution_weapon != null)
        {
            EvolutionWeapons.Set(responseObjects.evolution_weapon);
        }
        // �~�b�V�����f�[�^�ۑ�
        if (responseObjects.mission != null)
        {
            MissionMaster.Set(responseObjects.mission);
        }
        // �~�b�V�����J�e�S���[�f�[�^�ۑ�
        if (responseObjects.mission_category != null)
        {
            MissionCategories.Set(responseObjects.mission_category);
        }
        // ��V�J�e�S���[�f�[�^�ۑ�
        if (responseObjects.reward_category != null)
        {
            RewardCategories.Set(responseObjects.reward_category);
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
            case GameUtil.Const.HOME_URL:
            case GameUtil.Const.STAMINA_CONSUMPTION:
                UpdateUserData(responseObjects);
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
                // �G���[�R�[�h����������G���[��\��
                if (responseObjects.errcode != null)
                {
                    DisplayErrorTextManager.Instance.DisplayError(responseObjects.errcode);
                }
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
