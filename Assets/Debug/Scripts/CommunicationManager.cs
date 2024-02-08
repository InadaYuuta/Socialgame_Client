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

                switch (connectURL)
                {
                    case GameUtil.Const.RESISTRATION_URL:
                    case GameUtil.Const.STAMINA_RECOVERY_URL:
                        // ���[�U�[���ۑ�
                        if (responseObjects.users != null && !string.IsNullOrEmpty(responseObjects.users.user_id))
                        {
                            Users.Set(responseObjects.users);
                        }
                        // �ʉݏ��ۑ�
                        if (responseObjects.wallets != null)
                        {
                            UsersModel usersModel = Users.Get();
                            Wallets.Set(responseObjects.wallets, usersModel.user_id);
                        }
                        break;
                    case GameUtil.Const.LOGIN_URL:
                    case GameUtil.Const.STAMINA_CONSUMPTION:
                        // ���[�U�[���ۑ�
                        if (responseObjects.users != null && !string.IsNullOrEmpty(responseObjects.users.user_id))
                        {
                            Users.Set(responseObjects.users);
                        }
                        break;
                    case GameUtil.Const.BUY_CURRENCY_URL:
                        // �ʉݏ��ۑ�
                        if (responseObjects.wallets != null)
                        {
                            UsersModel usersModel = Users.Get();
                            Wallets.Set(responseObjects.wallets, usersModel.user_id);
                        }
                        break;
                    // TODO:�A�C�e���̃e�[�u�����ł�����A�X�^�~�i�񕜗p�̃P�[�X��p�ӂ���
                    default:
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
                        // �ʉ݃V���b�v���ۑ�
                        if (responseObjects.payment_shop != null)
                        {
                            PaymentShops.Set(responseObjects.payment_shop);
                        }
                        break;
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
