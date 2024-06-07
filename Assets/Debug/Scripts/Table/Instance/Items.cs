using System;
using System.Collections.Generic;

[Serializable]
public class ItemsModel
{
    public int item_id;  // �A�C�e��ID
    public int item_num; // �A�C�e���̏�����
    public int used_num; // �A�C�e���̎g�p��
}

public class Items : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists items(item_id bigint,item_num mediumint,used_num mediumint,primary key(item_id))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(ItemsModel[] items_model_list)
    {
        foreach (ItemsModel itemsModel in items_model_list)
        {
            setQuery = "insert or replace into items(item_id,item_num,used_num) values(" + itemsModel.item_id + "," + itemsModel.item_num + "," + itemsModel.used_num + ")";
            RunQuery(setQuery);
        }
    }

    // �A�C�e���̃f�[�^�S�擾
    public static ItemsModel[] GetItemDataAll()
    {
        List<ItemsModel> list = new();
        getQuery = "select * from items";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ItemsModel itemsModel = new();
            itemsModel.item_id = int.Parse(dr["item_id"].ToString());
            itemsModel.item_num = int.Parse(dr["item_num"].ToString());
            itemsModel.used_num = int.Parse(dr["used_num"].ToString());
            list.Add(itemsModel);
        }
        return list.ToArray(); // List��z��ɕϊ����ĕԂ�
    }

    // �w�肳�ꂽ���iID�̏��i�������擾
    public static ItemsModel GetItemData(int item_id)
    {
        ItemsModel itemsModel = new();
        getQuery = string.Format("select * from items where item_id = {0}", item_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemsModel.item_id = int.Parse(dr["item_id"].ToString());
            itemsModel.item_num = int.Parse(dr["item_num"].ToString());
            itemsModel.used_num = int.Parse(dr["used_num"].ToString());
        }
        return itemsModel;
    }

    // �w�肳�ꂽ����ID�̃A�C�e�����擾
    public static ItemsModel GetWeaponItemData(int weapon_id)
    {
        int item_id = CheckWeaponIdItem(weapon_id);
        ItemsModel itemsModel = new();
        getQuery = string.Format("select * from items where item_id = {0}", item_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemsModel.item_id = int.Parse(dr["item_id"].ToString());
            itemsModel.item_num = int.Parse(dr["item_num"].ToString());
            itemsModel.used_num = int.Parse(dr["used_num"].ToString());
        }
        return itemsModel;
    }

    // ���킲�ƂɓʃA�C�e����ID��Ԃ�
    // TODO:����s�ւ����獡�㏔�X�̎������I�������A�e�[�u���݌v���������B�@��̓I�ɂ�Weapons�e�[�u���ɑΉ�����ʃA�C�e����ID��������
    static int CheckWeaponIdItem(int weapon_id)
    {
        int item_id = 0;

        switch (weapon_id)
        {
            case 1010001:
                item_id = 40002; // ���ʂ̌��ʃA�C�e��
                break;
            case 1020001:
                item_id = 40003; // ���ʂ̋|�ʃA�C�e��
                break;
            case 1030001:
                item_id = 40004; // ���ʂ̑��ʃA�C�e��
                break;
            case 2020001:
                item_id = 40005; // �����|�ʃA�C�e��
                break;
            case 3010001:
                item_id = 40006; // �߂����ይ�����ʃA�C�e��
                break;
        }

        return item_id;
    }

}
