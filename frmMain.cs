using MetroFramework.Controls;
using MetroFramework.Forms;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace iMU_Tool
{
    public partial class frmMain : MetroForm
    {
        #region DLL File Import
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string SClassName, string SWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr findname);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr findname, int howShow);
        private const int showNORMAL = 1;
        private const int showMINIMIZED = 2;
        private const int showMAXIMIZED = 3;

        [DllImport("ImageSearch.dll")]
        private static extern IntPtr ImageSearch(int x, int y, int right, int bottom, [MarshalAs(UnmanagedType.LPStr)]string imagePath);
        #endregion

        string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string startDir = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\Programs\\iMU Tool.url";

        private MetroButton[] bnetButtons;      // 버튼들 배열
        private MetroButton[] editButtons;      // 버튼들 배열

        public frmMain()
        {
            InitializeComponent();
            tabControl1.TabIndex = 0;

            // 필드 초기화
            bnetButtons = new MetroButton[] { btn_Bnet1, btn_Bnet2, btn_Bnet3, btn_Bnet4, btn_Bnet5, btn_Bnet6, btn_Bnet7, btn_Bnet8 };
            editButtons = new MetroButton[] { editButton1, editButton2, editButton3, editButton4, editButton5, editButton6, editButton7, editButton8 };

            // 이벤트
            this.Load += Form_Main_Load;
            btn_RunBnet.Click += Btn_RunBnet_Click;
            btn_Update.Click += Btn_Update_Click;
            btn_PUBG_Backup.Click += Btn_PUBG_Backup_Click;
            btn_PUBG_Restore.Click += Btn_PUBG_Restore_Click;
            for (int i = 0; i < bnetButtons.Length; i++)
            {
                bnetButtons[i].Click += BnetLogin;
            }
            for (int i = 0; i < editButtons.Length; i++)
            {
                editButtons[i].Click += BnetEdit;
            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

            string strMakelnk = Program.APPDATA + "Makelnk"; //AppData영역의 Makelnk
            FileInfo fi = new FileInfo(startDir);

            if (!fi.Exists)
            {
                appShortcutToDesktop("iMU Tool");
                fi = new FileInfo(deskDir + "\\iMU Tool.url");
                fi.MoveTo(startDir);

            }

            fi = new FileInfo(strMakelnk);

            if (!fi.Exists)
            {
                fi = new FileInfo(deskDir + "\\iMU Tool.url");
                if (!fi.Exists)
                {
                    DialogResult result;
                    result = MessageBox.Show("바탕화면에 바로 가기를 생성하시겠습니까?", "바로가기", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        appShortcutToDesktop("iMU Tool");
                    }
                    else
                    {
                        result = MessageBox.Show("다음번에도 물어볼까요?", "바로가기", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                            File.Create(strMakelnk);
                    }
                }
            }
            LoadOverwatchAccounts();
        }

        private void appShortcutToDesktop(string linkName)
        {

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }
        }

        /// <summary>
        /// 오버워치 계정 정보를 폼에 적용합니다.
        /// </summary>
        public void LoadOverwatchAccounts()
        {
            for (int i = 0; i < bnetButtons.Length; i++)
            {
                bnetButtons[i].Text = Program.overwatchAccounts.GetAccount(i).Name;
                bnetButtons[i].TabIndex = i + 1;    // 버튼 구별할 때 이용할 것 (1번 버튼 ~ 8번 버튼)
            }
            for (int i = 0; i < editButtons.Length; i++)
            {
                editButtons[i].Text = Program.overwatchAccounts.GetAccount(i).Name;
                editButtons[i].TabIndex = i + 1;    // 버튼 구별할 때 이용할 것 (1번 버튼 ~ 8번 버튼)
            }
        }

        /// <summary>
        /// 이미지 파일 주소를 통해 이미지 서치를 진행합니다.
        /// </summary>
        public String[] UseImageSearch(string imgPath)
        {
            int right = Screen.PrimaryScreen.WorkingArea.Right;
            int bottom = Screen.PrimaryScreen.WorkingArea.Bottom;

            // 이미지 서치 시작
            Directory.SetCurrentDirectory(Program.TEMP_DIRECTORY);   // dll 파일 위치 지정

            IntPtr result = ImageSearch(0, 0, right, bottom, imgPath);
            String res = Marshal.PtrToStringAnsi(result);

            // 이미지 서치 결과값 : 인덱스 0 -> 성공1, 실패0 / 인덱스 1, 2번 -> x, y / 인덱스 3, 4 -> 이미지의 세로가로길이
            if (res[0] == '0')  // 이미지가 존재하지 않는가?
                return null;

            return res.Split('|');
        }

        /// <summary>
        /// 비트맵 인스턴스를 통해 이미지 서치를 진행합니다.
        /// </summary>
        public String[] UseImageSearch(Bitmap bitmap)
        {
            // 리소스에 있는 이미지 파일을 저장할 임시 폴더 지정 후 이미지 파일 저장
            string tempPath = Program.TEMP_DIRECTORY + @"image_search.png";
            bitmap.Save(tempPath, ImageFormat.Png);

            return UseImageSearch(tempPath);
        }

        private void Btn_IniEdit_Click(object sender, EventArgs e)
        {
            try
            {
                frmEdit frm = new frmEdit(this, 0);
                frm.Show();
            }
            catch
            {
                MessageBox.Show("계정 파일이 없는 것 같습니다!!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_PUBG_Backup_Click(object sender, EventArgs e)
        {
            // 설정 파일 업로드
            if (Program.ftpServer.UploadFile(Program.PUBG_SETTINGS_FILEPATH))
            {
                MessageBox.Show("설정파일 업로드 완료", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("설정파일 업로드 실패", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_PUBG_Restore_Click(object sender, EventArgs e)
        {
            // 설정 파일 다운로드
            try
            {
                WebClient webClient = new WebClient();

                webClient.DownloadFile("http://halion.ipdisk.co.kr:8000/list/HDD1/GameUserSettings.ini", Program.PUBG_SETTINGS_FILEPATH);
                MessageBox.Show("설정파일 교체 성공", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("설정파일 교체 실패", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            frmUpdater updater = new frmUpdater();
            updater.ShowDialog();
        }

        private void Btn_RunBnet_Click(object sender, EventArgs e) // 배틀넷 레지스트리로 켜기
        {
            RegistryKey reg = Registry.LocalMachine;
            reg = reg.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Battle.net", true);
            if (reg != null)
            {
                Object val = reg.GetValue("DisplayIcon");
                if (null != val)
                {
                    Process.Start(Convert.ToString(val));
                }
            }
        }

        private void MLinkVersion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("갓성호\n재혁", "Code");
        }

        private void BnetEdit(object sender, EventArgs e)
        {
            MetroButton btn = (MetroButton)sender;
            int Btn_index = btn.TabIndex - 1;
            try
            {
                frmEdit frm = new frmEdit(this, Btn_index);
                frm.Show();
            }
            catch
            {
                MessageBox.Show("계정 파일이 없는 것 같습니다!!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BnetLogin(object sender, EventArgs e)
        {
            Account account = Program.overwatchAccounts.GetAccount(((MetroButton)sender).TabIndex - 1);

            string Bnet_ID = account.ID;
            string Bnet_PW = account.PW;

            // 프로그램명으로 핸들을 찾음
            IntPtr findname = FindWindow(null, "블리자드 Battle.net 로그인");
            if (!findname.Equals(IntPtr.Zero))
            {
                // 프로그램이 최소화 되어 있다면 활성화 시킴 
                ShowWindowAsync(findname, showNORMAL);
                // 윈도우에 포커스를 줘서 최상위로 만듬
                SetForegroundWindow(findname);
            }
            
            else//Blizzard Battle.net Login
            {
                findname = FindWindow(null, "Blizzard Battle.net Login");
                if (!findname.Equals(IntPtr.Zero))
                {
                    // 프로그램이 최소화 되어 있다면 활성화 시킴 
                    ShowWindowAsync(findname, showNORMAL);
                    // 윈도우에 포커스를 줘서 최상위로 만듬
                    SetForegroundWindow(findname);
                }
                else
                {
                    MessageBox.Show("블리자드 Battle.net이 켜져있지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 이미지 서치 및 로그인 처리
            string[] search = UseImageSearch(Properties.Resources.focusID);
            if (search == null)
            {
                if (!findname.Equals(IntPtr.Zero))
                {
                    // 프로그램이 최소화 되어 있다면 활성화 시킴 
                    ShowWindowAsync(findname, showNORMAL);
                    // 윈도우에 포커스를 줘서 최상위로 만듬
                    SetForegroundWindow(findname);
                }
                SendKeys.Send("+{TAB}");
                SendKeys.Send("^{a}");
                SendKeys.Send(Bnet_ID);
                SendKeys.Send("{TAB}");
                SendKeys.Send(Bnet_PW);
                SendKeys.Send("{ENTER}");
            }

            search = UseImageSearch(Properties.Resources.focusPW);
            if (search == null)
            {
                SendKeys.Send("^{a}");
                SendKeys.Send(Bnet_ID);
                SendKeys.Send("{TAB}");
                SendKeys.Send(Bnet_PW);
                SendKeys.Send("{ENTER}");
            }
        }

        private void Btn_overwatch_restore_Click(object sender, EventArgs e)
        {
            // 설정 파일 다운로드
            try
            {
                WebClient webClient = new WebClient();

                webClient.DownloadFile("http://overwatch.imutool.kro.kr", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Overwatch\\Settings\\Settings_v0.ini");
                MessageBox.Show("설정파일 교체 성공", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("설정파일 교체 실패", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            Location = Properties.Settings.Default.Location;

            button1.Image = resizeImage(Properties.Resources.icon_key, button1.Size.Width / 2);
            button2.Image = resizeImage(Properties.Resources.icon_edit, button2.Size.Width / 2);
            button3.Image = resizeImage(Properties.Resources.icon_etc, button3.Size.Width / 2);
            string strVer = " Build. " + Properties.Resources.BuildDate;
            label2.Text = strVer;
        }

        private Bitmap resizeImage(Bitmap sourceImg, int size)
        {
            Size resize = new Size(size, size);
            Bitmap resized = new Bitmap(sourceImg, resize);
            return resized;
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            button1.BackColor = Color.FromArgb(47, 47, 47);
            button2.BackColor = Color.FromArgb(17, 17, 17);
            button3.BackColor = Color.FromArgb(17, 17, 17);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            button2.BackColor = Color.FromArgb(47, 47, 47);
            button1.BackColor = Color.FromArgb(17, 17, 17);
            button3.BackColor = Color.FromArgb(17, 17, 17);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            button3.BackColor = Color.FromArgb(47, 47, 47);
            button2.BackColor = Color.FromArgb(17, 17, 17);
            button1.BackColor = Color.FromArgb(17, 17, 17);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Save();
        }
    }
}