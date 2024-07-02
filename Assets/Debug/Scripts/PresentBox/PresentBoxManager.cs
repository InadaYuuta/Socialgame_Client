using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentBoxManager : MonoBehaviour
{
    [SerializeField] GameObject PresentPanel;
    [SerializeField] TextMeshProUGUI pageText, modeText;

    PresentBoxModel[] unReceiptPresents;   // 受取前のプレゼントデータ
    PresentBoxModel[] receiptedPresents;   // 受取済のプレゼントデータ

    GameObject[] unReceiptPresentClones;
    GameObject[] receiptedPresentClones;

    public GameObject[] UnReceiptPresentClones { get { return unReceiptPresentClones; } }
    public GameObject[] ReceiptedPresentClones { get { return receiptedPresentClones; } }

    GetPresentBoxData getPresent;
    CreatePresentObj createPresent;
    DisplayPresentsObj displayPresent;

    int pageNum = 1;
    [SerializeField] int totalPresentNum = 0;

    bool isSet = false;
    bool displayLog = false;

    private void Awake()
    {
        unReceiptPresents = new PresentBoxModel[PresentBoxes.GetPresentBoxDataAll().Length];
        receiptedPresents = new PresentBoxModel[PresentBoxes.GetPresentBoxDataAll().Length];
        getPresent = GetComponent<GetPresentBoxData>();
        createPresent = GetComponent<CreatePresentObj>();
        displayPresent = GetComponent<DisplayPresentsObj>();

        getPresent.CheckUpdatePresentBox(); // データ取得
    }

    void Start()
    {
        totalPresentNum = unReceiptPresents.Length / 5; // TODO:履歴追加するときにここも改良する
        pageText.text = string.Format("{0}/{1}", pageNum, totalPresentNum);
    }

    void Update()
    {
        if (displayLog) { totalPresentNum = receiptedPresents.Length / 5; /* TODO:履歴追加するときにここも改良する*/ }
        else { totalPresentNum = unReceiptPresents.Length / 5; /* TODO:履歴追加するときにここも改良する */}
        if (totalPresentNum == 0) { pageNum = 0; }
        pageText.text = string.Format("{0}/{1}", pageNum, totalPresentNum);
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
    public void SetPresentData(PresentBoxModel[] unReceiptPresentsData, PresentBoxModel[] receiptedPresentData)
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
    public void SetPresentClones(GameObject[] unreceipts, GameObject[] receipteds)
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
        if (pageNum < totalPresentNum)
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
