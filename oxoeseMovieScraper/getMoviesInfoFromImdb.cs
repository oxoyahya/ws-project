using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
namespace oxoeseMovieScraper
{
    public class getMoviesInfoFromImdb
    {    public string SearchedMovieName;

        public  void test1(string searchedMovieName)
        {
    
             
        SearchedMovieName = searchedMovieName;

            string v1_url = "http://www.imdb.com/find?ref_=nv_sr_fn&q=" + SearchedMovieName + "&s=all";
        Console.WriteLine(v1_url);
            HtmlWeb v1_htmlweb = new HtmlWeb();
        HtmlAgilityPack.HtmlDocument v1_doc = v1_htmlweb.Load(v1_url);

        HtmlNodeCollection v1_nodes = v1_doc.DocumentNode.SelectNodes("//td[@class='result_text']/a[@href]");

            foreach (var v1_node in v1_nodes)
            {
                HtmlAttribute v1_getSeries = v1_node.Attributes["href"];
        string a1 = v1_node.FirstChild.InnerHtml;
        Console.WriteLine(a1);

                Console.WriteLine("1  " + v1_getSeries.Value);
                string web1 = "http://www.imdb.com/" + v1_getSeries.Value;



        HtmlWeb v2_htmlweb = new HtmlWeb();
        HtmlDocument v2_doc = v2_htmlweb.Load(web1);


        HtmlNodeCollection v2_nodes = v2_doc.DocumentNode.SelectNodes("//*[@class='summary_text']");
                if (v2_nodes != null)
                {
                    foreach (var v2_node in v2_nodes)
                    {
                        string a = v2_node.FirstChild.InnerHtml;
        Console.WriteLine(a);

                    }
}
            }

        }

    }
}
