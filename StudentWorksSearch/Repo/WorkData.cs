using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWorksSearch.Repo
{
    class WorkData
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string auth;
        public string Auth
        {
            get { return auth; }
            set { auth = value; }
        }

        private string dis;
        public string Dis
        {
            get { return dis; }
            set { dis = value; }
        }

        private string descript;
        public string Descript
        {
            get { return descript; }
            set { descript = value; }
        }

        private int fileid;
        public int Fileid
        {
            get { return fileid; }
            set { fileid = value; }
        }

        private string filepath;
        public string Filepath
        {
            get { return filepath; }
            set { filepath = value; }
        }

        private double? plag;
        public double? Plag
        {
            get { return plag; }
            set { plag = value; }
        }


        public WorkData(string name, string auth, string dis, string descript, int fileid, double? plag, string filepath)
        {
            this.name = name;
            this.auth = auth;
            this.dis = dis;
            this.descript = descript;
            this.fileid = fileid;
            this.plag = plag;
            this.filepath = filepath;
        }
    }
}
