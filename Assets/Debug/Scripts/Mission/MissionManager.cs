using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] GameObject missionPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    [SerializeField] List<MissionsModel> unReceiptMissions = new();   // 受取前のプレゼントデータ
    List<MissionsModel> receiptedMissions = new();   // 受取済のプレゼントデータ

    public List<MissionsModel> ReceiptedMissions { get { return receiptedMissions; } }

    [SerializeField] List<GameObject> unReceiptMissionClones = new();
    List<GameObject> receiptedMissionClones = new();

    public List<GameObject> UnReceiptMissionClones { get { return unReceiptMissionClones; } }
    public List<GameObject> ReceiptedMissionClones { get { return receiptedMissionClones; } }

    CreateMissionObj createMissions;
    DisplayPresentsObj displayPresent;

    int pageNum = 1;
    [SerializeField] int totalPresentsPageNum = 0;
    int totalMissionsNum = 0;
    public int TotalMissionsNum { get { return totalMissionsNum; } }

    bool isSet = false;
    bool displayLog = false; // 表示するのが履歴かどうか(trueが履歴)

    private void Awake()
    {
        createMissions = GetComponent<CreateMissionObj>();
        displayPresent = GetComponent<DisplayPresentsObj>();

        //  missionPanel.SetActive(false);

        GetMissionData getMissions = GetComponent<GetMissionData>();
        getMissions.CheckUpdateMission(); // データ取得
    }

    // 開いたときに毎回履歴ではなく受取可能なプレゼントが表示されるようにする
    private void OnEnable()
    {
        displayLog = false;
    }

    void Update()
    {
        totalMissionsNum = unReceiptMissionClones.Count; // 現在の受け取れるプレゼントの数を更新
    }

    // 表示できるかを確認
    public bool CheckCanDisplay(string dateString)
    {
        string rePlaceDate = dateString.Replace("-", "/");
        DateTime checkDateTime = DateTime.Parse(rePlaceDate);
        DateTime currentTime = DateTime.Now;
        if (checkDateTime > currentTime)
        {
            return true;
        }
        return false;
    }

    // プレゼントデータを設定
    public void SetMissionData(List<MissionsModel> unReceiptMissionsData, List<MissionsModel> receiptedMissionData)
    {
        unReceiptMissions = unReceiptMissionsData;
        receiptedMissions = receiptedMissionData;

        // 最初の一回のみ生成
        if (!isSet)
        {
            createMissions.CreateMissions(unReceiptMissions, receiptedMissions);
        }
    }

    // プレゼントクローン設定
    public void SetMissionClones(List<GameObject> unreceipts, List<GameObject> receipteds)
    {
        if (unreceipts != null)
        {
            unReceiptMissionClones = unreceipts;
        }
        if (receipteds != null)
        {
            receiptedMissionClones = receipteds;
        }
        // 最初の一回のみプレゼントボックスの１ページ目を表示
        //if (!isSet)
        //{
        //    pageNum = 1;
        //    displayPresent.DisplayPresents(pageNum, false);
        //    isSet = true;
        //}
    }

    // 次へのボタンが押されたら次のページへ
    public void OnPushNextButton()
    {
        if (pageNum < totalPresentsPageNum)
        {
            pageNum++;
            displayPresent.DisplayPresents(pageNum, displayLog);
        }
    }

    // 前へのボタンが押されたら前のページへ
    public void OnPushPreviousButton()
    {
        if (pageNum > 1)
        {
            pageNum--;
            displayPresent.DisplayPresents(pageNum, displayLog);
        }
    }

    // パネルを開く
    public void OnPushOpenButton()
    {
        missionPanel.SetActive(true);
    }

    // 戻るボタンが押されたらパネルを閉じる
    public void OnPushBackButton()
    {
        missionPanel.SetActive(false);
    }

    // 履歴ボタンが押されたら
    public void OnPushLogButton()
    {
        if (displayLog)
        {
            displayLog = false;
            modeText.text = "受取履歴";
        }
        else
        {
            displayLog = true;
            modeText.text = "未受取へ";
        }
        pageNum = 1;
        displayPresent.DisplayPresents(pageNum, displayLog);
    }
}
