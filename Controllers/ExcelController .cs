using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ClosedXML.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AriesWebApi.Controllers {
    public class ExcelController : ControllerBase {
        [HttpGet]
        [Route ("api/file/{id}")]
        public async Task<HttpResponseMessage> DownloadFile (int id) {
            var wb = await BuildExcelFile (id);
            return wb.Deliver ("excelfile.xlsx");
        }

        private async Task<XLWorkbook> BuildExcelFile (int id) {
            //Creating the workbook
            var t = Task.Run (() => {
                var wb = new XLWorkbook ();
                var ws = wb.AddWorksheet ("Sheet1");
                ws.FirstCell ().SetValue (id);

                return wb;
            });

            return await t;
        }

        public void H(){

            

        }
    }
}