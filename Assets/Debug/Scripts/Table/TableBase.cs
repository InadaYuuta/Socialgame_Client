using System;
using UnityEngine;

public class TableBase : MonoBehaviour
{
    protected static string createQuery = "";
    protected static string setQuery = "";
    protected static string getQuery = "";

    // TODO:���\�b�h�Ƃ����ꊇ�Ōp������΂ł���悤�ɂ���
    //// �e�[�u���쐬
    //protected static void CreateTable()=>RunQuery(createQuery);

    //// ���R�[�h�o�^����
    //protected static void Set<T>(ref T models)
    //{
    //    RunQuery(setQuery);
    //}

    // ���R�[�h�擾����
    //protected static T Get<T>(Action<T> action) where T : TableBase, new()
    //{
    //    string query = getQuery;
    //    DataTable dataTable = RunQuery(query);

    //    var models = new T();
    //    foreach (DataRow dr in dataTable.Rows)
    //    {
    //        action(models);
    //    }
    //    return models;
    //}

    //// �����Ɏ擾�̏ڂ�������������
    //public virtual void GetModelParts<T>(ref T models)
    //{

    //}

    /// <summary>
    /// �N�G���𑖂点��z
    /// query�ɂ͑��点����sql������
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    protected static DataTable RunQuery(string query)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        return dataTable;
    }
}
