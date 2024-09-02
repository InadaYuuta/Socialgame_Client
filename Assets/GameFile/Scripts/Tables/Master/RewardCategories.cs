using System;
using System.Collections.Generic;

[Serializable]
public class RewardCategoryModel
{
    public int reward_category;  // ��V�J�e�S���[
    public string reward_category_name; // �J�e�S���[�̖��O
}

public class RewardCategories : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists reward_categories(reward_category tinyint,category_name varchar,primary key(reward_category))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(RewardCategoryModel[] mission_categories_list)
    {
        foreach (RewardCategoryModel mission_category in mission_categories_list)
        {
            setQuery = "insert or replace into reward_categories(reward_category,category_name) values(" + mission_category.reward_category + ",\"" + mission_category.reward_category_name + "\")";
            RunQuery(setQuery);
        }
    }

    // �S�ẴK�`���J�e�S���[�f�[�^���擾
    public static RewardCategoryModel[] GetRewardCategoryDataAll()
    {
        List<RewardCategoryModel> weaponCategoryList = new();
        getQuery = "select * from reward_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            RewardCategoryModel rewardCategoryModel = new();
            rewardCategoryModel.reward_category = int.Parse(dr["reward_category"].ToString());
            rewardCategoryModel.reward_category_name = dr["category_name"].ToString();
            weaponCategoryList.Add(rewardCategoryModel);
        }
        return weaponCategoryList.ToArray();
    }

    // �w�肳�ꂽ����J�e�S���[�̃f�[�^�̂ݎ擾
    public static RewardCategoryModel GetRewardCategoryData(int reward_category)
    {
        RewardCategoryModel rewardCategoryModel = new();
        getQuery = string.Format("select * from reward_categories where reward_category = {0}", reward_category);

        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            rewardCategoryModel.reward_category = int.Parse(dr["reward_category"].ToString());
            rewardCategoryModel.reward_category_name = dr["category_name"].ToString();
        }
        return rewardCategoryModel;
    }
}