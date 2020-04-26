﻿using IEXTrading.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IEXTrading.ModelDto;
using IEXTrading.Models;
using System.Threading.Tasks;

namespace IEXTrading.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
    public class DataController:Controller
    {
        public ApplicationDbContext dbContext;

        public DataController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        [HttpGet()]
        public IActionResult GetData()
        {
            var companies = dbContext.Companies
                                        .Include(c => c.Equities)
                                        
                                        .ToList();

            var companiesToReturn = Mapper.Map <IEnumerable<CompanyDto>>(companies);
            return Ok(companiesToReturn);
        }
    }
}
