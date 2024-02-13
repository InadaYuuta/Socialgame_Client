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
        getQuery = "select * from items where item_id =" + item_id;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemsModel.item_id = int.Parse(dr["item_id"].ToString());
            itemsModel.item_num = int.Parse(dr["item_num"].ToString());
            itemsModel.used_num = int.Parse(dr["used_num"].ToString());
        }
        return itemsModel;
    }
}
