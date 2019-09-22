using System.Xml;

namespace iMU_Tool
{
    public class AccountGroup
    {
        public Account[] accounts;

        /// <summary>
        /// index 수 만큼 빈 Account 계정들을 만듭니다.
        /// </summary>
        public AccountGroup(int index)
        {
            accounts = new Account[index];

            for (int i = 0; i < index; i++)
                accounts[i] = new Account();
        }

        /// <summary>
        /// XML로 제작된 계정 문서에서 index수 만큼 계정을 불러옵니다.
        /// </summary>
        public AccountGroup(string xmlPath, int index)
        {
            // xml 불러오기
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            // Account 인스턴스 모두 초기화
            accounts = new Account[index];
            XmlNodeList accountNodeList = doc["root"].ChildNodes;
            for (int i = 0; i < index; i++)
            {
                accounts[i] = new Account(accountNodeList[i].Attributes["name"].Value,
                    accountNodeList[i]["id"].InnerText,
                    accountNodeList[i]["pw"].InnerText);
            }
        }

        public void SetAccount(int index, string name, string id, string pw)
        {
            accounts[index].Name = name;
            accounts[index].ID = id;
            accounts[index].PW = pw;
        }

        public Account GetAccount(int index)
        {
            return accounts[index];
        }

        public void SaveAccountsToXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");

            XmlElement account, id, pw;
            for (int i = 0; i < 8; i++)
            {
                account = doc.CreateElement("account");
                account.SetAttribute("name", accounts[i].Name);

                id = doc.CreateElement("id");
                id.InnerText = accounts[i].ID;

                pw = doc.CreateElement("pw");
                pw.InnerText = accounts[i].PW;

                account.AppendChild(id);
                account.AppendChild(pw);
                root.AppendChild(account);
            }

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            doc.AppendChild(root);
            doc.Save(path);
        }
    }
}
