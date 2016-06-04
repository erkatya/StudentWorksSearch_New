using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentWorksSearch.Repo;

namespace StudentWorksSearch
{
    class Repository
    {
        private static UserData user;
        public static UserData User
        {
            get { return user; }
            set { user = value; }
        }

        private static bool edit; //false - регистрация, true - редактирование
        public static bool Edit
        {
            get { return edit; }
            set { edit = value; }
        }

        private static string path; 
        public static string Path
        {
            get { return path; }
            set { path = value; }
        }

        private static double size;
        public static double Size
        {
            get { return size; }
            set { size = value; }
        }

        private static string uid;
        public static string UID
        {
            get { return uid; }
            set { uid = value; }
        }

        private static WorkData work;
        public static WorkData Work
        {
            get { return work; }
            set { work = value; }
        }

    }
}
