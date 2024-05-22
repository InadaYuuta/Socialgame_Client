using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class LogCategoryModel
{
    public int log_category;     // ���O�̃J�e�S���[
    public string category_name; // �J�e�S���[�̖��O
}

public class LogCategories : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists log_categories (log_category tinyint, category_name varchar,primary key(log_category))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(LogCategoryModel[] log_category_model_list)
    {
        foreach (LogCategoryModel logCategoryModel in log_category_model_list)
        {
            setQuery = "insert or replace into log_categories (log_category,category_name) values(" + logCategoryModel.log_category + ", \"" + logCategoryModel.category_name + "\") ";
            RunQuery(setQuery);
        }
    }

    // �S�ẴJ�e�S���[���擾
    public static LogCategoryModel[] GetLogCategoryAll()
    {
        List<LogCategoryModel> log_category_model_list = new();
        getQuery = "select * from log_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            LogCategoryModel logCategoryModel = new();
            logCategoryModel.log_category = int.Parse(dr["log_category"].ToString());
            logCategoryModel.category_name = dr["category_name"].ToString();
            log_category_model_list.Add(logCategoryModel);
        }
        return log_category_model_list.ToArray(); // List��z��ɕϊ����ĕԂ�
    }

    // �w�肵���J�e�S���[�������擾
    public static LogCategoryModel GetLogCategory(string log_category)
    {
        LogCategoryModel logCategoryModel = new();
        getQuery = "select * from log_categories where log_category" + log_category;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            logCategoryModel.log_category = int.Parse(dr["log_category"].ToString());
            logCategoryModel.category_name = dr["category_name"].ToString();
        }
        return logCategoryModel;
    }
}