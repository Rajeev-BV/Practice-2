using System.Net.Http;
using System.Text.Json;

namespace Practice_2
{
    public class OperatorProvider
    {
        private HttpClient httpClient;

        public OperatorProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public int Add (string operatorSign)
        {
            if (operatorSign == "+")
                return 9;
            else
                return 0;
        }

        public async Task<int> GetOperatorAndAdd()
        {
            string baseURI = "https://localhost";
            HttpResponseMessage response = await httpClient.GetAsync(baseURI+ "/api/getOperator");
            if (response.IsSuccessStatusCode)
            {
                var opertorContentResponse =  await response.Content.ReadAsStringAsync();
                var operatorContent = JsonSerializer.Deserialize<IEnumerable<Operator>>(opertorContentResponse);
                string operatorSign = null;
                foreach (var item in operatorContent)
                {
                    operatorSign = item.OperatorName;
                }
                if (operatorSign == "+")
                    return 9;
                else
                    return 0;

                
            }
            return 0;
        }
    }
}