using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReceivePresent : MonoBehaviour
{
    [SerializeField] GameObject receivePanel, rewardPanel;
    [SerializeField] Image beforeItemImage, afterItemImage;
    [SerializeField] TextMeshProUGUI beforeItemNumText, afterItemNumText;

    GameObject receivePresent;

    int receive_present_id = -1;　// 受け取るプレゼントのID
    int beforeNum, afterNum, rewardCategory;

    GetPresentBoxData getPresentBoxData;
    CreatePresentObj createPresentObj;

    void Awake()
    {
        receivePanel.SetActive(false);
        rewardPanel.SetActive(false);
        getPresentBoxData = GetComponent<GetPresentBoxData>();
        createPresentObj = GetComponent<CreatePresentObj>();
    }

    // 更新前と更新後のアイテムの数を出す
    void GetTargetItemNum(int rewardNum)
    {
        int target = RewardCategories.GetRewardCategoryData(rewardCategory).reward_category;

        switch (target)
        {
            case 1: // 通貨
                beforeNum = Wallets.Get().free_amount + Wallets.Get().paid_amount;
                break;
            case 2: // スタミナ回復アイテム
                beforeNum = Items.GetItemData(10001).item_num;
                break;
            case 3: // 強化ポイント
                beforeNum = Items.GetItemData(20001).item_num;
                break;
            case 4: // 交換アイテム
                beforeNum = Items.GetItemData(30001).item_num;
                break;
            case 5: // 凸アイテム
                break;
            case 6: // 武器
                break;
            default:
                break;
        }
        afterNum = beforeNum + rewardNum;
        UpdateRewardPanel();
    }

    void UpdateRewardPanel()
    {
        beforeItemNumText.text = beforeNum.ToString();
        afterItemNumText.text = afterNum.ToString();

        beforeItemImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resourcesフォルダの中の特定の画像を取得して入れる
        afterItemImage.sprite = Resources.Load<Sprite>(string.Format("RewardImage/r{0}", rewardCategory)); // Resourcesフォルダの中の特定の画像を取得して入れる
    }

    // 受け取るプレゼントIDを設定する
    public void SetReceivePresentId(GameObject target, int setId, int category, int rewardNum)
    {
        receivePresent = target;
        receive_present_id = setId;
        rewardCategory = category;
        GetTargetItemNum(rewardNum);
    }



    // 受け取り確認画面を表示する
    public void DisplayCheckReceivePanel() => receivePanel.SetActive(true);

    // 成功した場合に呼ぶ関数
    void SuccessReceivePresent()
    {
        Debug.Log("プレゼント受け取り");
        rewardPanel.SetActive(true);
        getPresentBoxData.CheckUpdatePresentBox(); // プレゼントボックスの状態を更新
        GameObject[] test = new GameObject[1];
        test[0] = receivePresent;
        createPresentObj.ReceiptedPresent(test);
    }

    // 受け取りボタンが押された時
    public void OnpushReceiveButton()
    {
        List<IMultipartFormSection> receiveForm = new();
        receiveForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        receiveForm.Add(new MultipartFormDataSection("pid", receive_present_id.ToString()));
        Action afterAction = new(() => SuccessReceivePresent());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.RECEIVE_PRESENT_BOX_URL, receiveForm, afterAction));
    }

    // やめるボタンが押された時
    public void OnPushCancelButton() => receivePanel.SetActive(false);

    // 報酬獲得画面の戻るボタンが押されたら
    public void OnPushRewardBackButton()
    {
        rewardPanel.SetActive(false);
        receivePanel.SetActive(false);
    }
}
