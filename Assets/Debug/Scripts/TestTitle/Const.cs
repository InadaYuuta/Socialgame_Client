using UnityEngine;

namespace GameUtil
{
    public class Const : MonoBehaviour
    {
        /** DB��� */
        public const string REGISTRATION_URL = "http://localhost/api/register";             // �o�^URL
        public const string LOGIN_URL = "http://localhost/api/login";                       // ���O�C��URL
        public const string MASTER_CHECK_URL = "http://localhost/api/masterCheck";          // �}�X�^�[�f�[�^�`�F�b�NURL
        public const string MASTER_GET_URL = "http://localhost/api/masterGet";              // �}�X�^�[�f�[�^�擾URL
        public const string BUY_CURRENCY_URL = "http://localhost/api/buyCurrency";          // �ʉݍw��URL
        public const string STAMINA_RECOVERY_URL = "http://localhost/api/staminaRecovery";  // �X�^�~�i��URL
        public const string STAMINA_CONSUMPTION = "http://localhost/api/testConsumption";   // �X�^�~�i����URL(��)
        public const string ITEM_REGISTRATION_URL = "http://localhost/api/itemRegist";      // �A�C�e���̓o�^URL
        public const string ITEM_UPDATE_URL = "http://localhost/api/itemUpdate";            // �A�C�e���̍X�VURL
        public const string BUY_EXCHANGE_SHOP_URL = "http://localhost/api/exchangeShop";    // �����V���b�v�ł̃A�C�e���w��URL


        public const string SQLITE_FILE_NAME = "SQLiteFile.db";

        /** �G���[ID */
        public const string ERROR_DB_UPDATE = "1";
        public const string ERROR_MASTER_DATA_UPDATE = "2";
    }
}
