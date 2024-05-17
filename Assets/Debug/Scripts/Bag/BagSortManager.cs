using UnityEngine;
using UnityEngine.UI;

public class BagSortManager : WeaponBase
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject items;
    [SerializeField] GameObject lineupButton;
    [SerializeField] GameObject[] itemClone;
    [SerializeField] Sprite[] lineUpImage;
    Vector3 referencePoint = new Vector3(170, 620, 0); // 基準点

    int generateVerticalNum = 3; // 生成する縦の個数
    int generateBesideNum = 5;   // 生成する横の個数
    int changeNum = 200;         // 縦横の配置で変化する量  

    int weaponNum;

    int[] weaponId, weaponLevel, weaponExp, weaponCategory;
    [SerializeField] string[] weaponName;

    [SerializeField] WeaponModel[] weaponModel;

    bool sortMode = true; // 昇順か降順か、trueが昇順

    const string DEFAULTSORT = "DEFAULT";
    const string RARITYSORTASC = "RARITYASC";
    const string RARITYSORTDESC = "RARITYDESC";

    private void OnEnable()
    {
        itemClone = new GameObject[generateVerticalNum * generateBesideNum];
        SetWeaponParameters(DEFAULTSORT);
        SortGenerate();
    }

    private void OnDisable() => DestroyItemClone();

    // TODO: ここもあらかじめWeaponMasterのデータを保持しているオブジェクトを用意してそこから取得するようにする
    void SetWeaponParameters(string setMode)
    {
        switch (setMode)
        {
            case DEFAULTSORT:
            case RARITYSORTASC:
                weaponModel = Weapons.GetRaritySortDesc(true); // レアリティ順に昇順で取得
                break;
            case RARITYSORTDESC:
                weaponModel = Weapons.GetRaritySortDesc(false); // レアリティ順に降順で取得
                break;
            default:
                weaponModel = Weapons.GetWeaponDataAll();
                break;
        }
        weaponNum = weaponModel.Length;
        weaponId = new int[weaponNum];
        weaponLevel = new int[weaponNum];
        weaponExp = new int[weaponNum];
        weaponCategory = new int[weaponNum];
        weaponName = new string[weaponNum];

        for (int i = 0; i < weaponNum; i++)
        {
            weaponId[i] = weaponModel[i].weapon_id;
            weaponLevel[i] = weaponModel[i].level;
            weaponExp[i] = weaponModel[i].current_exp;
            weaponCategory[i] = WeaponMaster.GetWeaponMasterData(weaponId[i]).weapon_category;
            weaponName[i] = WeaponMaster.GetWeaponMasterData(weaponId[i]).weapon_name;
        }
    }

    // アイテムを並べて生成する
    public void SortGenerate()
    {
        int count = 0;
        for (int i = 0; i < generateVerticalNum; i++)
        {
            for (int j = 0; j < generateBesideNum; j++)
            {
                Vector3 setPos = new Vector3(referencePoint.x + (changeNum * j), referencePoint.y - (changeNum * i), 0);
                if (count < itemClone.Length && itemClone[count] == null)
                {
                    itemClone[count] = Instantiate(itemPrefab, setPos, Quaternion.identity);
                    if (weaponName.Length > count)
                    {
                        itemClone[count].name = weaponName[j].ToString();
                        WeaponSetting(itemClone[count], weaponId[j]);
                    }
                    itemClone[count].transform.parent = items.transform;
                }
                count++;
            }
        }
    }

    // アイテムを並び替える
    void SortItems()
    {
        int count = 0;
        for (int i = 0; i < generateVerticalNum; i++)
        {
            for (int j = 0; j < generateBesideNum; j++)
            {
                Vector3 setPos = new Vector3(referencePoint.x + (changeNum * j), referencePoint.y - (changeNum * i), 0);
                if (count < itemClone.Length)
                {
                    if (weaponName.Length > count)
                    {
                        itemClone[count].name = weaponName[j].ToString();
                        WeaponSetting(itemClone[count], weaponId[j]);
                    }
                    itemClone[count].transform.position = setPos;
                }
                count++;
            }
        }
    }

    public void RaritySort()
    {

    }


    // アイテムを削除する
    void DestroyItemClone()
    {
        for (int i = 0; i < generateVerticalNum; i++)
        {
            for (int j = 0; j < generateBesideNum; j++)
            {
                if (itemClone[j] != null) { Destroy(itemClone[j]); }
            }
        }
    }

    // 並び替えボタンが押されたら
    public void PushSortButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }

    // 絞り込みボタンが押されたら
    public void PushNarrowDownButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }

    // 昇順、降順のボタンが押されたら
    public void PushLineUpButton()
    {
        Image image = lineupButton.transform.GetChild(0).GetComponent<Image>();
        if (!sortMode)
        {
            SetWeaponParameters(RARITYSORTASC);
            sortMode = true;
            image.sprite = lineUpImage[0];
        }
        else
        {
            SetWeaponParameters(RARITYSORTDESC);
            sortMode = false;
            image.sprite = lineUpImage[1];
        }
        SortItems();
    }
}
