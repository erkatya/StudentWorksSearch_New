using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWorksSearch.Repo
{
    class UserData
    {
        private string login; 
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string mail;
        public string E_mail
        {
            get { return mail; }
            set { mail = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private DateTime? registration;
        public DateTime? Registration
        {
            get { return registration; }
            set { registration = value; }
        }

        private string university;
        public string University
        {
            get { return university; }
            set { university = value; }
        }

        public UserData(string login, string mail, string name, DateTime? registration, string university)
        {
            this.login = login;
            this.mail = mail;
            this.name = name;
            this.registration = registration;
            this.university = university;
        }
    }
}
