using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LimitBreakManager : MonoBehaviour
{
    [SerializeField] GameObject reinforcePanel;
    [SerializeField] TextMeshProUGUI changeLimitBreaklText, consumptionConvexItemText, weaponDataLimitBreakText;
    [SerializeField] Image LimitBreakButton;

    string ConvexStr = "凸";
    string arrowStr = " -> ";
    string necessaryImteStr = "必要";
    string spaceStr = "   ";
    string slashStr = " / ";
    int currentLimitBreak, afterLimitBreak, consumptionItem, currentItem;
    int limitBreakpWeaponId;

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
        if (limitBreakpWeaponId == 0) { return; }
        // 武器や所持している強化アイテム等のテキスト更新
        SetLimitBreakUpWeaponData();
        CheckCanLimitBreak();
    }

    // 凸をする武器の情報を取得する
    public void SetLimitBreakWeaponParameter(int weapon_id)
    {
        limitBreakpWeaponId = weapon_id;
        SetLimitBreakUpWeaponData();
    }

    // 限界突破する武器のIDから武器画像と現在の限界突破、所持凸アイテムなどを設定する
    public void SetLimitBreakUpWeaponData()
    {
        currentLimitBreak = Weapons.GetWeaponData(limitBreakpWeaponId).limit_break;
        if (currentLimitBreak < 5) { afterLimitBreak = currentLimitBreak + 1; }
        else { afterLimitBreak = 5; }
        consumptionItem = 1; // TODO: 今後凸に必要なアイテムが増えたら増やす

        ItemsModel test = Items.GetWeaponItemData(limitBreakpWeaponId);

        currentItem = Items.GetWeaponItemData(limitBreakpWeaponId).item_num;
        // 各テキストの中身を書き換え
        changeLimitBreaklText.text = string.Format("{0}{1}{2}{3}{4}", ConvexStr, currentLimitBreak, arrowStr, ConvexStr, afterLimitBreak);
        consumptionConvexItemText.text = string.Format("{0}{1}{2}{3}{4}", necessaryImteStr, spaceStr, consumptionItem, slashStr, currentItem);
        weaponDataLimitBreakText.text = string.Format("{0}{1}", ConvexStr, currentLimitBreak);
    }

    // 強化ポイントが足りているか確認してボタンを押せる状態かどうか判別する
    void CheckCanLimitBreak()
    {
        consumptionItem = 1;
        currentItem = Items.GetWeaponItemData(limitBreakpWeaponId).item_num;
        currentLimitBreak = Weapons.GetWeaponData(limitBreakpWeaponId).limit_break;

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
        List<IMultipartFormSection> levelUpForm = new();
        levelUpForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        levelUpForm.Add(new MultipartFormDataSection("wid", limitBreakpWeaponId.ToString()));
        Debug.Log("限界突破成功");
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.WEAPON_LIMIT_BREAK_URL, levelUpForm));
    }
}
