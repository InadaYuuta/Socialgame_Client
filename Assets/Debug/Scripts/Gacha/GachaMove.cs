using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Unity.VisualScripting;

public class GachaMove : WeaponBase
{
    [SerializeField] GameObject gachaSingleResult, gachaMultiResult;
    [SerializeField] GameObject resultWeapon, panel;
    [SerializeField] TextMeshProUGUI[] fragmentText;
    [SerializeField] Sprite[] weaponsSprites;
    GameObject[] weaponClone;

    [SerializeField] Vector3[] MultiPos;

    int fragmentNum = 0;
    int[] newWeapons, resultWeapons = { };

    void Start()
    {
        weaponClone = new GameObject[10];
        gachaSingleResult.SetActive(false);
        resultWeapons = new int[10];
        newWeapons = new int[10];

        gachaMultiResult.SetActive(false);
        panel.SetActive(false);
    }

    void SuccessGachaSingle()
    {
        ResultPanelController.HideCommunicationPanel();
        SingleWait();
    }

    void SuccessGachaMulti()
    {
        ResultPanelController.HideCommunicationPanel();
        MultiWait();
    }

    // �P���K�`���̎��̓���
    public void SingleMove()
    {
        if (Wallets.Get().free_amount + Wallets.Get().paid_amount > 30)
        {
            ResultPanelController.DisplayCommunicationPanel();
            panel.SetActive(true);
            List<IMultipartFormSection> gachaForm = new();
            gachaForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
            gachaForm.Add(new MultipartFormDataSection("gCount", "1"));
            Action afterAction = new(() => SuccessGachaSingle());
            StartCoroutine(ConnectServer(GameUtil.Const.GACHA_URL, gachaForm, afterAction));
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel("�W�F��������Ȃ�!"));
        }
    }

    // �\�A�K�`���̎��̓���
    public void MultiMove()
    {
        if (Wallets.Get().free_amount + Wallets.Get().paid_amount > 300)
        {
            ResultPanelController.DisplayCommunicationPanel();
            panel.SetActive(true);
            List<IMultipartFormSection> gachaForm = new();
            gachaForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
            gachaForm.Add(new MultipartFormDataSection("gCount", "10"));
            Action afterAction = new(() => SuccessGachaMulti());
            StartCoroutine(ConnectServer(GameUtil.Const.GACHA_URL, gachaForm, afterAction));
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel("�W�F��������Ȃ�!"));
        }
    }

    void SingleWait()
    {
        gachaSingleResult.SetActive(false);
        if (weaponClone[0] != null) { Destroy(weaponClone[0]); }
        gachaSingleResult.SetActive(true);
        weaponClone[0] = Instantiate(resultWeapon, new Vector3(0, 0, 0), Quaternion.identity);
        weaponClone[0].transform.SetParent(gachaSingleResult.transform, false);
        int weaponId = resultWeapons[0];
        WeaponSetting(weaponClone[0], weaponId);
        fragmentText[0].text = string.Format("�擾�����琔:{0}��", fragmentNum);
        Invoke("HidePanel", 1f);
    }

    void MultiWait()
    {
        gachaMultiResult.SetActive(false);
        StartCoroutine(MultiGachaResult());
        fragmentText[1].text = string.Format("�擾�����琔:{0}��", fragmentNum);
    }

    public void PushBackButton()
    {
        gachaSingleResult.SetActive(false);
        Destroy(weaponClone[0]);
        gachaMultiResult.SetActive(false);
    }

    // �T�[�o�[����̏���ۑ�����
    void GachaSetting(ResponseObjects responseObjects)
    {
        if (responseObjects.weapons != null)
        {
            UsersModel usersModel = Users.Get();
            Weapons.Set(responseObjects.weapons, usersModel.user_id);
        }
        if (responseObjects.items != null)
        {
            Items.Set(responseObjects.items);
        }
        if (responseObjects.wallets != null)
        {
            UsersModel usersModel = Users.Get();
            Wallets.Set(responseObjects.wallets, usersModel.user_id);
        }
        if (responseObjects.users != null && !string.IsNullOrEmpty(responseObjects.users.user_id))
        {
            Users.Set(responseObjects.users);
        }
        if (responseObjects.new_weaponIds != null)
        {
            newWeapons = responseObjects.new_weaponIds;
        }
        if (responseObjects.fragment_num != null)
        {
            fragmentNum = responseObjects.fragment_num;
        }
        if (responseObjects.weapons != null)
        {
            int[] gachaResult = new int[10];
            int count = 0;
            foreach (string s in responseObjects.gacha_result.Split('/')) // /��؂��ID���o�͂���
            {
                if (count > 0)
                {
                    gachaResult[count - 1] = int.Parse(s);
                }
                count++;
            }
            resultWeapons = gachaResult;
        }

    }

    void HidePanel()
    {
        panel.SetActive(false);
    }

    // �T�[�o�[�ɐڑ�����
    public IEnumerator ConnectServer(string connectURL, List<IMultipartFormSection> parameter, Action action)
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
                            break;
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
                yield return new WaitForSeconds(0.5f);
                GachaSetting(responseObjects);

                // ����I���A�N�V�������s
                if (action != null)
                {
                    action();
                    action = null;
                }
            }
        }
    }

    // 10�̌��ʂ��o��
    public IEnumerator MultiGachaResult()
    {
        // �O�̂��̂��c���Ă��Ȃ����`�F�b�N
        for (int i = 0; i < 10; i++)
        {
            if (weaponClone[i] != null)
            {
                Destroy(weaponClone[i]);
            }
        }

        yield return new WaitForSeconds(0.5f);
        gachaMultiResult.SetActive(true);

        for (int i = 0; i < 10; i++)
        {
            weaponClone[i] = Instantiate(resultWeapon, MultiPos[i], Quaternion.identity);
            weaponClone[i].transform.SetParent(gachaMultiResult.transform, false);
            int weaponId = resultWeapons[i];
            WeaponSetting(weaponClone[i], weaponId);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        Invoke("HidePanel", 1f);
        yield return null;
    }

}
