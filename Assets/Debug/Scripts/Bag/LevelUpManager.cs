using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelUpManager : WeaponBase
{
    [SerializeField] GameObject reinforcePanel;
    [SerializeField] TextMeshProUGUI changeLevelText, consumptionReinforcePointText;
    [SerializeField] TextMeshProUGUI haveReinforcePointText;
    [SerializeField] Image LevelUpButton;

    string beforeLevelStr = "Lv";
    string afterLevelStr = "Lv";
    string arrowStr = " -> ";
    string necessaryPointStr = "必要 P ";
    string slashStr = " / ";
    int currentLevel, afterLevel, consumptionRPoint, currentRPoint;
    string currentRPointStr;
    int levelUpWeaponId;

    bool isPush = false; // ボタンを押せるか

    ChoiceWeaponDataManager weaponData;
    UserProfileGetManager userProfile;
    ChangeImageColor changeImageColor;

    enum UnPushReason
    {
        NONE = 0, // 押せる    
        SHORTAGE, // 所持数不足
        MAX       // 上限に達している
    }
    UnPushReason currentState = UnPushReason.NONE; // ボタンが押せない理由

    void Start()
    {
        if (reinforcePanel != null)
        {
            reinforcePanel.SetActive(false);
        }
        weaponData = FindObjectOfType<ChoiceWeaponDataManager>();
        userProfile = FindObjectOfType<UserProfileGetManager>();
        changeImageColor = FindObjectOfType<ChangeImageColor>();
    }

    private void Update()
    {
        if (!reinforcePanel.activeInHierarchy) { return; }
        // 武器や所持している強化アイテム等のテキスト更新
        SetLevelUpWeaponData(levelUpWeaponId);
        // 所持ポイントの表示
        currentRPointStr = userProfile.GetCurrentRPointStr();
        haveReinforcePointText.text = currentRPointStr;

        CheckCanLevelUp();
    }

    // レベルアップや凸をする武器の情報を取得する
    public void SetLevelUpWeaponParameter(int weapon_id)
    {
        levelUpWeaponId = weapon_id;
        SetLevelUpWeaponData(levelUpWeaponId);
    }

    // 強化する武器のIDから武器画像と現在レベル、所持凸アイテムなどを設定する
    public void SetLevelUpWeaponData(int weapon_id)
    {
        currentLevel = Weapons.GetWeaponData(weapon_id).level;
        afterLevel = currentLevel >= 50 ? 50 : currentLevel + 1; // 現在のレベルが50以下ならその値に＋１、５０なら５０
        consumptionRPoint = WeaponExps.GetWeaponExpData(weapon_id).use_reinforce_point;
        currentRPoint = Users.Get().has_reinforce_point;
        currentRPointStr = userProfile.GetCurrentRPointStr();
        changeLevelText.text = string.Format("{0}{1}{2}{3}{4}", beforeLevelStr, currentLevel, arrowStr, afterLevelStr, afterLevel);
        consumptionReinforcePointText.text = string.Format("{0}{1}{2}{3}", necessaryPointStr, consumptionRPoint, slashStr, currentRPointStr);
    }

    // 強化ポイントが足りているか確認してボタンを押せる状態かどうか判別する
    void CheckCanLevelUp()
    {
        consumptionRPoint = WeaponExps.GetWeaponExpData(levelUpWeaponId).use_reinforce_point;
        currentRPoint = Users.Get().has_reinforce_point;
        currentLevel = Weapons.GetWeaponData(levelUpWeaponId).level;

        ChangeImageColor.ChangeMode changeMode = ChangeImageColor.ChangeMode.UNSELECT;

        if (currentLevel < 50)
        {
            if (currentRPoint >= consumptionRPoint)
            {
                changeMode = ChangeImageColor.ChangeMode.REINFORCE;
                currentState = UnPushReason.NONE;
            }
            else { currentState = UnPushReason.SHORTAGE; } // 所持ポイントが足りなかったら選択できなくする
        }
        else { currentState = UnPushReason.MAX; } // レベル上限なら選択できないようにする

        changeImageColor.ChangeTargetColor(LevelUpButton, changeMode);
    }

    // 武器のレベルアップを行う
    public void LevelUp()
    {
        // ボタンが押せない理由があればそれを表示してリターン
        switch (currentState)
        {
            case UnPushReason.NONE:
                isPush = true;
                break;
            case UnPushReason.SHORTAGE:
                StartCoroutine(ResultPanelController.DisplayResultPanel("強化ポイントが足りません"));
                isPush = false;
                break;
            case UnPushReason.MAX:
                StartCoroutine(ResultPanelController.DisplayResultPanel("レベル上限です"));
                isPush = false;
                break;
        }
        if (!isPush) { return; }
        List<IMultipartFormSection> levelUpForm = new();
        levelUpForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        levelUpForm.Add(new MultipartFormDataSection("wid", levelUpWeaponId.ToString()));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.WEAPON_LEVEL_UP_URL, levelUpForm));
    }
}
