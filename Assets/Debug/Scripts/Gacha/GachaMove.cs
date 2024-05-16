using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

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

    // 単発ガチャの時の動き
    public void SingleMove()
    {
        if (Wallets.Get().free_amount + Wallets.Get().paid_amount > 30)
        {
            panel.SetActive(true);
            List<IMultipartFormSection> gachaForm = new();
            gachaForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
            gachaForm.Add(new MultipartFormDataSection("gCount", "1"));
            StartCoroutine(ConnectServer(GameUtil.Const.GACHA_URL, gachaForm));
            Invoke("SingleWait", 1f);
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel("ジェムが足りない!"));
        }
    }

    // 十連ガチャの時の動き
    public void MultiMove()
    {
        if (Wallets.Get().free_amount + Wallets.Get().paid_amount > 300)
        {
            panel.SetActive(true);
            List<IMultipartFormSection> gachaForm = new();
            gachaForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
            gachaForm.Add(new MultipartFormDataSection("gCount", "10"));
            StartCoroutine(ConnectServer(GameUtil.Const.GACHA_URL, gachaForm));
            Invoke("MultiWait", 1f);
        }
        else
        {
            StartCoroutine(ResultPanelController.DisplayResultPanel("ジェムが足りない!"));
        }
    }

    void SingleWait()
    {
        gachaSingleResult.SetActive(true);
        weaponClone[0] = Instantiate(resultWeapon, new Vector3(0, 0, 0), Quaternion.identity);
        weaponClone[0].transform.SetParent(gachaSingleResult.transform, false);
        int weaponId = resultWeapons[0];
        WeaponSetting(weaponClone[0], weaponId);
        fragmentText[0].text = string.Format("取得かけら数:{0}個", fragmentNum);
        Invoke("HidePanel", 1f);
    }

    void MultiWait()
    {
        gachaMultiResult.SetActive(true);
        StartCoroutine(MultiGachaResult());
        fragmentText[1].text = string.Format("取得かけら数:{0}個", fragmentNum);
    }

    public void PushBackButton()
    {
        gachaSingleResult.SetActive(false);
        Destroy(weaponClone[0]);
        gachaMultiResult.SetActive(false);
    }

    // サーバーからの情報を保存する
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
        if (responseObjects.wallet != null)
        {
            UsersModel usersModel = Users.Get();
            Wallets.Set(responseObjects.wallet, usersModel.user_id);
        }
        if (responseObjects.user != null && !string.IsNullOrEmpty(responseObjects.user.user_id))
        {
            Users.Set(responseObjects.user);
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
            foreach (string s in responseObjects.gacha_result.Split('/')) // /区切りでIDを出力する
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

    // サーバーに接続する
    public IEnumerator ConnectServer(string connectURL, List<IMultipartFormSection> parameter)
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
                            break;
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
                yield return new WaitForSeconds(0.5f);
                GachaSetting(responseObjects);
            }
        }
    }

    // 10個の結果を出す
    public IEnumerator MultiGachaResult()
    {
        yield return new WaitForSeconds(0.5f);
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
