using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PresentBoxManager : MonoBehaviour
{
    [SerializeField] GameObject PresentPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    List<PresentBoxModel> unReceiptPresents = new();   // 受取前のプレゼントデータ
    List<PresentBoxModel> receiptedPresents = new();   // 受取済のプレゼントデータ

    public List<PresentBoxModel> ReceiptedPresents { get { return receiptedPresents; } }

    [SerializeField] List<GameObject> unReceiptPresentClones = new();
    List<GameObject> receiptedPresentClones = new();

    public List<GameObject> UnReceiptPresentClones { get { return unReceiptPresentClones; } }
    public List<GameObject> ReceiptedPresentClones { get { return receiptedPresentClones; } }

    CreatePresentObj createPresent;
    DisplayPresentsObj displayPresent;

    int pageNum = 1;
    [SerializeField] int totalPresentsPageNum = 0;
    int totalPresentsNum = 0;
    public int TotalPresentsNum { get { return totalPresentsNum; } }

    bool isSet = false;
    bool displayLog = false; // 表示するのが履歴かどうか(trueが履歴)

    private void Awake()
    {
        unReceiptPresents = new List<PresentBoxModel>();
        receiptedPresents = new List<PresentBoxModel>();
        createPresent = GetComponent<CreatePresentObj>();
        displayPresent = GetComponent<DisplayPresentsObj>();

        PresentPanel.SetActive(false);

        GetPresentBoxData getPresent = GetComponent<GetPresentBoxData>();
        getPresent.CheckUpdatePresentBox(); // データ取得
    }

    void Start()
    {
        totalPresentsPageNum = unReceiptPresents.Count / 5;
        if (unReceiptPresents.Count % 5 > 0) { totalPresentsPageNum++; }
        pageText.text = string.Format("{0}/{1}", pageNum, totalPresentsPageNum);
    }

    // 開いたときに毎回履歴ではなく受取可能なプレゼントが表示されるようにする
    private void OnEnable()
    {
        displayLog = false;
    }

    void Update()
    {
        totalPresentsNum = unReceiptPresentClones.Count; // 現在の受け取れるプレゼントの数を更新
        ChangePageText();
    }

    // 表示するプレゼントのページ数を変更
    void ChangePageText()
    {
        if (displayLog)
        {
            totalPresentsPageNum = receiptedPresents.Count / 5;
            if (receiptedPresents.Count % 5 > 0) { totalPresentsPageNum++; }
        }
        else
        {
            totalPresentsPageNum = unReceiptPresents.Count / 5;
            if (unReceiptPresents.Count % 5 > 0) { totalPresentsPageNum++; }
        }
        if (totalPresentsPageNum == 0) { pageNum = 0; }
        pageText.text = string.Format("{0}/{1}", pageNum, totalPresentsPageNum);
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
    public void SetPresentData(List<PresentBoxModel> unReceiptPresentsData, List<PresentBoxModel> receiptedPresentData)
    {
        unReceiptPresents = unReceiptPresentsData;
        receiptedPresents = receiptedPresentData;

        // 最初の一回のみ生成
        if (!isSet)
        {
            createPresent.CreatePresents(unReceiptPresents, receiptedPresents);
        }
    }

    // プレゼントクローン設定
    public void SetPresentClones(List<GameObject> unreceipts, List<GameObject> receipteds)
    {
        if (unreceipts != null)
        {
            unReceiptPresentClones = unreceipts;
        }
        if (receipteds != null)
        {
            receiptedPresentClones = receipteds;
        }
        // 最初の一回のみプレゼントボックスの１ページ目を表示
        if (!isSet)
        {
            pageNum = 1;
            displayPresent.DisplayPresents(pageNum, false);
            isSet = true;
        }
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
        PresentPanel.SetActive(true);
    }

    // 戻るボタンが押されたらパネルを閉じる
    public void OnPushBackButton()
    {
        PresentPanel.SetActive(false);
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
