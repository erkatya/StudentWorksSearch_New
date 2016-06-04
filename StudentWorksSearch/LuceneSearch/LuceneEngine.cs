using System.IO;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;
using System;
using System.Collections.Generic;
using StudentWorksSearch.Engines;

namespace StudentWorksSearch.LuceneSearch
{
    public  class LuceneEngine
    {
        //разобраться с серчером, там что то про изменения в индексе


        //this part taken from http://www.codeproject.com/Articles/320219/Lucene-Net-ultra-fast-search-for-MVC-or-WebForms
        //START
          private const string _luceneDir = "../../Data/lucene_index1";
 //       private const string _luceneDir = "../../../Data/lucene_index";
        private   FSDirectory _directoryTemp;
        private    FSDirectory _directory
        {
            get
            {
                if (_directoryTemp == null) _directoryTemp = FSDirectory.Open(new
            DirectoryInfo(_luceneDir));
                if (IndexWriter.IsLocked(_directoryTemp)) IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }
        //END

        //this method creates document from an ObjectToIndex
        public   void BuildIndex(FileToIndex file)
        {
            using (var analyzer = new Lucene.Net.Analysis.Ru.RussianAnalyzer(Version.LUCENE_30))
            {
                using (IndexWriter idxw = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    //check if document exists, if true deletes existing
                    
                    var searchQuery = new TermQuery(new Term("Id", file.Id.ToString()));
                    idxw.DeleteDocuments(searchQuery);
                    //creation
                    Document doc = new Document();
                    doc.Add(new Field("Id", file.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));//аналайзер разбивает строки на слова
                    doc.Add(new Field("Title", file.Title, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Description", file.Description, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Authors", file.Authors, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Text", file.Text, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Hashtags", file.Hashtags, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Discipline", file.Discipline, Field.Store.YES, Field.Index.ANALYZED));
                    //write the document to the index
                    idxw.AddDocument(doc);                   
                    //optimize and close the writer
                    idxw.Commit();
                    
                    idxw.Optimize();
                   
                }
            }
        }

        public  IEnumerable<FileToIndex> Search(string input, out int count, string fieldName = "")
        {
            if (string.IsNullOrEmpty(input))
            {
                count = 0;
                return new List<FileToIndex>();
            }
            //trim- удаляет пробелы, * просто мешает мне лично жить(мешает стеммеры приходить к исходной морфологической форме)
            var terms = input.Replace("-", " ").Replace("*", "").Split(' ');
            for (int i = 0; i < terms.Count(); i++)
            {
                terms[i] = terms[i].Trim();
            }
            input = string.Join(" ", terms);
            return _search(input, out count, fieldName);
        }

        //partially taken from http://www.codeproject.com/Articles/320219/Lucene-Net-ultra-fast-search-for-MVC-or-WebForms
        //START
        private  IEnumerable<FileToIndex> _search(string keywords, out int count, string field = "")
        {
            if (string.IsNullOrEmpty(keywords.Replace("*", "").Replace("?", "")))
            {
                count = 0;
                return new List<FileToIndex>();
            }
            using (var searcher = new IndexSearcher(_directory))
            using (var analyzer = new Lucene.Net.Analysis.Ru.RussianAnalyzer(Version.LUCENE_30))
            {
                if (!string.IsNullOrEmpty(field))
                {
                    var parser = new QueryParser(Version.LUCENE_30, field, analyzer);
                    var queryForField = parseQuery(keywords, parser);
                    var docs = searcher.Search(queryForField, 100);
                    count = docs.TotalHits;
                    var samples = _convertDocs(docs.ScoreDocs, searcher);
                    searcher.Dispose();
                    return samples;
                }
                else
                {
                    var parser = new MultiFieldQueryParser
                        (Version.LUCENE_30, new[] { "Title", "Authors", "Description", "Text", "Discipline" }, analyzer);
                    var queryForField = parseQuery(keywords, parser);
                    var docs = searcher.Search(queryForField, null, 100, Sort.RELEVANCE);
                    count = docs.TotalHits;
                    var samples = _convertDocs(docs.ScoreDocs, searcher);
                    searcher.Dispose();
                    return samples;
                }
            }
        }
        

        private   Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                //make query with some symbols ignored
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }
        //END


        //converting
        private   FileToIndex _convertDoc(Document doc)
        {
            return new FileToIndex
            {
                Id = Convert.ToInt32(doc.Get("Id")),
                Authors = doc.Get("Authors"),
                Text = doc.Get("Text"),
                Title = doc.Get("Title"),
                Description=doc.Get("Description"),
                Hashtags=doc.Get("Hashtags")
            };
        }

        private  IEnumerable<FileToIndex> _convertDocs(IEnumerable<ScoreDoc> docs, IndexSearcher searcher)
        {
            var samples = new List<FileToIndex>();
            foreach (var doc in docs)
            {
                samples.Add(_convertDoc(searcher.Doc(doc.Doc)));
            }
            return samples;
        }

        private  IEnumerable<FileToIndex> _convertDocs(IEnumerable<Document> docs)
        {
            var samples = new List<FileToIndex>();
            foreach (var doc in docs)
            {
                samples.Add(_convertDoc(doc));
            }
            return samples;
        }

        ////deleting index
        //public  void DeleteIndex(int id)
        //{
        //    using (var analyzer = new Lucene.Net.Analysis.Ru.RussianAnalyzer(Version.LUCENE_30))
        //    using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
        //    {
        //        var searchQuery = new TermQuery(new Term("Id", id.ToString()));
        //        writer.DeleteDocuments(searchQuery);
        //        writer.Commit();
        //    }
        //}

        public int CountDocs()
        {
            var reader = IndexReader.Open(_directory, true);
            return reader.NumDocs();
        }

    }
}
