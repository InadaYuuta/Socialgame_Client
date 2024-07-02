using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetPresentBoxData : MonoBehaviour
{
    [SerializeField] PresentBoxModel[] unReceiptPresents;   // 受取前のプレゼントデータ
    [SerializeField] PresentBoxModel[] receiptedPresents;   // 受取済のプレゼントデータ

    PresentBoxManager presentBoxManager;

    private void Awake()
    {
        presentBoxManager = GetComponent<PresentBoxManager>();
        unReceiptPresents = new PresentBoxModel[0];
        receiptedPresents = new PresentBoxModel[0];
    }

    void Start()
    {
    }

    void Update()
    {

    }

    // 成功した場合に呼ぶ関数
    void SuccessGetPresentBoxData()
    {
        Debug.Log("プレゼントデータの取得に成功しました。");
        GetPresentData();
    }

    // プレゼントボックスデータを取得する
    public void CheckUpdatePresentBox()
    {
        List<IMultipartFormSection> getPresentBoxForm = new();
        getPresentBoxForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        Action afterAction = new(() => SuccessGetPresentBoxData());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.GET_PRESENT_BOX_URL, getPresentBoxForm, afterAction));
    }

    // プレゼントのデータを受取済と受取前に分けて取得
    void GetPresentData()
    {
        PresentBoxModel[] allData = PresentBoxes.GetPresentBoxDataAll();

        foreach (var data in allData)
        {
            string display = data.display;
            // 表示可能期間かつ受け取っていなければ受取可能な配列に保存、そうでなければ受取済みの配列に保存
            if (presentBoxManager.CheckCanDisplay(display) && data.receipt != 1)
            {
                Array.Resize(ref unReceiptPresents, unReceiptPresents.Length + 1);
                unReceiptPresents[unReceiptPresents.Length - 1] = data;
            }
            else
            {
                Array.Resize(ref receiptedPresents, receiptedPresents.Length + 1);
                receiptedPresents[receiptedPresents.Length - 1] = data;
            }
        }
        presentBoxManager.SetPresentData(unReceiptPresents, receiptedPresents);
    }


}
