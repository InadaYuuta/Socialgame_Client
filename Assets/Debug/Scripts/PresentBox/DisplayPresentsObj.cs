using UnityEngine;

public class DisplayPresentsObj : MonoBehaviour
{
    GameObject[] unReceiptPresentClones, receiptedPresentClones;
    GameObject[] currentDisplayPresents; // ���ݕ\�����̃v���[���g

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

    // ���ꂼ��̃N���[�����i�[
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

    ///<summary>
    /// �w�肳�ꂽ�y�[�W�̃v���[���g��\������
    ///</summary>
    /// <param name="pageNum">���y�[�W�ڂ�\�����邩</param>
    /// <param name="isReceipt">�\������v���[���g�̎�ށAtrue�͎���</param>
    public void DisplayPresents(int pageNum, bool isReceipt)
    {
        SetClones();
        if (currentDisplayPresents[0] != null) { ResetDisplayPresents(); }

        int displayNum = (pageNum * 5);
        int firstNum = displayNum - 5 > 0 ? displayNum - 5 : 0;
        int count = 0;
        GameObject[] displayClones;

        if (isReceipt)
        {
            displayClones = receiptedPresentClones;
        }
        else
        {
            displayClones = unReceiptPresentClones;
        }
        for (int i = firstNum; i < displayNum; i++)
        {
            displayClones[i].SetActive(true);
            RectTransform rect = displayClones[i].GetComponent<RectTransform>();
            rect.anchoredPosition = displayPos[count];
            currentDisplayPresents[count] = displayClones[i]; // ���ݕ\�����̂��̂��i�[
            count++;
        }
    }

    // �\������v���[���g���X�V����Ƃ��ɌĂяo��
    void ResetDisplayPresents()
    {
        foreach (var display in currentDisplayPresents)
        {
            display.SetActive(false);
        }
    }
}