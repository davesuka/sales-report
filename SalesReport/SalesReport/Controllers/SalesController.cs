using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesReport.Context;
using SalesReport.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SalesReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDBContext context;

        public SalesController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.Sales.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpGet("{dealNumber}", Name = "GetSingleSale")]
        public ActionResult Get(int dealNumber)
        {
            try
            {
                var sale = context.Sales.FirstOrDefault(c => c.DealNumber == dealNumber);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post(IFormFile file)
        {
            try
            {
                var fileData = Request.Form.Files[0];

                List<Sales> items = new List<Sales>();
                using (var reader = new StreamReader(fileData.OpenReadStream()))
                {
                    reader.ReadLine();
                    
                    while (reader.Peek() >= 0)
                    {
                        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                        string[] result = Regex.Split(reader.ReadLine(), "[,]{1}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                        
                        //string[] result = CSVParser.Split(reader.ReadLine());
                        //string[]result = reader.ReadLine().Split(",");
                        string priceLine = result[4];
                        string dateLine = result[5];

                        double price = 0;
                        double.TryParse(JsonConvert.DeserializeObject<string>(priceLine), out price);

                        CultureInfo culture = new CultureInfo("en-US");
                        DateTime date = new DateTime();//DateTime.ParseExact(dateLine, "M/dd/yyyy", culture);
                        DateTime.TryParseExact(dateLine, "M/dd/yyyy", culture, DateTimeStyles.None, out date);
                        items.Add(new Sales 
                        { 
                            DealNumber = int.Parse(result[0]),
                            CustomerName = result[1],
                            DealerShipName = result[2],
                            Vehicle = result[3],
                            Price = price,
                            Date = date
                        });
                    }
                        
                }
                
                context.AddRange(items);
                context.SaveChanges();
                return Ok();                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{dealNumber}")]
        public ActionResult Put(int dealNumber, [FromBody] Sales sale)
        {
            try
            {
                if(sale.DealNumber == dealNumber)
                {
                    context.Entry(sale).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetSingleSale", new { dealNumber = sale.DealNumber }, sale);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{dealNumber}")]
        public ActionResult Delete(int dealNumber)
        {
            try
            {
                var sale = context.Sales.FirstOrDefault(f => f.DealNumber == dealNumber);
                if(sale != null)
                {
                    context.Sales.Remove(sale);
                    context.SaveChanges();
                    return Ok(dealNumber);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
