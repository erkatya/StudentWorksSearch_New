using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using StudentWorksSearch.Repo;

namespace StudentWorksSearch.Engines
{
    public class DBEngine
    {
        SearchWorkEntities db = new SearchWorkEntities();

        public DBEngine()
        {
        }

        public List<string> GetUni()
        {
                var query =
                      from UNI in db.University
                      select UNI.Short_name;

                List<string> UNIList = new List<string>();
                return UNIList = query.Select(a => a).ToList();
        }

        public List<string> GetDis()
        {
            var query =
                      from DIS in db.Discipline
                      select DIS.Name;

            List<string> DISList = new List<string>();
            return DISList = query.Select(a => a).ToList();
        }

        public void GetWorkInfo(string selection)
        {
            string[] splt;
            splt = selection.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            int id = Convert.ToInt32(splt[0]);

            var query =
                      from W in db.Work
                      where W.Id == id
                      select new { W.Name, W.Authors, W.Discipline, W.Description, W.Files, W.Plagiarism };

            foreach (var work in query)
            {
                Repository.Work = new WorkData(work.Name, work.Authors, work.Discipline.Name, work.Description, work.Files.Id, work.Plagiarism, work.Files.Path);
            }
        }
        
        public bool CheckWorks()
        {
            var query =
                      from W in db.Work
                      select W.Id;

            List<int> WList = new List<int>();
            WList = query.Select(a => a).ToList();
            if (WList.Count == 0)
                return false;
            else
                return true;
        }

    }
}
