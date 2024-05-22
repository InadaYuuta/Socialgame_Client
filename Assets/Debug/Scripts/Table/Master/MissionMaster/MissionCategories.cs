using System;
using System.Collections.Generic;

[Serializable]
public class MissionCategoryModel
{
    public int mission_category; // �~�b�V�����J�e�S���[
    public string category_name; // �J�e�S���[�̖��O
}

public class MissionCategories : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists mission_categories(mission_category tinyint,category_name varchar,primary key(mission_category))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(MissionCategoryModel[] mission_categories_list)
    {
        foreach (MissionCategoryModel mission_category in mission_categories_list)
        {
            setQuery = "insert or replace into mission_categories(mission_category,category_name) values(" + mission_category.mission_category + ",\"" + mission_category.category_name + "\")";
            RunQuery(setQuery);
        }
    }

    // �S�ẴK�`���J�e�S���[�f�[�^���擾
    public static MissionCategoryModel[] GetMissionCategoryDataAll()
    {
        List<MissionCategoryModel> weaponCategoryList = new();
        getQuery = "select * from mission_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionCategoryModel weaponCategoryModel = new();
            weaponCategoryModel.mission_category = int.Parse(dr["mission_category"].ToString());
            weaponCategoryModel.category_name = dr["category_name"].ToString();
            weaponCategoryList.Add(weaponCategoryModel);
        }
        return weaponCategoryList.ToArray();
    }

    // �w�肳�ꂽ����J�e�S���[�̃f�[�^�̂ݎ擾
    public static MissionCategoryModel GeMissionCategoryData(int mission_category)
    {
        MissionCategoryModel weaponCategoryModel = new();
        getQuery = "select * from mission_categories where mission_category =" + mission_category;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            weaponCategoryModel.mission_category = int.Parse(dr["mission_category"].ToString());
            weaponCategoryModel.category_name = dr["category_name"].ToString();
        }
        return weaponCategoryModel;
    }
}