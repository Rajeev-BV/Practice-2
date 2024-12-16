using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Practice_2
{
    public class SlabProvider
    {
        private HttpClient _httpClient;
        private string url = "/api/GetSlabs";

        public SlabProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
    }
}
