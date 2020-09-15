using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data;
using ExcelDataReader;
using WebApplication23.Models;

namespace UploadFilesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private DataSource dbS = new DataSource();
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var streams = new FileStream(fullPath, FileMode.Create))
                    {
                       file.CopyTo(streams);
                    }

                    //a
                    string path = @"F:\FinallyYear\New folder\New folder\severNet\WebApplication23\StaticFiles\Images\Book2.xlsx";
                    // For .net core, the next line requires the NuGet package, 
                    // System.Text.Encoding.CodePages
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            while (reader.Read()) //Each row of the file
                            {
                                int id_Lession = 1;
                                int id_Part = 10;
                                string Title_question = "";
                                string question = "";
                                string dapanA = "";
                                string dapanB = "";
                                string dapanC = "";
                                string dapanD = "";
                                string answer = "";
                                string Description = "";
                                string sound = "";
                                string image = "";
                                try
                                {
                                    if (reader.GetValue(0) != null) id_Lession = Int32.Parse(reader.GetValue(0).ToString());
                                    if (reader.GetValue(1) != null) id_Part = Int32.Parse(reader.GetValue(1).ToString());
                                    if (reader.GetValue(2) != null) Title_question = reader.GetValue(2).ToString();
                                    if (reader.GetValue(3) != null) question = reader.GetValue(3).ToString();
                                    if (reader.GetValue(4) != null) dapanA = reader.GetValue(4).ToString();
                                    if (reader.GetValue(5) != null) dapanB = reader.GetValue(5).ToString();
                                    if (reader.GetValue(6) != null) dapanC = reader.GetValue(6).ToString();
                                    if (reader.GetValue(7) != null) dapanD = reader.GetValue(7).ToString();
                                    if (reader.GetValue(8) != null) answer = reader.GetValue(8).ToString();
                                    if (reader.GetValue(9) != null) Description = reader.GetValue(9).ToString();
                                    if (reader.GetValue(10) != null) sound = reader.GetValue(10).ToString();
                                    if (reader.GetValue(11) != null) image = reader.GetValue(11).ToString();
                                }
                                catch (Exception e)
                                {

                                }
                                dbS.Questions.Add(new QuestionModel
                                {
                                    Id_Lession = id_Lession,
                                    Id_Part = id_Part,
                                    title_question = Title_question,
                                    Question = question,
                                    DapanA = dapanA,
                                    DapanB = dapanB,
                                    DapanC = dapanC,
                                    DapanD = dapanD,
                                    Answer = answer,
                                    description = Description,
                                    Sound = sound,
                                    Image = image,
                                    isActive = 1
                                });

                            }


                        }

                    }
                    int output = dbS.SaveChanges();
                    if (output > 0)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest();
                    }

            }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }



        }
    }
}
//[Route("UploadExcel")]
//[HttpPost]
//public string ExcelUpload()
//{
//    string message = "";
//    HttpResponseMessage result = null;
//    var httpRequest = HttpContext.Request;
//    using (AngularDBEntities objEntity = new AngularDBEntities())
//    {

//        if (httpRequest.Files.Count > 0)
//        {
//            HttpPostedFile file = httpRequest.Files[0];
//            Stream stream = file.InputStream;

//            IExcelDataReader reader = null;

//            if (file.FileName.EndsWith(".xls"))
//            {
//                reader = ExcelReaderFactory.CreateBinaryReader(stream);
//            }
//            else if (file.FileName.EndsWith(".xlsx"))
//            {
//                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
//            }
//            else
//            {
//                message = "This file format is not supported";
//            }

//            DataSet excelRecords = reader.AsDataSet();
//            reader.Close();

//            var finalRecords = excelRecords.Tables[0];
//            for (int i = 0; i < finalRecords.Rows.Count; i++)
//            {
//                UserDetail objUser = new UserDetail();
//                objUser.UserName = finalRecords.Rows[i][0].ToString();
//                objUser.EmailId = finalRecords.Rows[i][1].ToString();
//                objUser.Gender = finalRecords.Rows[i][2].ToString();
//                objUser.Address = finalRecords.Rows[i][3].ToString();
//                objUser.MobileNo = finalRecords.Rows[i][4].ToString();
//                objUser.PinCode = finalRecords.Rows[i][5].ToString();

//                objEntity.UserDetails.Add(objUser);

//            }

//            int output = objEntity.SaveChanges();
//            if (output > 0)
//            {
//                message = "Excel file has been successfully uploaded";
//            }
//            else
//            {
//                message = "Excel file uploaded has fiald";
//            }

//        }

//        else
//        {
//            result = Request.CreateResponse(HttpStatusCode.BadRequest);
//        }
//    }
//    return message;
//}

//[Route("UserDetails")]
//[HttpGet]
//public List<UserDetail> BindUser()
//{
//    List<UserDetail> lstUser = new List<UserDetail>();
//    using (AngularDBEntities objEntity = new AngularDBEntities())
//    {
//        lstUser = objEntity.UserDetails.ToList();
//    }
//    return lstUser;
//}

