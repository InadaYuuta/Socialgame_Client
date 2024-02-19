using UnityEngine;

public class BagSortManager : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Canvas bagCanvas;
    GameObject[] itemClone;
    Vector3 referencePoint = new Vector3(170, 620, 0); // ��_

    int generateVerticalNum = 3; // ��������c�̌�
    int generateBesideNum = 8;   // �������鉡�̌�
    int changeNum = 200;         // �c���̔z�u�ŕω������  

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

    // TODO: ���������炩����WeaponMaster�̃f�[�^��ێ����Ă���I�u�W�F�N�g��p�ӂ��Ă�������擾����悤�ɂ���
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

    // �A�C�e������ׂ�
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

    // �A�C�e�����폜����
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
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }
}
