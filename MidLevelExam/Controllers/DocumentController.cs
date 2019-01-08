using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using DocHub.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DocHub.Controllers
{
    public class DocumentController : Controller
    {
        private IDocumentService documentService;
        private static string userEmail = "";
        private IAccountService accountService;

        public DocumentController(IDocumentService _documentService,
            IAccountService _accountService)
        {
            documentService = _documentService;
            accountService = _accountService;

        }

        // GET: Document
        public ActionResult Index()
        {
            // User needs to be authenticated before he/she can view the documents.
            if (!User.Identity.IsAuthenticated || !Request.IsAuthenticated)
            {
                return View("Error");
            }

            return View();

        }

        [HttpPost]
        public ActionResult Upload(List<HttpPostedFileBase> fileData)
        {
            string fileDestination = Server.MapPath("~/Uploads/");

            foreach (HttpPostedFileBase postedFile in fileData)
            {
                if (postedFile != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string path = fileDestination + fileName;
                    postedFile.SaveAs(path);

                    object pathObject = path;
                    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                    object miss = System.Reflection.Missing.Value;
                    object readOnly = true;
                    Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref pathObject, ref miss, ref readOnly, ref miss, ref miss,
                        ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                    string totaltext = "";
                    for (int i = 0; i < docs.Paragraphs.Count; i++)
                    {
                        totaltext += " \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString();
                    }
                    documentService.Add(fileName, totaltext, User.Identity.Name);

                    docs.Close();
                    word.Quit();

                }
            }

            return Content("Success");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Editor(int ID)
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDocumentByID(int ID)
        {
            var result = documentService.GetDocumentById(ID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public EmptyResult ExportToWord(string DocText, string FileName)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
            Response.Output.Write(DocText);
            Response.Flush();
            Response.End();
            return new EmptyResult();

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public EmptyResult ExportToPDF(string DocText, string FileName)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment;filename=" + FileName + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            StringReader sr = new StringReader(DocText.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            return new EmptyResult();

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDocuments()
        {
            var result = documentService.GetDocuments();

            JsonResult json = Json(result, JsonRequestBehavior.AllowGet);
            string json2 = JsonConvert.SerializeObject(result);

            return Content(json2);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendInvitation(string Email, string Url)
        {
            var body = "<p>Good day!</p><p>This is an invitation just click the link below to register:</p><p></p><p><a href='" + Url + "'>" + Url + "</a></p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(Email)); 
            message.From = new MailAddress("DocHub@gmail.com"); 
            message.Subject = "Invitation for Document Collaboration";
            message.Body = string.Format(body);
            message.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    //return RedirectToAction("Sent");
                    //return View();
                    return Json(new { Message = "Invitation successfully sent", Status = true });

                }

            }
            catch (System.Exception)
            {

                return View("Error");
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetUsers()
        {
            var result = accountService.GetUsers();

            JsonResult json = Json(result, JsonRequestBehavior.AllowGet);
            string json2 = JsonConvert.SerializeObject(result);

            return Content(json2);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Exit()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }

}