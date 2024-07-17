using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// TODO: 今後時間ができたらプレゼントとミッションの処理をまとめるために親クラスを作成
public class GetItemBase : MonoBehaviour
{
    protected string URL = "";

    // 成功した場合に呼ぶ関数
    void SuccessGetItemData()
    {
        Debug.Log("プレゼントデータの取得に成功しました。");
      //  GetItemData();
    }

    // プレゼントボックスデータを取得する
    public void CheckUpdatePresentBox()
    {
        List<IMultipartFormSection> getItemsForm = new();
        getItemsForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        Action afterAction = new(() => SuccessGetItemData());
        StartCoroutine(CommunicationManager.ConnectServer(URL, getItemsForm, afterAction));
    }

    // プレゼントのデータを受取済と受取前に分けて取得
    //void GetItemData()
    //{
    //    PresentBoxModel[] allData = PresentBoxes.GetPresentBoxDataAll();

    //    foreach (var data in allData)
    //    {
    //        if (CheckDuplication(data)) { continue; }
    //        string display = data.display;
    //        // 表示可能期間かつ受け取っていなければ受取可能な配列に保存、そうでなければ受取済みの配列に保存
    //        if (presentBoxManager.CheckCanDisplay(display) && data.receipt != 1)
    //        {
    //            unReceiptPresents.Add(data);
    //        }
    //        else
    //        {
    //            receiptedPresents.Add(data);
    //        }
    //    }
    //    presentBoxManager.SetPresentData(unReceiptPresents, receiptedPresents);
    //}

    ///// <summary>
    ///// データのIDが重複しているかどうかを確認する、重複していたらTrueを返す
    ///// </summary>
    ///// <param name="target">確認するモデル</param>
    ///// <returns></returns>
    //bool CheckDuplication(PresentBoxModel target)
    //{
    //    bool result = false;

    //    bool isReceipt = target.receipt > 0 ? true : false;

    //    if (isReceipt)
    //    {
    //        foreach (var receiptedData in receiptedPresents)
    //        {
    //            if (receiptedData.present_id == target.present_id) { result = true; }
    //        }
    //    }
    //    else
    //    {
    //        foreach (var unReceiptData in unReceiptPresents)
    //        {
    //            if (unReceiptData.present_id == target.present_id) { result = true; }
    //        }
    //    }

    //    return result;
    //}

}
