using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GachaMove : MonoBehaviour
{
    [SerializeField] GameObject gachaSingleResult, gachaMultiResult;
    [SerializeField] GameObject resultWeapon, panel;
    [SerializeField] TextMeshProUGUI fragmentText;
    [SerializeField] Sprite[] weaponsSprites;
    GameObject[] weaponClone;

    [SerializeField] Vector3[] MultiPos;

    int fragmentNum = 0;
    string[] newWeapons, resultWeapons = { };

    bool[] singleAnimStart = { false, false, false, false };

    void Start()
    {
        weaponClone = new GameObject[10];
        gachaSingleResult.SetActive(false);
        resultWeapons = new string[10];
        newWeapons = new string[10];

        gachaMultiResult.SetActive(false);
        panel.SetActive(false);
    }

    void GenerateWeaponClone(GameObject weaponClone, int weapon_id)
    {
        Outline outline = weaponClone.GetComponent<Outline>();
        Image image = weaponClone.transform.GetChild(0).GetComponent<Image>();
        switch (weapon_id)
        {
            case 1010001:
                outline.effectColor = Color.blue;
                image.sprite = weaponsSprites[0];
                break;
            case 1020001:
                outline.effectColor = Color.blue;
                image.sprite = weaponsSprites[1];
                break;
            case 1030001:
                outline.effectColor = Color.blue;
                image.sprite = weaponsSprites[2];
                break;
            case 2020001:
                outline.effectColor = Color.red;
                image.sprite = weaponsSprites[3];
                break;
            case 3010001:
                outline.effectColor = Color.yellow;
                image.sprite = weaponsSprites[4];
                break;
            default:
                break;
        }
    }

    // �P���K�`���̎��̓���
    public void SingleMove()
    {
        if (Wallets.Get().free_amount + Wallets.Get().paid_amount > 0)
        {
            panel.SetActive(true);
            List<IMultipartFormSection> gachaForm = new();
            gachaForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
            gachaForm.Add(new MultipartFormDataSection("gCount", "1"));
            StartCoroutine(ConnectServer(GameUtil.Const.GACHA_URL, gachaForm));
            Invoke("TestWait", 1f);
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel("�W�F��������Ȃ�!"));
        }

    }

    // �\�A�K�`���̎��̓���
    public void MultiMove()
    {
        List<IMultipartFormSection> gachaForm = new();
        gachaForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        gachaForm.Add(new MultipartFormDataSection("gCount", "1"));
        StartCoroutine(ConnectServer(GameUtil.Const.GACHA_URL, gachaForm));
        Invoke("TestWaitMulti", 1f);
    }

    void TestWait()
    {
        gachaSingleResult.SetActive(true);
        weaponClone[0] = Instantiate(resultWeapon, new Vector3(0, 0, 0), Quaternion.identity);
        weaponClone[0].transform.SetParent(gachaSingleResult.transform, false);
        int weaponId = int.Parse(resultWeapons[0]);
        GenerateWeaponClone(weaponClone[0], weaponId);
        fragmentText.text = string.Format("�擾�����琔:{0}��", fragmentNum);
        Invoke("HidePanel", 0.5f);
    }

    void TestWaitMulti()
    {
        gachaMultiResult.SetActive(true);
        for (int i = 0; i < 9; i++)
        {
            weaponClone[i] = Instantiate(resultWeapon, MultiPos[i], Quaternion.identity);
            weaponClone[i].transform.SetParent(gachaSingleResult.transform, false);
            int weaponId = int.Parse(resultWeapons[i]);
            GenerateWeaponClone(weaponClone[i], weaponId);
        }
        fragmentText.text = string.Format("�擾�����琔:{0}��", fragmentNum);
    }

    public void PushBackButton()
    {
        gachaSingleResult.SetActive(false);
        Destroy(weaponClone[0]);
        gachaMultiResult.SetActive(false);
    }

    void TestGachaMove(ResponseObjects responseObjects)
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
            resultWeapons = responseObjects.gacha_result;
        }

    }

    void HidePanel()
    {
        panel.SetActive(false);
    }

    public IEnumerator ConnectServer(string connectURL, List<IMultipartFormSection> parameter)
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
                TestGachaMove(responseObjects);
            }
        }
    }

}
