using UnityEngine;
using UnityEngine.UI;

public class BagSortManager : WeaponBase
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject items;
    [SerializeField] GameObject lineupButton;
    [SerializeField] GameObject[] itemClone;
    [SerializeField] Sprite[] lineUpImage;
    [SerializeField] GameObject detailBack;
    Vector3 referencePoint = new Vector3(170, 620, 0); // ��_

    int generateVerticalNum = 3; // ��������c�̌�
    int generateBesideNum = 5;   // �������鉡�̌�
    int changeNum = 200;         // �c���̔z�u�ŕω������  

    int weaponNum;

    int[] weaponId, weaponLevel, weaponExp, weaponCategory;
    [SerializeField] string[] weaponName;

    [SerializeField] WeaponModel[] weaponModel;

    bool sortMode = true; // �������~�����Atrue������

    const string DEFAULTSORT = "DEFAULT";
    const string RARITYSORTASC = "RARITYASC";
    const string RARITYSORTDESC = "RARITYDESC";


    private void Awake()
    {
        itemClone = new GameObject[generateVerticalNum * generateBesideNum];
        SetWeaponParameters(DEFAULTSORT);
    }

    private void OnDisable() => DestroyItemClone();

    // �O������o�b�O�̏��X�V
    public void UpdateBag()
    {
        DestroyItemClone();
        itemClone = new GameObject[generateVerticalNum * generateBesideNum];
        SetWeaponParameters(DEFAULTSORT);
    }

    // TODO: ���������炩����WeaponMaster�̃f�[�^��ێ����Ă���I�u�W�F�N�g��p�ӂ��Ă�������擾����悤�ɂ���
    void SetWeaponParameters(string setMode)
    {
        switch (setMode)
        {
            case DEFAULTSORT:
            case RARITYSORTASC:
                weaponModel = Weapons.GetRaritySortDesc(true); // ���A���e�B���ɏ����Ŏ擾
                break;
            case RARITYSORTDESC:
                weaponModel = Weapons.GetRaritySortDesc(false); // ���A���e�B���ɍ~���Ŏ擾
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
            bool check = CheckSameId(weaponModel[i].weapon_id);
            if (!check)
            {
                weaponId[i] = weaponModel[i].weapon_id;
                weaponLevel[i] = weaponModel[i].level;
                weaponExp[i] = weaponModel[i].current_exp;
                weaponCategory[i] = WeaponMaster.GetWeaponMasterData(weaponId[i]).weapon_category;
                weaponName[i] = WeaponMaster.GetWeaponMasterData(weaponId[i]).weapon_name;
            }
            else
            { continue; }
        }
        SortGenerate();
    }

    // �d������ID���Ȃ����m�F
    bool CheckSameId(int checkId)
    {
        foreach (int id in weaponId)
        {
            if (id == 0) { continue; }
            if (checkId == id) { return true; }
        }

        return false;
    }

    // �A�C�e������ׂĐ�������
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
                    GameObject currentClone = itemClone[count];
                    if (weaponModel.Length > count)
                    {
                        currentClone.name = weaponName[count].ToString();
                        WeaponSetting(currentClone, weaponId[count]);
                        if (currentClone.GetComponent<BagItemManager>() != null)
                        {
                            BagItemManager bagItemManager = currentClone.GetComponent<BagItemManager>();
                            if (bagItemManager != null)
                            {
                                bagItemManager.SetParameters(weaponId[count], weaponLevel[count], weaponExp[count], weaponCategory[count], weaponName[count]);
                            }
                        }
                    }
                    itemClone[count].transform.parent = items.transform;
                }
                count++;
            }
        }
    }

    // �A�C�e������ёւ���
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
                        itemClone[count].name = weaponName[count].ToString();
                        WeaponSetting(itemClone[count], weaponId[count]);
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


    // �A�C�e�����폜����
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

    // ���ёւ��{�^���������ꂽ��
    public void PushSortButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }

    // �i�荞�݃{�^���������ꂽ��
    public void PushNarrowDownButton()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }

    // �����A�~���̃{�^���������ꂽ��
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
