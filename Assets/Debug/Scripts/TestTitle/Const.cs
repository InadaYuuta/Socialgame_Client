using UnityEngine;

namespace GameUtil
{
    public class Const : MonoBehaviour
    {
        /** DB��� */
        public const string RESISTRATION_URL = "http://localhost/api/register";             // �o�^URL
        public const string LOGIN_URL = "http://localhost/api/login";                       // ���O�C��URL
        public const string MASTER_CHECK_URL = "http://localhost/api/masterCheck";          // �}�X�^�[�f�[�^�`�F�b�NURL
        public const string MASTER_GET_URL = "http://localhost/api/masterGet";              // �}�X�^�[�f�[�^�擾URL
        public const string BUY_CURRENCY_URL = "http://localhost/api/buyCurrency";          // �ʉݍw��URL
        public const string STAMINA_RECOVERY_URL = "http://localhost/api/staminaRecovery";  // �X�^�~�i��URL
        public const string STAMINA_CONSUMPTION = "http://localhost/api/testConsumption";   // �X�^�~�i����URL(��)

        public const string SQLITE_FILE_NAME = "SQLiteFile.db";

        /** �G���[ID */
        public const string ERROR_DB_UPDATE = "1";
        public const string ERROR_MASTER_DATA_UPDATE = "2";
    }
}

