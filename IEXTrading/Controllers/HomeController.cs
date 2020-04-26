using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IEXTrading.Infrastructure.RecDataHandler;
using IEXTrading.Models;
using IEXTrading.Models.ViewModel;
using IEXTrading.DataAccess;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace MVCTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext dbContext;
        private readonly AppSettings _appSettings;
        public const string SessionKeyName = "Recreational_Activities";
        //List<Company> companies = new List<Company>();
        public HomeController(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            dbContext = context;
            _appSettings = appSettings.Value;
        }

       public IActionResult HelloIndex()
        {
            ViewBag.Hello = _appSettings.Hello;
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        /****
         * The Symbols action calls the GetSymbols method that returns a list of Companies.
         * This list of Companies is passed to the Symbols View.
        ****/
        public IActionResult Read_rec_data()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            RecDataHandler webHandler = new RecDataHandler();
            List<Rec> rec_centers = webHandler.Getrecdata();

            String recdata = JsonConvert.SerializeObject(rec_centers);
            //int size =  System.Text.ASCIIEncoding.ASCII.GetByteCount(companiesData);

            HttpContext.Session.SetString(SessionKeyName, recdata);
            
            return View(rec_centers);
        }

        /****
         * The Chart action calls the GetChart method that returns 1 year's equities for the passed symbol.
         * A ViewModel CompaniesEquities containing the list of companies, prices, volumes, avg price and volume.
         * This ViewModel is passed to the Chart view.
        ****/
        /* public IActionResult Chart(string symbol)
         {
             //Set ViewBag variable first
             ViewBag.dbSuccessChart = 0;
             List<Equity> equities = new List<Equity>();
             if (symbol != null)
             {
                 IEXHandler webHandler = new IEXHandler();
                 equities = webHandler.GetChart(symbol);
                 equities = equities.OrderBy(c => c.date).ToList(); //Make sure the data is in ascending order of date.
             }

             CompaniesEquities comp  aniesEquities = getCompaniesEquitiesModel(equities);

             return View(companiesEquities);
         }

         /****
          * The Refresh action calls the ClearTables method to delete records from a or all tables.
          * Count of current records for each table is passed to the Refresh View.
         ****/
        public IActionResult Refresh(string tableToDel)
         {
             ClearTables(tableToDel);
             Dictionary<string, int> tableCount = new Dictionary<string, int>();
             tableCount.Add("Delete all Records", dbContext.Companies.Count());
             tableCount.Add("Delete records for activity Basketball", dbContext.Companies.Where(c => c.activity == "Basketball").Count());
             tableCount.Add("Delete records for activity Badminton", dbContext.Companies.Where(c => c.activity == "Badminton").Count());
             //tableCount.Add("Charts", dbContext.Equities.Count());
             
             return View(tableCount);
         }

        /****
 * Deletes the records from tables.
****/
        public void ClearTables(string tableToDel)
        {
            if ("Delete all Records".Equals(tableToDel))
            {
               
                dbContext.Companies.RemoveRange(dbContext.Companies);
            }
            else if ("Delete records for activity Basketball".Equals(tableToDel))
            {
               
                dbContext.Companies.RemoveRange(dbContext.Companies
                                                         .Where(c => c.activity == "Basketball")
                                                                      );
            }
            else if ("Delete records for activity Badminton".Equals(tableToDel))
            {
                dbContext.Companies.RemoveRange(dbContext.Companies
                                                         .Where(c => c.activity == "Badminton")
                                                                      );
            }
            dbContext.SaveChanges();
        }

        /****
         * Saves the Symbols in database.
        ****/
        public IActionResult Savedata()
        {
            string recdata = HttpContext.Session.GetString(SessionKeyName);
            List<Rec> rec_center_list = null;
            if (recdata != "")
            {
                rec_center_list = JsonConvert.DeserializeObject<List<Rec>>(recdata);
            }
            
            foreach (Rec r in rec_center_list)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                //if (dbContext.Companies.Where(c => c.activity.Equals(company.activity)).Count() == 0)
                //{
                    dbContext.Companies.Add(r);
                //}
            }
            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("Read_rec_data", rec_center_list);
        }

        /****
         * Saves the equities in database.
        ****/
        /*public IActionResult SaveCharts(string symbol)
        {
            IEXHandler webHandler = new IEXHandler();
            List<Equity> equities = webHandler.GetChart(symbol);
            //List<Equity> equities = JsonConvert.DeserializeObject<List<Equity>>(TempData["Equities"].ToString());
            foreach (Equity equity in equities)
            {
                if (dbContext.Equities.Where(c => c.date.Equals(equity.date)).Count() == 0)
                {
                    dbContext.Equities.Add(equity);
                }
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessChart = 1;

            CompaniesEquities companiesEquities = getCompaniesEquitiesModel(equities);

            return View("Chart", companiesEquities);
        }



        /****
         * Returns the ViewModel CompaniesEquities based on the data provided.
         ****/
        /*    public CompaniesEquities getCompaniesEquitiesModel(List<Equity> equities)
            {
                List<Company> companies = dbContext.Companies.ToList();

                if (equities.Count == 0)
                {
                    return new CompaniesEquities(companies, null, "", "", "", 0, 0);
                }

                Equity current = equities.Last();
                string dates = string.Join(",", equities.Select(e => e.date));
                string prices = string.Join(",", equities.Select(e => e.high));
                string volumes = string.Join(",", equities.Select(e => e.volume / 1000000)); //Divide vol by million
                float avgprice = equities.Average(e => e.high);
                double avgvol = equities.Average(e => e.volume) / 1000000; //Divide volume by million
                return new CompaniesEquities(companies, equities.Last(), dates, prices, volumes, avgprice, avgvol);
            }*/

    }
}
