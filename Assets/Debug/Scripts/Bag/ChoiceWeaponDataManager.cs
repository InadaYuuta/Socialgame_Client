using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceWeaponDataManager : WeaponBase
{
    int weapon_id;
    public int WeaponId { get { return weapon_id; } }

    int weapon_level;
    public int weaponLevel { get { return weapon_level; } }

    [SerializeField] TextMeshProUGUI detailNameText, reinforceNameText, evolutionNameText, detailLimitBreakText, reinforceLimitBreakText, evolutionLimitBreakText;
    [SerializeField] GameObject detailWaponBack, reinforceWeaponBack, evolutionWeaponBack;
    [SerializeField] Image convexWeaponBack;
    [SerializeField] GameObject detailBack;
    public GameObject DetailBack { get { return detailBack; } }

    private void Awake() => detailBack.SetActive(false);

    // 武器が選択された時、強化や進化のために武器の基本データを保存する
    public void SetChoiceWeaponData(int id, int level)
    {
        weapon_id = id;
        weapon_level = level;
    }

    // 詳細画面の設定、強化ボタンが押された時に画像等が変わる(武器)
    public void SetDetailData(int id)
    {
        WeaponModel weaponData = Weapons.GetWeaponData(id);
        string name = WeaponMaster.GetWeaponMasterData(id).weapon_name;
        // 名前
        detailNameText.text = name;
        reinforceNameText.text = name;
        evolutionNameText.text = name;

        // 限界突破情報
        detailLimitBreakText.text = string.Format("凸{0}", weaponData.limit_break);
        reinforceLimitBreakText.text = string.Format("凸{0}", weaponData.limit_break);
        evolutionLimitBreakText.text = string.Format("凸{0}", weaponData.limit_break);

        // 画像
        WeaponSetting(detailWaponBack, id);     // バッグ画面の武器詳細画面の武器画像変更
        WeaponSetting(reinforceWeaponBack, id); // 強化画面の武器詳細画面の武器画像変更
        WeaponSetting(evolutionWeaponBack, id); // 強化画面の武器詳細画面の武器画像変更
        ChangeOnlyWeaponImage(convexWeaponBack, id); // 凸アイテムの画像変更
    }

}
