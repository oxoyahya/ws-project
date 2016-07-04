using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

// 6 22 work set 
namespace ef_test4
{
    class Movie
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public string MovieUrl { get; set; }
        public List<video> videos { get; set; }

    }
    class video
    {
        public int VideoID { get; set; }
        public string VideoTitle { get; set; }
        public string VideoUrl { get; set; }
        public List<data> datas { get; set; }
    }

    class data
    {
        public int dataID { get; set; }
        public string VideoData { get; set; }

        public string VideoDataSourceName { get; set; }
    }
    class Mecontext : DbContext
    {
        public Mecontext() : base(@"Data source=.\TEW_SQLEXPRESS ; Initial Catalog=w6222 ;Integrated security= true; ")
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<video> videos { get; set; }
        public DbSet<data> datas { get; set; }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Mecontext db = new Mecontext();




            for (int i = 10; i <= 96; i++)
            {
                string v_url = "http://www.sadecebelgesel.com/page/" + i;
                HtmlWeb v_htmlweb = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument v_doc = v_htmlweb.Load(v_url);
                HtmlNodeCollection v_nodes = v_doc.DocumentNode.SelectNodes("//*[@id='content']/div[1]/a[@href]");


                foreach (var v_node in v_nodes)
                {
                    HtmlAttribute v_getSeries = v_node.Attributes["href"];
                    HtmlAttribute v_getTitle = v_node.Attributes["title"];

                    Movie movie12 = new Movie() { MovieTitle = v_getTitle.Value, MovieUrl = v_getSeries.Value };


                    string v_urlEpisode = v_getSeries.Value;
                    HtmlWeb v_Episode = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument v_docEpisode = v_Episode.Load(v_urlEpisode);
                    HtmlNodeCollection v_nodesEpisode = v_docEpisode.DocumentNode.SelectNodes("//div[@id='tabbolum']/p/a[@href]");
                    // MessageBox.Show(Convert.ToString(v_nodesEpisode)+ v_getSeries.Value);
                    if (Convert.ToString(v_nodesEpisode) != "")
                    {
                        List<video> videoList = new List<video>();

                        foreach (var v_nodeEpisode in v_nodesEpisode)
                        {    //get video URL
                            HtmlAttribute v_getEpisode = v_nodeEpisode.Attributes["href"];
                            string v_urlVideoData = "http://www.sadecebelgesel.com/" + v_getEpisode.Value;
                            // Add videos data to database



                            var b = new video() { VideoTitle = v_nodeEpisode.ChildNodes[0].InnerHtml, VideoUrl = v_urlVideoData };
                            var a = db.videos.Add(b);
                            videoList.Add(a);
                            //


                            HtmlWeb v_htmlwebVideoData = new HtmlWeb();
                            HtmlAgilityPack.HtmlDocument v_docVideoData = v_htmlwebVideoData.Load(v_urlVideoData);
                            HtmlNodeCollection v_nodesVideoData = v_docVideoData.DocumentNode.SelectNodes("//body/div[@id='wrapper']/div[@class ='arkaplan']/div[@class= 'filmcercevea']/div[@id='movie']/div[@class='tab_container']/div[@class='tab_content']/iframe[@src]");
                            if (Convert.ToString(v_nodesVideoData) != "")
                            {

                                //2/get server names 

                                string[] vtest1 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                                int iii = 0;
                                HtmlNodeCollection dsn = v_docVideoData.DocumentNode.SelectNodes("//*[@id=\"movie\"]/ul/li");
                                foreach (var test12 in dsn)
                                {
                                    vtest1[iii] = test12.ChildNodes[0].InnerHtml;
                                    iii += 1;



                                }
                                iii = 0;

                                //2/
                                List<data> dataList = new List<data>();
                                foreach (var nodeVedioData in v_nodesVideoData)
                                {//getting video data of each episode
                                    HtmlAttribute v_VideoData = nodeVedioData.Attributes["src"];




                                    // Add data to database
                                    var c = new data() { VideoData = v_VideoData.Value, VideoDataSourceName = vtest1[iii] };
                                    iii += 1;

                                    var d = db.datas.Add(c);
                                    dataList.Add(d);
                                    //

                                    //12//

                                    // names.Items.Add(v_VideoData.Value);
                                }
                                b.datas = dataList;
                                db.videos.Add(b);
                            }
                        }
                        movie12.videos = videoList;
                        db.Movies.Add(movie12);

                    }
                    // for who's  not have an episode
                    else
                    {
                        string v_urlVideoData = v_getSeries.Value;
                        HtmlWeb v_htmlwebVideoData = new HtmlWeb();
                        HtmlAgilityPack.HtmlDocument v_docVideoData = v_htmlwebVideoData.Load(v_urlVideoData);
                        HtmlNodeCollection v_nodesVideoData = v_docVideoData.DocumentNode.SelectNodes("//body/div[@id='wrapper']/div[@class ='arkaplan']/div[@class= 'filmcercevea']/div[@id='movie']/div[@class='tab_container']/div[@class='tab_content']/iframe[@src]");

                        // videos list
                        List<video> NotHavevideoList = new List<video>();
                        List<data> dataList = new List<data>();
                        if (Convert.ToString(v_nodesVideoData) != "")
                        {

                            //2/ get server name
                            string[] vtest1 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                            int iii = 0;
                            HtmlNodeCollection dsn = v_docVideoData.DocumentNode.SelectNodes("//*[@id=\"movie\"]/ul/li");
                            foreach (var test12 in dsn)
                            {
                                vtest1[iii] = test12.ChildNodes[0].InnerHtml;
                                iii += 1;
                            }
                            iii = 0;
                            //2/

                            var x = new video() { VideoTitle = "don't have an episode", VideoUrl = "don't have an episode" };
                            var y = db.videos.Add(x);
                            NotHavevideoList.Add(y);
                            foreach (var nodeVedioData in v_nodesVideoData)
                            {//Adding video data 



                                //getting video data of each episode
                                HtmlAttribute v_VideoData = nodeVedioData.Attributes["src"];




                                // Add data to database
                                var c = new data() { VideoData = v_VideoData.Value, VideoDataSourceName = vtest1[iii] };
                                iii += 1;
                                var d = db.datas.Add(c);
                                dataList.Add(d);
                                //
                                // names.Items.Add(v_VideoData.Value);
                            }
                            
                        }


                        movie12.videos = NotHavevideoList;
                        db.Movies.Add(movie12);
                    }




                }
                db.SaveChanges();
                Console.WriteLine(i + ". page has been scraped");
            }

            Console.WriteLine("the website has been scraped");











        }
    }
}

