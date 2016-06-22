using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Schemes
{
    static public class LuceneRepository
    {
        static public void BuildIndex()
        {
            List<ViewPost> posts = Repository.GetaLLPosts();
            using (var directory = GetDirectory())
            using (var analyzer = GetAnalyzer())
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                writer.DeleteAll();
                foreach (var post in posts)
                {
                    var document = MapPost(post);
                    writer.AddDocument(document);
                }
            }
        }

        static public Document MapPost(ViewPost post)
        {
            var document = new Document();
            document.Add(new NumericField("Id", Field.Store.YES, true).SetIntValue(post.post.id));
            document.Add(new Field("Title", post.post.title, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Teme", post.post.teme, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Tags", post.post.tags, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Description", post.post.description, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Time", post.post.time.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("UserEmail", post.UserEmail, Field.Store.YES, Field.Index.ANALYZED));
            return document;
        }

        static public Lucene.Net.Store.Directory GetDirectory()
        {
            return new SimpleFSDirectory(new DirectoryInfo(@"D:\SampleIndex"));
        }

        static public Analyzer GetAnalyzer()
        {
            return new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
        }

        static public Query GetQuery(string keywords)
        {
            using (var analyzer = GetAnalyzer())
            {
                var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Description", analyzer);

                var query = new BooleanQuery();
                var keywordsQuery = parser.Parse(keywords);
                return query; // +Name:ibanez -Brand:Fender Name:"electric guitar" 
            }
        }

        static public List<ViewPost> Search(string keywords, out int count)
        {
            using (var directory = GetDirectory())
            using (var searcher = new IndexSearcher(directory))
            {
                var query = GetQuery(keywords);
                
                var docs = searcher.Search(query,1000000);
                count = docs.TotalHits;

                var posts = new List<ViewPost>();
                foreach (var scoreDoc in docs.ScoreDocs)
                {
                    var doc = searcher.Doc(scoreDoc.Doc);
                    string time = doc.Get("Time");
                    DateTime t = Convert.ToDateTime(time);
                    var post = new ViewPost(new Post { id = int.Parse(doc.Get("Id")), title = doc.Get("Title"), teme = doc.Get("Teme"), tags = doc.Get("Tags"), description = doc.Get("Description"), time = t});
                    
                    posts.Add(post);
                }

                return posts;
            }
        }
    }
}