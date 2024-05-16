using UnityEngine;

public class BagItemManager : MonoBehaviour
{
    int level, Exp, Category, id, rarity_id;
    string Name;

    // 各自パラメータを設定する
    public void SetParameters(int weaponId, int currentLevel, int currentExp, int weaponCategory, string weaponName)
    {
        id = weaponId;
        level = currentLevel;
        Exp = currentExp;
        Category = weaponCategory;
        Name = weaponName;
    }
}
