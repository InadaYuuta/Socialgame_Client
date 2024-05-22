using System;

[Serializable]
public class WalletsModel
{
    public int free_amount; // �����ʉ�
    public int paid_amount; // �L���ʉ�
    public int max_amount;  // �ʉݏ������
}

public class Wallets : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists wallets(user_id varchar,free_amount mediumint,paid_amount mediumint,max_amount mediumint,primary key(user_id))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(WalletsModel walles, string user_id)
    {
        if (walles == null || user_id == null) { return; }
        setQuery = "insert or replace into wallets(user_id,free_amount,paid_amount,max_amount) values(\"" + user_id + "\"," + walles.free_amount + "," + walles.paid_amount + "," + walles.max_amount + ")";
        RunQuery(setQuery);
    }

    // ���R�[�h�擾����
    public static WalletsModel Get()
    {
        getQuery = "select * from wallets";
        DataTable dataTable = RunQuery(getQuery);
        WalletsModel walletsModel = new();
        foreach (DataRow dr in dataTable.Rows)
        {
            walletsModel.free_amount = int.Parse(dr["free_amount"].ToString());
            walletsModel.paid_amount = int.Parse(dr["paid_amount"].ToString());
            walletsModel.max_amount = int.Parse(dr["max_amount"].ToString());
        }
        return walletsModel;
    }
}
