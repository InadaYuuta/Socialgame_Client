using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GachaLogManager : WeaponBase
{
    [SerializeField] GameObject gachaLogPanel;
    [SerializeField] TextMeshProUGUI gachaLogText, gachaLogTitleText;
    [SerializeField] TextMeshProUGUI pageText;

    string[] weaponNames, gachaNames, createds;
    int[] weaponIds;
    string gachaLogTitleString = "ガチャ履歴\n\n";
    string gachaLogString = " ";
    string user_id;

    int count = 0;     // 表示する分のカウント
    int pageCount = 1; // 今何枚目のページか
    int pageMax;

    GachaLogModel[] gachaLogModel;

    void Start()
    {
        GetGachaLog();
        gachaLogPanel.SetActive(false);
        user_id = Users.Get().user_id;
    }

    public void GetGachaLog()
    {
        List<IMultipartFormSection> gachaLogForm = new();
        gachaLogForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.GET_GACHA_LOG, gachaLogForm));
        Invoke("UpdateText", 1.0f);
    }

    // 戻るボタンが押されたら
    public void PushBackButton()
    {
        pageCount = 1;
        gachaLogPanel.SetActive(false);
    }

    // 次のページに行くボタンが押されたら
    public void PushNextButton()
    {
        gachaLogModel = GachaLogs.GetGacaLogDataAll();
        pageMax = gachaLogModel.Length / 10;

        if (pageCount < pageMax)
        {
            pageCount++;
            UpdateText();
        }
    }

    // 前のページに行くボタンが押されたら
    public void PushPreviousButton()
    {
        if (pageCount > 1)
        {
            pageCount--;
            UpdateText();
        }
    }

    void GetData()
    {
        gachaLogModel = GachaLogs.GetGacaLogDataAll();
        if (gachaLogModel.Length == 0)
        {
            gachaLogTitleString = "ガチャ履歴はない";
        }
        else
        {
            int displayMin = (pageCount - 1) * 10; // 表示する最低値
            int displayMax = pageCount * 10;       // 表示する最高値
            pageMax = gachaLogModel.Length / 10;

            gachaLogString = "";
            gachaLogTitleString = "ガチャ履歴";
            foreach (GachaLogModel gachaLogData in gachaLogModel)
            {
                weaponIds[count] = gachaLogData.weapon_id;
                gachaNames[count] = "テストガチャ"; // TODO: ガチャの種類が増えたらガチャのテーブル追加してそこから取得できるようにする
                weaponNames[count] = WeaponMaster.GetWeaponMasterData(weaponIds[count]).weapon_name;
                createds[count] = gachaLogData.created;
                if (count > displayMin && count <= displayMax)
                {
                    int rarity = GetNthDigitNum(weaponIds[count], 7);
                    switch (rarity)
                    {
                        case 1:
                            gachaLogString = string.Format("{0}{1}.{2}-<color=\"blue\">{3}</color>-{4}\n", gachaLogString, count, gachaNames[count], weaponNames[count], createds[count]);
                            break;
                        case 2:
                            gachaLogString = string.Format("{0}{1}.{2}-<color=\"red\">{3}</color>-{4}\n", gachaLogString, count, gachaNames[count], weaponNames[count], createds[count]);
                            break;
                        case 3:
                            gachaLogString = string.Format("{0}{1}.{2}-<color=\"yellow\">{3}</color>-{4}\n", gachaLogString, count, gachaNames[count], weaponNames[count], createds[count]);
                            break;
                        default:
                            Debug.Log("範囲外のレアリティ");
                            break;
                    }
                }
                count++;
            }
            count = 0;
        }
    }

    // テキストをアップデートしたりモデルを取得したりする
    void UpdateText()
    {
        gachaLogModel = GachaLogs.GetGacaLogDataAll();
        if (gachaLogModel != null)
        {
            weaponNames = new string[gachaLogModel.Length];
            gachaNames = new string[gachaLogModel.Length];
            createds = new string[gachaLogModel.Length];
            weaponIds = new int[gachaLogModel.Length];
        }
        GetData();
        gachaLogText.text = gachaLogString;
        gachaLogTitleText.text = gachaLogTitleString;
        pageText.text = string.Format("{0}/{1}ページ", pageCount, pageMax);
    }
}
