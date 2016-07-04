using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oxoeseMovieScraper
{
    public class serverName
    {

        string ServerUrl;
        public string hostName;

        public void getServerName(string serverUrl)
        {
            ServerUrl = serverUrl;
            Uri serverUri = new Uri(ServerUrl);
            hostName = serverUri.Host;

        }

    }
}
