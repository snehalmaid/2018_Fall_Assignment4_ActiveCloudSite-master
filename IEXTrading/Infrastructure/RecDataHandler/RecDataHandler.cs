using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using IEXTrading.Models;
using Newtonsoft.Json;

namespace IEXTrading.Infrastructure.RecDataHandler
{
    public class RecDataHandler
    {
        static string BASE_URL = "https://data.montgomerycountymd.gov/resource/ijwf-vj4h.json"; //This is the base URL, method specific URL is appended to this.
        HttpClient httpClient;

        public RecDataHandler()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /****
         * Calls the IEX reference API to get the list of symbols. 
        ****/
        public List<Rec> Getrecdata()
        {
            string Rec_API_PATH = BASE_URL + "?";
            string companyList = "";

            List<Rec> companies = null;
                        
            httpClient.BaseAddress = new Uri(Rec_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(Rec_API_PATH).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                companyList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            if (!companyList.Equals(""))
            {
                companies = JsonConvert.DeserializeObject<List<Rec>>(companyList);
                
                companies = companies.GetRange(20, 40);
                
            }
            return companies;
        }

        /****
         * Calls the IEX stock API to get 1 year's chart for the supplied symbol. 
        ****/
        public List<Equity> GetChart(string symbol)
        {
            //Using the format method.
            //string IEXTrading_API_PATH = BASE_URL + "stock/{0}/batch?types=chart&range=1y";
            //IEXTrading_API_PATH = string.Format(IEXTrading_API_PATH, symbol);

            string Rec_API_PATH = BASE_URL + "stock/" + symbol + "/batch?types=chart&range=1y";

            string charts = "";
            List<Equity> Equities = new List<Equity>();
            httpClient.BaseAddress = new Uri(Rec_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(Rec_API_PATH).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                charts = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!charts.Equals(""))
            {
                ChartRoot root = JsonConvert.DeserializeObject<ChartRoot>(charts, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                Equities = root.chart.ToList();
            }
            //make sure to add the symbol the chart
            foreach (Equity Equity in Equities)
            {
                Equity.symbol = symbol;
            }

            return Equities;
        }
    }
}
