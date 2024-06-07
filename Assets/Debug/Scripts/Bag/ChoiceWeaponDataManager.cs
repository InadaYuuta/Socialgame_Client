using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceWeaponDataManager : WeaponBase
{
    int weapon_id;
    public int WeaponId { get { return weapon_id; } }

    int weapon_level;
    public int weaponLevel { get { return weapon_level; } }

    [SerializeField] TextMeshProUGUI detailNameText, reinforceNameText, limitbreakText;
    [SerializeField] GameObject detailWaponBack, reinforceWeaponBack;
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
    public void SetDetailData(string weapon_name)
    {
        detailNameText.text = weapon_name;
        reinforceNameText.text = weapon_name;
        WeaponModel weaponData = Weapons.GetWeaponData(weapon_id);
        limitbreakText.text = string.Format("凸{0}", weaponData.limit_break);
        WeaponSetting(detailWaponBack, weapon_id);     // バッグ画面の武器詳細画面の武器画像変更
        WeaponSetting(reinforceWeaponBack, weapon_id); // 強化画面の武器詳細画面の武器画像変更
        ChangeOnlyWeaponImage(convexWeaponBack, weapon_id); // 凸アイテムの画像変更
    }

}
