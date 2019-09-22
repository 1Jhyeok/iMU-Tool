using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace iMU_Tool
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.RunWorkerAsync();
        }

        // Worker Thread가 실제 하는 일
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /* 메인 폼 실행 전 처리할 것들 */

            // ImageSearch DLL 로드
            File.WriteAllBytes(Program.TEMP_DIRECTORY + "ImageSearch.dll", Properties.Resources.ImageSearch);

            //Update.bat 생성
            StreamWriter sw = new StreamWriter(Program.TEMP_DIRECTORY + "\\Update.bat");
            sw.WriteLine("@echo off");
            sw.WriteLine();
            sw.WriteLine("rem AppStart");
            sw.WriteLine("rem AppUpdate");
            sw.WriteLine();
            sw.WriteLine(@"set AppStart=" + Application.StartupPath + @"\iMU Tool.exe");
            sw.WriteLine(@"set AppUpdate=" + Program.TEMP_DIRECTORY + @"Update.exe");
            sw.WriteLine();
            sw.WriteLine("taskkill /F /IM \"iMU Tool.exe\"");
            sw.WriteLine();
            sw.WriteLine(":loop");
            sw.WriteLine("tasklist /FI \"IMAGENAME eq iMU Tool.exe\" 2>NUL | find /I /N \"iMU Tool.exe\">NUL");
            sw.WriteLine("if \" % ERRORLEVEL % \"==\"0\" (");
            sw.WriteLine("echo running...");
            sw.WriteLine("goto :loop");
            sw.WriteLine(")");
            sw.WriteLine();
            sw.WriteLine("del %AppStart%");
            sw.WriteLine("move %AppUpdate% %AppStart%");
            sw.WriteLine("echo 업데이트가 완료되었습니다. 이 창은 닫으셔도 됩니다.");
            sw.WriteLine("%AppStart%");
            sw.WriteLine("exit");
            sw.Close();

            // 계정 정보 로드
            try
            {
                WebClient webClient = new WebClient();

                webClient.DownloadFile("http://halion.ipdisk.co.kr:8000/list/HDD1/Accounts.imu", Program.CRYPTED_ACCOUNTS);
                Crypto.DecryptFile(Program.CRYPTED_ACCOUNTS, Program.ACCOUNTS_FILEPATH);
                Program.overwatchAccounts = new AccountGroup(Program.ACCOUNTS_FILEPATH, 8);    // 계정 인스턴스 초기화
            }

            catch
            {
                MessageBox.Show("계정 정보를 다운로드 하는데 실패했습니다.\n\r인터넷 연결 혹은 서버에 파일이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.overwatchAccounts = new AccountGroup(8);    // 빈 계정 인스턴스 생성
            }

        }

        // 작업 완료 - UI Thread
        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // 에러가 있는지 체크
            if (e.Error != null)
            {
                lblMsg.Text = e.Error.Message;
                MessageBox.Show(e.Error.Message, "Error");
                return;
            }
            Close();
        }
    }
}

