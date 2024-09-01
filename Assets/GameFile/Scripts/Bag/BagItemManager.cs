using TMPro;
using UnityEngine;

public class BagItemManager : WeaponBase
{
    [SerializeField] int weapon_level, weapon_exp, item_category, weapon_id, rarity_id;
    [SerializeField] string item_name = "no name";
    string default_name = "no name";

    [SerializeField] TextMeshProUGUI detailNameText, reinforceNameText;
    [SerializeField] GameObject detailWaponBack, reinforceWeaponBack;
    GameObject detailBack;
    ChoiceWeaponDataManager choiceManager;

    void OnEnable()
    {
        choiceManager = FindObjectOfType<ChoiceWeaponDataManager>();
        if (choiceManager != null)
        {
            detailBack = choiceManager.DetailBack;
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
        if (item_name == default_name) { return; }
        detailBack.SetActive(true);
        choiceManager.SetChoiceWeaponData(weapon_id, weapon_level);
        choiceManager.SetDetailData(weapon_id);
    }
}
