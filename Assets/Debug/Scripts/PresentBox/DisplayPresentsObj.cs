using UnityEngine;

public class DisplayPresentsObj : MonoBehaviour
{
    GameObject[] unReceiptPresentClones, receiptedPresentClones;
    GameObject[] currentDisplayPresents; // 現在表示中のプレゼント

    Vector3[] displayPos;

    PresentBoxManager presentBoxManager;

    private void Awake()
    {
        displayPos = new Vector3[5];
        int y = 200;
        for (int i = 0; i < displayPos.Length; i++)
        {
            displayPos[i] = new(0, y - (130 * i), 0);
        }
        currentDisplayPresents = new GameObject[5];

        presentBoxManager = GetComponent<PresentBoxManager>();
    }

    // それぞれのクローンを格納
    void SetClones()
    {
        if (presentBoxManager == null) { presentBoxManager = GetComponent<PresentBoxManager>(); }
        if (presentBoxManager.UnReceiptPresentClones != null)
        {
            unReceiptPresentClones = presentBoxManager.UnReceiptPresentClones;
        }
        if (presentBoxManager.ReceiptedPresentClones != null)
        {
            receiptedPresentClones = presentBoxManager.ReceiptedPresentClones;
        }
    }

    // 元々表示されていたものと今回表示するものが違ったら元のプレゼント達を非表示にする
    void CheckPresentsAndHide(GameObject[] checks)
    {
        bool hide = false;
        if (checks[0] != currentDisplayPresents[0]) { hide = true; }
        if (hide)
        {
            for (int i = 0; i < currentDisplayPresents.Length; i++)
            {
                currentDisplayPresents[i].SetActive(false);
            }
        }
    }

    ///<summary>
    /// 指定されたページのプレゼントを表示する
    ///</summary>
    /// <param name="pageNum">何ページ目を表示するか</param>
    /// <param name="isReceipt">表示するプレゼントの種類、trueは受取済</param>
    public void DisplayPresents(int pageNum, bool isReceipt)
    {
        SetClones();
        if (currentDisplayPresents[0] != null) { ResetDisplayPresents(); }
        int displayNum = (pageNum * 5);
        int firstNum = displayNum - 5 > 0 ? displayNum - 5 : 0;
        int count = 0;
        GameObject[] displayClones;

        if (isReceipt) { displayClones = receiptedPresentClones; }
        else { displayClones = unReceiptPresentClones; }

        for (int i = firstNum; i < displayNum; i++)
        {
            if (displayClones == null) { continue; }
            displayClones[i].SetActive(true);
            RectTransform rect = displayClones[i].GetComponent<RectTransform>();
            rect.anchoredPosition = displayPos[count];
            currentDisplayPresents[count] = displayClones[i]; // 現在表示中のものを格納
            count++;
        }
    }

    // 表示するプレゼントを更新するときに呼び出す
    void ResetDisplayPresents()
    {
        foreach (var display in currentDisplayPresents)
        {
            display.SetActive(false);
        }
    }
}
