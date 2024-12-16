using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Practice_2
{
    public class TaxCalculator
    {
        private HttpClient _httpClient;

        public TaxCalculator(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<double> CalculateTaxAsync(double grossSalary)
        {
            double taxAmount = 0;
            var response = await _httpClient.GetAsync("http://test.com/");
            var body = await response.Content.ReadAsStringAsync();
            var slabs = JsonSerializer.Deserialize<List<TaxSlabs>>(body);

            foreach (TaxSlabs slab in slabs)
            {
                if (grossSalary > slab.SlabAmount)
                {
                    taxAmount += (grossSalary - slab.SlabAmount) * slab.taxAmount / 100;
                    grossSalary = slab.SlabAmount;
                }
            }

            return taxAmount;
        }
    }
}
