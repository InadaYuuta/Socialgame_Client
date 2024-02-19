using UnityEngine;

public class BagSortManager : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Canvas bagCanvas;
    GameObject[] itemClone;
    Vector3 referencePoint = new Vector3(170, 620, 0); // 基準点

    int generateVerticalNum = 3; // 生成する縦の個数
    int generateBesideNum = 8;   // 生成する横の個数
    int changeNum = 200;         // 縦横の配置で変化する量  

    int weaponNum;

    int[] weaponId, weaponLevel, weaponExp, weaponCategory;
    string[] weaponName;

    [SerializeField] WeaponModel[] weaponModel;

    private void OnEnable()
    {
        SetWeaponParameters();
        SortGenerate();
    }

    private void OnDisable() => DestroyItemClone();

    void Update()
    {

    }

    // TODO: ここもあらかじめWeaponMasterのデータを保持しているオブジェクトを用意してそこから取得するようにする
    void SetWeaponParameters()
    {
        weaponModel = Weapons.GetWeaponDataAll();
        weaponNum = weaponModel.Length;
        weaponId = new int[weaponNum];
        weaponLevel = new int[weaponNum];
        weaponExp = new int[weaponNum];
        weaponCategory = new int[weaponNum];
        weaponName = new string[weaponNum];
        itemClone = new GameObject[generateVerticalNum * generateBesideNum];

        for (int i = 0; i < weaponNum; i++)
        {
            weaponId[i] = weaponModel[i].weapon_id;
            weaponLevel[i] = weaponModel[i].level;
            weaponExp[i] = weaponModel[i].current_exp;
            weaponCategory[i] = WeaponsMaster.GetWeaponMasterData(weaponId[i]).weapon_category;
            weaponName[i] = WeaponsMaster.GetWeaponMasterData(weaponId[i]).weapon_name;
        }
    }

    // アイテムを並べる
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
                        itemClone[count].name = string.Format("{0}", weaponName[j]);
                    }
                    itemClone[count].transform.parent = bagCanvas.transform;
                }
                count++;
            }
        }
    }

    // アイテムを削除する
    void DestroyItemClone()
    {
        for (int i = 0; i < generateVerticalNum; i++)
        {
            for (int j = 0; j < generateBesideNum; j++)
            {
                Destroy(itemClone[j]);
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
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }
}
