using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace iMU_Tool
{
    public partial class frmEdit : MetroForm
    {
        frmMain frmMain;
        Account[] accounts;
        int index;

        public frmEdit(frmMain form, int sel)
        {
            index = sel;

            InitializeComponent();
            AcceptButton = Btn_IniSave;
            txtbox_Tag.Text = (Program.overwatchAccounts.GetAccount(sel).Name);
            txtbox_ID.Text = (Program.overwatchAccounts.GetAccount(sel).ID);
            txtbox_PW.Text = (Program.overwatchAccounts.GetAccount(sel).PW);

            // 필드 초기화
            try
            {
                frmMain = form;
                accounts = new Account[8];
                //backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
            }
            catch
            {
                MessageBox.Show("계정 파일이 없는 것 같습니다!!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
        }

        private void Btn_IniSave_Click(object sender, EventArgs e)
        {
            // AccountGroup에 속해있는 Account 인스턴스 속성 변경
            int sel = index;
            Program.overwatchAccounts.SetAccount(sel, txtbox_Tag.Text, txtbox_ID.Text, txtbox_PW.Text);

            // 메인 폼에 버튼 내용 반영
            frmMain.LoadOverwatchAccounts();

            // AccountGroup에서 제공하는 XML로 저장하는 기능 사용
            Program.overwatchAccounts.SaveAccountsToXML(Program.ACCOUNTS_FILEPATH);
            // 저장된 XML 암호화
            Crypto.EncryptFile(Program.ACCOUNTS_FILEPATH, Program.CRYPTED_ACCOUNTS);

            // 암호화한 파일을 다시 업로드
            if (!Program.ftpServer.UploadFile(Program.CRYPTED_ACCOUNTS))
            {
                MessageBox.Show("계정 정보를 업로드하는데 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtbox_ID.Text);
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtbox_PW.Text);
        }
    }
}
