using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class LimitBreakManager : MonoBehaviour
{
    [SerializeField] GameObject reinforcePanel;
    [SerializeField] TextMeshProUGUI changeLimitBreakText, consumptionConvexItemText, weaponDataLimitBreakText;
    [SerializeField] Image LimitBreakButton;

    string ConvexStr = "凸";
    string arrowStr = " -> ";
    string necessaryImteStr = "必要";
    string spaceStr = "   ";
    string slashStr = " / ";
    int currentLimitBreak, afterLimitBreak, consumptionItem, currentItem;
    int limitBreakWeaponId;

    bool isPush = false; // ボタンを押せるか
    enum UnPushReason
    {
        NONE = 0, // 押せる    
        SHORTAGE, // 所持数不足
        MAX       // 上限に達している
    }
    UnPushReason currentState = UnPushReason.NONE; // ボタンが押せない理由

    ChoiceWeaponDataManager weaponData;
    ChangeImageColor changeImageColor;

    void Start()
    {
        if (reinforcePanel != null)
        {
            reinforcePanel.SetActive(false);
        }
        weaponData = FindObjectOfType<ChoiceWeaponDataManager>();
        changeImageColor = FindObjectOfType<ChangeImageColor>();
    }

    private void Update()
    {
        if (!reinforcePanel.activeInHierarchy) { return; }
        if (limitBreakWeaponId == 0) { return; }
        // 武器や所持している強化アイテム等のテキスト更新
        SetLimitBreakWeaponData();
        CheckCanLimitBreak();
    }

    // 凸をする武器の情報を取得する
    public void SetLimitBreakWeaponParameter(int weapon_id)
    {
        limitBreakWeaponId = weapon_id;
        SetLimitBreakWeaponData();
    }

    // 限界突破する武器のIDから武器画像と現在の限界突破、所持凸アイテムなどを設定する
    public void SetLimitBreakWeaponData()
    {
        currentLimitBreak = Weapons.GetWeaponData(limitBreakWeaponId).limit_break;
        if (currentLimitBreak < 5) { afterLimitBreak = currentLimitBreak + 1; }
        else { afterLimitBreak = 5; }
        consumptionItem = 1; // TODO: 今後凸に必要なアイテムが増えたら増やす
        currentItem = Items.GetWeaponItemData(limitBreakWeaponId).item_num;

        // 各テキストの中身を書き換え
        changeLimitBreakText.text = string.Format("{0}{1}{2}{3}{4}", ConvexStr, currentLimitBreak, arrowStr, ConvexStr, afterLimitBreak);
        consumptionConvexItemText.text = string.Format("{0}{1}{2}{3}{4}", necessaryImteStr, spaceStr, consumptionItem, slashStr, currentItem);
        weaponDataLimitBreakText.text = string.Format("{0}{1}", ConvexStr, currentLimitBreak);
    }

    // 強化ポイントが足りているか確認してボタンを押せる状態かどうか判別する
    void CheckCanLimitBreak()
    {
        consumptionItem = 1;
        currentItem = Items.GetWeaponItemData(limitBreakWeaponId).item_num;
        currentLimitBreak = Weapons.GetWeaponData(limitBreakWeaponId).limit_break;

        ChangeImageColor.ChangeMode changeMode = ChangeImageColor.ChangeMode.UNSELECT;

        if (currentLimitBreak < 5)
        {
            if (currentItem >= consumptionItem)
            {
                changeMode = ChangeImageColor.ChangeMode.REINFORCE;
                currentState = UnPushReason.NONE;
            }
            else { currentState = UnPushReason.SHORTAGE; } // 所持アイテムが足りなかったら選択できなくする
        }
        else
        { currentState = UnPushReason.MAX; } // 限界突破が上限まで行われていたら選択できなくする

        changeImageColor.ChangeTargetColor(LimitBreakButton, changeMode);
    }

    // 成功した場合に呼ぶ関数
    void SuccessLimitBreak()
    {
        ResultPanelController.HideCommunicationPanel();
        StartCoroutine(ResultPanelController.DisplayResultPanel("限界突破しました。"));
    }

    // 武器の限界突破を行う
    public void LimitBreak()
    {
        // ボタンが押せない理由があればそれを表示してリターン
        switch (currentState)
        {
            case UnPushReason.NONE:
                isPush = true;
                break;
            case UnPushReason.SHORTAGE:
                StartCoroutine(ResultPanelController.DisplayResultPanel("凸アイテムが足りません"));
                isPush = false;
                break;
            case UnPushReason.MAX:
                StartCoroutine(ResultPanelController.DisplayResultPanel("限界突破上限です"));
                isPush = false;
                break;
        }
        if (!isPush) { return; }
        ResultPanelController.DisplayCommunicationPanel();
        List<IMultipartFormSection> limitBreakForm = new();
        limitBreakForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        limitBreakForm.Add(new MultipartFormDataSection("wid", limitBreakWeaponId.ToString()));
        Action afterAction = new(() => SuccessLimitBreak());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.WEAPON_LIMIT_BREAK_URL, limitBreakForm, afterAction));
    }
}
