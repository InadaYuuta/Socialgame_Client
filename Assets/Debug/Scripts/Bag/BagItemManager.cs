using TMPro;
using UnityEngine;

public class BagItemManager : WeaponBase
{
    [SerializeField] int weapon_level, weapon_exp, item_category, weapon_id, rarity_id;
    [SerializeField] string item_name = "no name";
    string default_name = "no name";

    [SerializeField] TextMeshProUGUI name;
    [SerializeField] GameObject weaponBack;

    void OnEnable()
    {
        if (GameObject.Find("DetailBack") != null)
        {
            GameObject detailBack = GameObject.Find("DetailBack");
            if (detailBack.activeInHierarchy == false) { return; }
            name = GameObject.Find("BagWeaponName").GetComponent<TextMeshProUGUI>();
            weaponBack = GameObject.Find("WeaponImageData");
        }
    }

    /// <summary>
    /// 各自パラメータを設定する
    /// </summary>
    /// <param weaponId="weaponId"></param>
    /// <param level="currentLevel"></param>
    /// <param exp="currentExp"></param>
    /// <param category="weaponCategory"></param>
    /// <param name="weaponName"></param>
    public void SetParameters(int weaponId, int currentLevel, int currentExp, int weaponCategory, string weaponName)
    {
        weapon_id = weaponId;
        weapon_level = currentLevel;
        weapon_exp = currentExp;
        item_category = weaponCategory;
        item_name = weaponName;
    }

    // クリックされた時に指定されたIDの武器の情報を表示する
    public void DisplayWeaponData()
    {
        if (item_name == default_name || name == null || weaponBack == null) { return; }
        name.text = item_name;
        WeaponSetting(weaponBack, weapon_id);
    }
}
