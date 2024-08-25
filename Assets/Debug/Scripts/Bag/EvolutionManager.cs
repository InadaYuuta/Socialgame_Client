using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class EvolutionManager : WeaponBase
{
    [SerializeField] GameObject EvolutionPanel;
    [SerializeField] TextMeshProUGUI necessaryLevelText, consumptionRPointText, weaponDataEvolutionText, haveReinforcePointText;
    [SerializeField] Image EvolutionButton;

    string convexStr = "凸";
    string levelStr = "Lv";
    string necessaryRPointStr = "必要P";
    string slashStr = " / ";
    int currentLimitBreak, currentLevel, maxLevel = 50, consumptionRPoint, currentRPoint;
    int EvolutionWeaponId;

    bool isPush = false; // ボタンを押せるか
    enum UnPushReason
    {
        NONE = 0, // 押せる    
        SHORTAGE, // 所持数不足
        LEVELNOTENOUGH,      // 上限に達している
        EVOLUTED, // 進化済み
    }
    UnPushReason currentState = UnPushReason.NONE; // ボタンが押せない理由

    ChoiceWeaponDataManager choiceWeaponDataManager;
    ChangeImageColor changeImageColor;

    void Start()
    {
        if (EvolutionPanel != null)
        {
            EvolutionPanel.SetActive(false);
        }
        choiceWeaponDataManager = FindObjectOfType<ChoiceWeaponDataManager>();
        changeImageColor = FindObjectOfType<ChangeImageColor>();
    }

    private void Update()
    {
        if (!EvolutionPanel.activeInHierarchy) { return; }
        if (EvolutionWeaponId == 0) { return; }
        // 武器や所持している強化アイテム等のテキスト更新
        SetEvolutionWeaponData();
        CheckCanEvolution();
        haveReinforcePointText.text = currentRPoint.ToString();
    }

    // 凸をする武器の情報を取得する
    public void SetEvolutionWeaponParameter(int weapon_id)
    {
        EvolutionWeaponId = weapon_id;
        SetEvolutionWeaponData();
    }

    // 限界突破する武器のIDから武器画像と現在の限界突破、所持凸アイテムなどを設定する
    public void SetEvolutionWeaponData()
    {
        currentLimitBreak = Weapons.GetWeaponData(EvolutionWeaponId).limit_break;
        currentLevel = Weapons.GetWeaponData(EvolutionWeaponId).level;
        consumptionRPoint = GetTheReinforcePointsNeededForEvolution(EvolutionWeaponId);
        currentRPoint = Users.Get().has_reinforce_point;

        // 各テキストの中身を書き換え
        necessaryLevelText.text = string.Format("{0} {1} {2} {3}", levelStr, currentLevel, slashStr, maxLevel);
        consumptionRPointText.text = string.Format("{0}{1}{2}{3}", necessaryRPointStr, consumptionRPoint, slashStr, currentRPoint);
        weaponDataEvolutionText.text = string.Format("{0}{1}", convexStr, currentLimitBreak);
    }

    // 強化ポイントが足りているか確認してボタンを押せる状態かどうか判別する
    void CheckCanEvolution()
    {
        currentLevel = Weapons.GetWeaponData(EvolutionWeaponId).level;
        consumptionRPoint = GetTheReinforcePointsNeededForEvolution(EvolutionWeaponId);
        currentRPoint = Users.Get().has_reinforce_point;
        currentLimitBreak = Weapons.GetWeaponData(EvolutionWeaponId).limit_break;

        ChangeImageColor.ChangeMode changeMode = ChangeImageColor.ChangeMode.UNSELECT;
        int evoluted = Weapons.GetWeaponData(EvolutionWeaponId).evolution;
        if (evoluted >= 1)
        {
            currentState = UnPushReason.EVOLUTED;
        }
        else if (currentLevel < maxLevel)
        {
            currentState = UnPushReason.LEVELNOTENOUGH;
        }
        else if (currentRPoint < consumptionRPoint)
        {
            currentState = UnPushReason.SHORTAGE;
        }
        else
        {
            changeMode = ChangeImageColor.ChangeMode.EVOLUTION;
            currentState = UnPushReason.NONE;
        }
        changeImageColor.ChangeTargetColor(EvolutionButton, changeMode);
    }

    // 進化が成功したら呼ぶ
    void SuccessEvolution()
    {
        var evolutionWeapon = WeaponMaster.GetWeaponMasterData(EvolutionWeaponId).evolution_weapon_id;

        if (Weapons.GetWeaponData(EvolutionWeaponId) != null) { Weapons.DeleteWeapon(EvolutionWeaponId); } // 進化前の武器の削除
        choiceWeaponDataManager.SetDetailData(EvolutionWeaponId);
        StartCoroutine(ResultPanelController.DisplayResultPanel("進化完了"));
    }

    // 武器の進化を行う
    public void Evolution()
    {
        isPush = false;
        // ボタンが押せない理由があればそれを表示してリターン
        switch (currentState)
        {
            case UnPushReason.NONE:
                isPush = true;
                break;
            case UnPushReason.SHORTAGE:
                StartCoroutine(ResultPanelController.DisplayResultPanel("強化ポイントが足りません"));
                break;
            case UnPushReason.LEVELNOTENOUGH:
                StartCoroutine(ResultPanelController.DisplayResultPanel("レベルが足りません"));
                break;
            case UnPushReason.EVOLUTED:
                StartCoroutine(ResultPanelController.DisplayResultPanel("進化済みです"));
                break;
        }
        if (!isPush) { return; }
        List<IMultipartFormSection> evolutionForm = new();
        evolutionForm.Add(new MultipartFormDataSection("uid", Users.Get().user_id));
        evolutionForm.Add(new MultipartFormDataSection("wid", EvolutionWeaponId.ToString()));
        Action afterAction = new(() => SuccessEvolution());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.WEAPON_EVOLUTION_URL, evolutionForm,afterAction));
    }
}
