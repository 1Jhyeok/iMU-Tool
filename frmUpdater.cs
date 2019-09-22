using MetroFramework.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace iMU_Tool
{
    public partial class frmUpdater : MetroForm
    {
        WebClient webClient;               // 다운용 웹클라
        Stopwatch sw = new Stopwatch();

        public frmUpdater()
        {
            InitializeComponent();
            DownloadFile("update.imutool.kro.kr", Program.TEMP_DIRECTORY + "\\Update.exe");
        }

        public void DownloadFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // http:// 번거로우니까
                Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);

                // 다운속도 측정용 스탑워치
                sw.Start();

                try
                {
                    // 파일 다운로드 시작
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // 프로그래스 바 및 텍스트 변경
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            labelSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));

            progressBar.Value = e.ProgressPercentage;

            //labelPerc.Text = e.ProgressPercentage.ToString() + "%";//퍼센트 좀 이상함 20 언저리에서 100됨;
        }

        // 다운로드 후 실행
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();

            System.Diagnostics.Process.Start(Program.TEMP_DIRECTORY + "Update.bat");
        }
    }
}
