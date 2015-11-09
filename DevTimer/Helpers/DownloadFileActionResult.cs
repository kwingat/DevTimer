using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

namespace DevTimer.Helpers
{
    public class DownloadFileActionResult : ActionResult
    {
        public ExcelPackage Package { get; set; }
        public string FileName { get; set; }

        public DownloadFileActionResult(ExcelPackage package, string pFileName)
        {
            Package = package;
            FileName = pFileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            // Create a response stream to create and write the excel file
            HttpContext curContext = HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", FileName));
            curContext.Response.Charset = "";
            curContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            curContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            curContext.Response.BinaryWrite(Package.GetAsByteArray());
            curContext.Response.End();

            // Convert the rendering of the gridview to a string representation
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //ExcelGridView.RenderControl(htw);

            // Open a memory stream that you can use to write back to the response
            //Byte[] byteArray = Package.GetAsByteArray();
            //MemoryStream ms = new MemoryStream(byteArray);
            //StreamReader sr = new StreamReader(ms, Encoding.ASCII);

            //// Write the stream back ot the response
            //curContext.Response.Write(sr.ReadToEnd());
            //curContext.Response.End();
        }
    }
}