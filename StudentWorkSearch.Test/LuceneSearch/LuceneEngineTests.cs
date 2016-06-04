using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentWorksSearch.LuceneSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneEngine.Tests
{
    [TestClass()]
    public class LuceneEngineTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BuildIndex_nullAuthor_NullReferenceException()
        {
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            ls.BuildIndex(
                new StudentWorksSearch.LuceneSearch.FileToIndex
                {
                    Id = 0,
                    Authors = null,
                    Description = "test",
                    Hashtags = "test",
                    Text = "test",
                    Title = "test",
                    Discipline = "test"
                });
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BuildIndex_nullDescription_NullReferenceException()
        {
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            ls.BuildIndex(new FileToIndex
            {
                Id = 0,
                Authors = "test",
                Description = null,
                Hashtags = "test",
                Text = "test",
                Title = "test",
                Discipline = "test"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BuildIndex_nullHashtags_NullReferenceException()
        {
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            ls.BuildIndex(new FileToIndex
            {
                Id = 0,
                Authors = "test",
                Description = "test",
                Hashtags = null,
                Text = "test",
                Title = "test",
                Discipline = "test"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BuildIndex_nullText_NullReferenceException()
        {
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            ls.BuildIndex(new FileToIndex
            {
                Id = 0,
                Authors = "test",
                Description = "test",
                Hashtags = "test",
                Text = null,
                Title = "test",
                Discipline = "test"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BuildIndex_nullTitle_NullReferenceException()
        {
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            ls.BuildIndex(new FileToIndex
            {
                Id = 0,
                Authors = "test",
                Description = "test",
                Hashtags = "test",
                Text = "test",
                Title = null,
                Discipline = "test"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BuildIndex_nullDiscipline_NullReferenceException()
        {
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            ls.BuildIndex(new FileToIndex
            {
                Id = 0,
                Authors = "test",
                Description = "test",
                Hashtags = "test",
                Text = "test",
                Title = "test",
                Discipline = null
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BuildIndex_nullObject_NullReferenceException()
        {
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            ls.BuildIndex(new StudentWorksSearch.LuceneSearch.FileToIndex());
        }

        [TestMethod]
        public void BuildIndex_objectPassed_NumberOfDocsIncreased()
        {
            //1
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            var file= new FileToIndex
            {
                Id = 0,
                Authors = "test",
                Description = "test",
                Hashtags = "test",
                Text = "test",
                Title = null,
                Discipline = "test"
            };
            var was = ls.CountDocs();

            //2
            ls.BuildIndex(file);
            var now = ls.CountDocs();

            //3
            Assert.AreEqual(was+1,now);
        }

        [TestMethod]
        public void Search_nullPassedToFind_EmptyListReturned()
        {
            //1
            var ls = new StudentWorksSearch.LuceneSearch.LuceneEngine();
            int inList;
            //2
            var returned= ls.Search(null, out inList);

            //3
            Assert.AreEqual(0, returned.Count<FileToIndex>());
        }
    }
}