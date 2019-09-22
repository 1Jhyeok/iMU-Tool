using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace iMU_Tool
{
    static class Program
    {
        public const string PROGRAM_VERSION = "7.7";

        // FTP 관련 상수들 (필요에 따라 바꿔주기만 하면됨)
        public const string FTP_SERVER_PATH = @"ftp://halion.ipdisk.co.kr/HDD1/";
        public const string FTP_SERVER_ID = "imutool";
        public const string FTP_SERVER_PW = "0208";

        // 파일 이름 관련 상수들
        public static readonly string APPDATA; // "%Appdata%/imu tool/" 을 가르킴
        public static readonly string TEMP_DIRECTORY;
        public static readonly string ACCOUNTS_FILEPATH;
        public static readonly string PUBG_SETTINGS_FILEPATH;
        public static readonly string CRYPTED_ACCOUNTS;
        private const string sSecretKey = "imutool-account-password";

        public static AccountGroup overwatchAccounts;
        public static FTPTool ftpServer;

        static Program()
        {
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, "iMU Tool", out bool isNew);

            if (isNew == false)
            {
                MessageBox.Show("이미 실행 중 입니다. \n 오류방지를 위해 중복실행이 불가합니다.");
                Application.Exit();
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                // 실행
                mutex.ReleaseMutex();
            }

            // 임시 폴더 만들고(없으면) 그 후 경로를 변수에 저장
            TEMP_DIRECTORY = Path.GetTempPath() + @"iMU Tool\";
            APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\iMU Tool\\";

            if (!Directory.Exists(TEMP_DIRECTORY))
                Directory.CreateDirectory(TEMP_DIRECTORY);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\iMU Tool"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\iMU Tool");

            // 오버워치 계정 정보 파일 경로
            ACCOUNTS_FILEPATH = TEMP_DIRECTORY + "Accounts.xml";
            CRYPTED_ACCOUNTS = APPDATA + "Accounts.imu";

            // 배그 설정 파일 경로
            PUBG_SETTINGS_FILEPATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                @"\TslGame\Saved\Config\WindowsNoEditor\GameUserSettings.ini";

            // FTP 서버 초기화
            ftpServer = new FTPTool(FTP_SERVER_PATH, FTP_SERVER_ID, FTP_SERVER_PW);

        }

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSplash());

            Application.Run(new frmMain());

            /* 폼 종료 후 처리 할 것들*/

            // 임시 폴더 지우기 
            Directory.Delete(Program.TEMP_DIRECTORY, true);
        }
    }
}
