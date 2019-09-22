using System;
using System.IO;
using System.Net;

namespace iMU_Tool
{
    class FTPTool
    {
        string ftpPath;
        string id, pw;

        public FTPTool(string ftp, string _id, string _pw)
        {
            ftpPath = ftp;
            id = _id;
            pw = _pw;
        }

        /// <summary>
        /// ftp에 올려져 있는 fileName을 목적지에 옮깁니다. (목적지는 파일 이름 포함)
        /// </summary>
        public bool DownloadFile(string fileName, string destination)
        {
            bool result = true;

            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Credentials = new NetworkCredential(id, pw);
                    wc.DownloadFile(ftpPath + fileName, destination);
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 로컬 파일 주소만 입력하면 알아서 ftp 서버에 업로드 할 파일 이름으로 업로드 됩니다.
        /// </summary>
        public bool UploadFile(string filePath)
        {
            bool result = true;

            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Credentials = new NetworkCredential(id, pw);
                    wc.UploadFile(ftpPath + Path.GetFileName(filePath), filePath);
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
