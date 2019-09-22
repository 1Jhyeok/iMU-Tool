namespace iMU_Tool
{
    public class Account
    {
        private string name;
        private string id;
        private string pw;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string PW
        {
            get { return pw; }
            set { pw = value; }
        }

        public Account()
        {

        }

        public Account(string _name, string _id, string _pw)
        {
            name = _name;
            id = _id;
            pw = _pw;
        }
    }
}
