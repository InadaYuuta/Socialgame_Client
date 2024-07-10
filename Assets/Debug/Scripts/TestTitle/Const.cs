#define LOCAL_SERVER

using UnityEngine;


namespace GameUtil
{
    public class Const : MonoBehaviour
    {
#if LOCAL_SERVER
        /** DB��� */
        public const string REGISTRATION_URL = "http://localhost/api/register";                 // �o�^URL
        public const string LOGIN_URL = "http://localhost/api/login";                           // ���O�C��URL
        public const string HOME_URL = "http://localhost/api/home";                             // �z�[�����X�VURL
        public const string MASTER_CHECK_URL = "http://localhost/api/masterCheck";              // �}�X�^�[�f�[�^�`�F�b�NURL
        public const string MASTER_GET_URL = "http://localhost/api/masterGet";                  // �}�X�^�[�f�[�^�擾URL
        public const string BUY_CURRENCY_URL = "http://localhost/api/buyCurrency";              // �ʉݍw��URL
        public const string STAMINA_RECOVERY_URL = "http://localhost/api/staminaRecovery";      // �X�^�~�i��URL
        public const string STAMINA_CONSUMPTION = "http://localhost/api/testConsumption";       // �X�^�~�i����URL(��)
        public const string ITEM_REGISTRATION_URL = "http://localhost/api/itemRegist";          // �A�C�e���̓o�^URL
        public const string BUY_EXCHANGE_SHOP_URL = "http://localhost/api/exchangeShop";        // �����V���b�v�ł̃A�C�e���w��URL
        public const string GACHA_URL = "http://localhost/api/gachaExecute";                    // �K�`��URL
        public const string GET_GACHA_LOG = "http://localhost/api/getGachaLog";                 // �K�`�����O����
        public const string WEAPON_LEVEL_UP_URL = "http://localhost/api/levelUp";               // ���틭��
        public const string WEAPON_LIMIT_BREAK_URL = "http://localhost/api/limitBreak";         // ������E�˔j
        public const string WEAPON_EVOLUTION_URL = "http://localhost/api/evolution";            // ����i��
        public const string GET_PRESENT_BOX_URL = "http://localhost/api/getPresentBox";         // �v���[���g�{�b�N�X�f�[�^�擾
        public const string RECEIVE_PRESENT_BOX_URL = "http://localhost/api/receivePresent";    // �v���[���g���
#elif DEVELOP_SERVER
        /** DB��� */
        public const string REGISTRATION_URL = "http://localhost/api/register";                // �o�^URL
        public const string LOGIN_URL = "http://localhost/api/login";                          // ���O�C��URL
        public const string HOME_URL = "http://localhost/api/home";                            // �z�[�����X�VURL
        public const string MASTER_CHECK_URL = "http://localhost/api/masterCheck";             // �}�X�^�[�f�[�^�`�F�b�NURL
        public const string MASTER_GET_URL = "http://localhost/api/masterGet";                 // �}�X�^�[�f�[�^�擾URL
        public const string BUY_CURRENCY_URL = "http://localhost/api/buyCurrency";             // �ʉݍw��URL
        public const string STAMINA_RECOVERY_URL = "http://localhost/api/staminaRecovery";     // �X�^�~�i��URL
        public const string STAMINA_CONSUMPTION = "http://localhost/api/testConsumption";      // �X�^�~�i����URL(��)
        public const string ITEM_REGISTRATION_URL = "http://localhost/api/itemRegist";         // �A�C�e���̓o�^URL
        public const string ITEM_UPDATE_URL = "http://localhost/api/itemUpdate";               // �A�C�e���̍X�VURL
        public const string BUY_EXCHANGE_SHOP_URL = "http://localhost/api/exchangeShop";       // �����V���b�v�ł̃A�C�e���w��URL
        public const string GACHA_URL = "http://localhost/api/gachaExcute";                    // �K�`��URL
        public const string GET_GACHA_LOG = "http://localhost/api/getGachaLog";                // �K�`�����O����
        public const string WEAPON_LEVEL_UP_URL = "http://localhost/api/levelUp";              // ���틭��
        public const string WEAPON_LIMIT_BREAK_URL = "http://localhost/api/limitBreak";        // ������E�˔j
        public const string WEAPON_EVOLUTION_URL = "http://localhost/api/evolution";           // ����i��
        public const string GET_PRESENT_BOX_URL = "http://localhost/api/getPresentBox";        // �v���[���g�{�b�N�X�f�[�^�擾
        public const string RECEIVE_PRESENT_BOX_URL = "http://localhost/api/receivePresent";   // �v���[���g���
#endif


        public const string SQLITE_FILE_NAME = "SQLiteFile.db";

        /** �G���[ID */
        public const string ERROR_DB_UPDATE = "1";
        public const string ERROR_MASTER_DATA_UPDATE = "2";
    }
}

