using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemMasterModel
{
    public int item_id;
    public string item_name;
    public int item_category;
}

public class ItemsMaster : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists item_masters(item_id bigint,item_name varchar,item_category tinyint,primary key(item_id))";
        RunQuery(createQuery);
        // �C���f�b�N�X�쐬
        createQuery = "CREATE INDEX IF NOT EXISTS item_category_index ON item_masters(item_category);";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(ItemMasterModel[] item_master_model_list)
    {
        foreach (ItemMasterModel itemMasterModel in item_master_model_list)
        {
            setQuery = "insert or replace into item_masters(item_id, item_name, item_category) values(" + itemMasterModel.item_id + ", '" + itemMasterModel.item_name + "', " + itemMasterModel.item_category + ")";
            RunQuery(setQuery);
        }
    }

    // �S�ẴA�C�e���f�[�^���擾
    public static ItemMasterModel[] GetItemMasterDataAll()
    {
        List<ItemMasterModel> itemMasterList = new();
        getQuery = "select * from item_masters";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ItemMasterModel itemMasterModel = new();
            itemMasterModel.item_id = int.Parse(dr["item_id"].ToString());
            itemMasterModel.item_name = dr["Name"].ToString();
            itemMasterModel.item_category = int.Parse(dr["item_category"].ToString());
            itemMasterList.Add(itemMasterModel);
        }
        return itemMasterList.ToArray();
    }

    // �w�肳�ꂽ�A�C�e���̃f�[�^�݂̂��擾
    public static ItemMasterModel GetItemMasterData(int item_id)
    {
        ItemMasterModel itemMasterModel = new();
        getQuery = "select * from item_masters where item_id =" + item_id;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemMasterModel.item_id = int.Parse(dr["item_id"].ToString());
            itemMasterModel.item_name = dr["item_name"].ToString();
            itemMasterModel.item_category = int.Parse(dr["item_category"].ToString());
        }
        return itemMasterModel;
    }
}
