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
        createQuery = "create table if not exists wallets(free_amount mediumint,paid_amount mediumint,max_amount mediumint)";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(WalletsModel walles)
    {
        setQuery = "insert or replace into wallets(free_amount,paid_amount,max_amount) values(," + walles.free_amount + "," + walles.paid_amount + "," + walles.max_amount + ")";
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
            walletsModel.free_amount = int.Parse(dr[""].ToString());
            walletsModel.paid_amount = int.Parse(dr[""].ToString());
            walletsModel.max_amount = int.Parse(dr[""].ToString());
        }
        return walletsModel;
    }
}
