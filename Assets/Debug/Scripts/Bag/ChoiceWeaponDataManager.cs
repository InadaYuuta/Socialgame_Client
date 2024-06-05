using TMPro;
using UnityEngine;

public class ChoiceWeaponDataManager : WeaponBase
{
    int weapon_id;
    public int WeaponId { get { return weapon_id; } }

    int weapon_level;
    public int weaponLevel { get { return weapon_level; } }

    [SerializeField] TextMeshProUGUI detailNameText, reinforceNameText;
    [SerializeField] GameObject detailWaponBack, reinforceWeaponBack;
    [SerializeField] GameObject detailBack;
    public GameObject DetailBack { get { return detailBack; } }

    private void Awake() => detailBack.SetActive(false);

    // 武器が選択された時、強化や進化のために武器の基本データを保存する
    public void SetChoiceWeaponData(int id, int level)
    {
        weapon_id = id;
        weapon_level = level;
    }

    // 詳細画面の設定
    public void SetDetailData(string item_name)
    {
        detailNameText.text = item_name;
        reinforceNameText.text = item_name;
        WeaponSetting(detailWaponBack, weapon_id);     // バッグ画面の武器詳細画面の武器画像変更
        WeaponSetting(reinforceWeaponBack, weapon_id); // 強化画面の武器詳細画面の武器画像変更
    }

}
