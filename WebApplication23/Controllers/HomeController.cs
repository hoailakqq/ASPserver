using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplication23.Entities;
using WebApplication23.Models;
using WebApplication23.Helpers;
using System.IO;
using System.Data;
using ExcelDataReader;
using OfficeOpenXml;
using System.Collections.Generic;

namespace WebApplication23.Controllers
{

    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        private DataSource dbS = new DataSource();
        [Route("Home/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            var getAll = db.Users.ToList();

            return Ok(getAll);
        }




        [Route("Home/getQuestion")]
        [HttpGet]
        public IActionResult getAllQuestions(int id)
        {
            var qr = db.Lessions.FromSqlRaw("select l.id, l.id_category, l.link, l.name, l.image, l.imageCheck, l.isActive from lession l JOIN category c on l.id_category = c.id where c.id = " + id + " and c.isActive=1 and l.isActive=1").ToList();

            return Ok(qr);
        }



        [Route("Home/getConfig")]
        [HttpPost]
        public IActionResult getConfig(int id_user, String type)
        {
            var getAll = db.Overviews.FirstOrDefault(p => p.Id_user == id_user && p.Type == type);

            return Ok(getAll);
        }

        [Route("Home/getLession")]
        [HttpPost]
        public IActionResult getLessions([FromBody] GetID aaa)
        {
            var getAll = db.Lessions.FromSqlRaw("select * from lession where id_category =" + aaa.Id).ToList();

            return Ok(getAll);

        }

        [Route("Home/getLessions")]
        [HttpGet]
        public IActionResult getLession()
        {
            var getAll = db.Lessions.ToList();

            return Ok(getAll);

        }

        [Route("Home/getQuesions")]
        [HttpGet]
        public IActionResult getQuesions()
        {
            var getAll = db.Questions.ToList();

            return Ok(getAll);
        }

        [Route("Home/getParts")]
        [HttpGet]
        public IActionResult getParts()
        {
            var getAll = db.Parts.ToList();

            return Ok(getAll);
        }

        [Route("Home/getPart")]
        [HttpPost]
        public IActionResult getPart([FromBody] GetID aaa)
        {
            if (aaa != null)
            {

                var getAll = db.Parts.FromSqlRaw("select * from part where id_category =" + aaa.Id).ToList();

                return Ok(getAll);
            }
            return Ok("lỗi");
        }

        [Route("Home/getPartRead")]
        [HttpGet]
        public IActionResult getPartRead()
        {
            var qr = db.Parts.FromSqlRaw("select * from part where type=" + "'read'").ToList();

            return Ok(qr);
        }

        [Route("Home/getCategorys")]
        [HttpGet]
        public IActionResult getCategorys()
        {
            var qr = db.Categorys.FromSqlRaw("select * from category where id > 2").ToList();

            return Ok(qr);
        }

        [Route("Home/getPartListening")]
        [HttpGet]
        public IActionResult getPartListening()
        {
            var qr = db.Parts.FromSqlRaw("select * from part where type=" + "'listening'").ToList();

            return Ok(qr);
        }

        [Route("Home/getQuestionPart")]
        [HttpPost]
        public IActionResult getQuestionPart([FromBody] GetQuestion user)
        {
            if (user != null)
                try
                {

                    var qr = db.Questions.FromSqlRaw("select q.isActive,q.id,q.id_part,q.title_question,q.id_lession,q.question,q.dapanA,q.dapanB,q.dapanC,q.dapanD,q.answer,q.image,q.sound,q.description " +
             "from `question` as q,`lession` as l,`category` as c, `part` as p, (SELECT q.sound as ss " +
             "from `question` q where q.id_part = " +
               user.Id_part +
               " GROUP BY q.sound) as q2 " +
              "WHERE c.id = l.id_category and l.id = q.id_lession and c.id = " +
               user.Id +
               " and p.id=q.id_part and l.id=" +
               user.Id_lession +
               " and q.sound=q2.ss").ToList();

                    return Ok(qr);
                }
                catch (global::System.Exception)
                {
                    return Ok("lỗi");
                }
            return Ok("lỗi");


        }

        [Route("Home/getQuestionPart2")]
        [HttpPost]
        public IActionResult getQuestionPart2([FromBody] GetQuestion user)
        {

            return Ok(user);
        }

        // Information USER
        [Route("Home/getData")]
        [HttpGet]
        public IActionResult getData()
        {
            var qr = db.Users.FromSqlRaw("select * from User").ToList();

            return Ok(qr);
        }


        // Information USER
        [Route("Home/sendData")]
        [HttpPost]
        public IActionResult sendData(String username, String email, String password)
        {
            //User a = new User(username, password, "", email, "", 2, 1);
            //db.Users.Add(a);
            //db.SaveChanges();
            return Ok("success");
        }


        // Information USER
        [Route("Home/checkLogin")]
        [HttpPost]
        public IActionResult checkLogin(String username, String password)
        {
            var qr = db.Users.FromSqlRaw("select * from User where username='" + username + "' and password='" + password + "'").ToList();

            return Ok(qr);
        }

        [Route("Home/checkLogin2")]
        [HttpPost]
        public IActionResult checkLogin2(User user)
        {
            var qr = db.Users.FromSqlRaw("select * from User where username='" + user.Username + "' and password='" + user.Password + "'").ToList();

            return Ok(qr);
        }

        [Route("Home/checkLogin3")]
        [HttpPost]
        public IActionResult checkLogin3(String username)
        {
            var qr = db.Users.FromSqlRaw("select * from User where username='" + username + "'").ToList();
            return Ok(qr);
        }

        [Route("Home/signup")]
        [HttpPost]
        public IActionResult signup(User user)
        {
            //var usera = new User(4,"123123","123123","asss","123");
            //Console.WriteLine(user.)
            db.Users.Add(user);
            db.SaveChanges();
            return Ok("success");
        }

        [Route("Home/signup2")]
        [HttpPost]
        public IActionResult signup2([FromBody] User user)
        {
            user.RoleId = 2;
            user.IsActive = 1;
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Ok(new AuthenticateResponse(user, generateJwtToken(user)));
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Username or password is incorrect" });
            }

        }
        public string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("NGUYENTHUONGHOAIIN0NOml3z9FMfmpgXwovR9fp6ryDIoGRM8EPHAB6iHsc0fb");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [Route("Home/getRoles")]
        [HttpGet]
        public IActionResult getRoles()
        {
            var qr = db.role_users.FromSqlRaw("select * from role_user").ToList();

            return Ok(qr);
        }

        [Route("Home/addQuestion")]
        [HttpPost]
        public IActionResult addQuestion([FromBody] QuestionModel question)
        {
            if (question != null)
                try
                {
                    db.Questions.Add(question);
                    db.SaveChanges();
                    return Ok(question);
                }
                catch (Exception)
                {

                    return BadRequest(new { message = "incorrect" });
                }
            return BadRequest(new { message = "incorrect" });
        }
        [Route("Home/getLogByDay")]
        [HttpPost]
        public IActionResult getLogByDay([FromBody] GetQuestion question)
        {
            if (question != null)
                try
                {
                    var qr = db.Logs.FromSqlRaw("select * from log where current_day = '" + question.Day + "'").ToList();
                    return Ok(qr);
                }
                catch (Exception e)
                {

                    return BadRequest(new { message = " incorrect " + e.Message });
                }
            return BadRequest(new { message = "null" });
        }
        [Route("Home/getFamousUser")]
        [HttpGet]
        public IActionResult getFamousUser()
        {

            try
            {
                var result = Helper.RawSqlQuery(
                "SELECT user.`Name`,rank.total_score FROM luanvan.rank INNER JOIN user ON rank.id_user =`user`.Id ORDER BY total_score DESC limit 10",
    x => new TopUser { Name = (string)x[0], Count = (int)x[1] });
                result.ForEach(x => Console.WriteLine($"{x.Name,-25}{x.Count}"));
                // var qr = db.Users.FromSqlRaw("SELECT * FROM user as v1 JOIN (select id_user from rank WHERE total_score > 0 ORDER BY total_score DESC limit 10) as v2 where v1.Id = v2.id_user").ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = " incorrect " + e.Message });
            }
        }

        [Route("Home/logsys")]
        [HttpPost]
        public IActionResult logsys([FromBody] Logsys user)
        {
            if (user != null)
            {
                db.Logsys.Add(user);
                db.SaveChanges();
                return Ok("success");
            }
            return Ok("loi");

        }
        [Route("Home/delQuesionById")]
        [HttpPost]
        public IActionResult delQuesionById([FromBody] GetID user)
        {
            if (user != null)
            {
                try
                {
                    db.Questions.FromSqlRaw("DELETE FROM `question` WHERE `id` = " + user.Id).ToList();
                }
                catch (Exception)
                {

                    return Ok("loi");
                }

                return Ok("success");
            }
            return Ok("loi");

        }

        [Route("Home/getQuesionById")]
        [HttpPost]
        public IActionResult getQuesionById([FromBody] GetID user)
        {
            if (user != null)
            {
                try
                {
                    var qr = db.Questions.FromSqlRaw("SELECT * FROM `question` WHERE `id` = " + user.Id).ToList();
                    return Ok(qr);
                }
                catch (Exception e)
                {

                    return Ok("loi" + e.Message);
                }


            }
            return Ok("loi");

        }

        [Route("Home/getWord")]
        [HttpPost]
        public IActionResult getWord([FromBody] Dictionary user)
        {
            if (user != null)
            {
                try
                {
                    var qr = db.Dictionaries.FromSqlRaw("SELECT * FROM `dictionary` WHERE `Word` = '" + user.Word + "'").ToList();
                    return Ok(qr);
                }
                catch (Exception e)
                {

                    return Ok("loi" + e.Message);
                }


            }
            return Ok("loi" + user);

        }

        [Route("Home/RankOfUser")]
        [HttpPost]
        public IActionResult RankOfUser([FromBody] Rank a)
        {
            if (a != null)
            {
                try
                {
                    var qr = db.ranks.FromSqlRaw("select * from `rank` WHERE `rank`.id_user =" + a.Id_user).ToList();
                    return Ok(qr);
                }
                catch (Exception e)
                {
                    return Ok(a);
                }

            }
            return Ok("loi" + a);
        }

        [Route("Home/UpdateScore")]
        [HttpPost]
        public IActionResult UpdateScore([FromBody] Rank a)
        {
            if (a != null)
            {
                try
                {
                    var qr = db.ranks.FromSqlRaw("UPDATE `rank` SET crown = "+a.Crown+ ", total_score = " + a.Total_score + " WHERE id_user = " + a.Id_user).ToList();
                    return Ok(qr);
                }
                catch (Exception e)
                {
                    return Ok(a);
                }

            }
            return Ok("loi" + a);
        }


        [Route("Home/UpdateUser")]
        [HttpPost]
        public IActionResult UpdateUser([FromBody] User a)
        {
            if (a != null)
            {
                try
                {
                    db.ranks.FromSqlRaw("UPDATE `luanvan`.`user` SET `Name` = '" + a.Name + "', `Email` = '" + a.Email + "', `Avatar` = '" + a.Avatar + "', `IsActive` = 1 WHERE `Id` = " + a.Id);
                    return Ok(a);
                }
                catch (Exception e)
                {
                    return Ok(e.Message);
                }

            }
            return Ok("loi" + a);
        }

        [Route("Home/getDataQuesionSource")]
        [HttpGet]
        public IActionResult getDataQuesionSource()
        {
            List<QuestionModel> users = new List<QuestionModel>();
            String path = @"D:\Book2.xlsx";
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
        [Route("Home/getCheckQuestions")]
        [HttpGet]
        public IActionResult getCheckQuestions()
        {
                try
                {
                   var qr = dbS.Questions.ToList();
                    return Ok(qr);
                }
                catch (Exception e)
                {
                    return Ok(e.Message);
                }

        }

        [Route("Home/pushData")]
        [HttpPost]
        public IActionResult pushData([FromBody] GetID user)
        {
            if (user != null)
            {
                try
                {
                    var qr = dbS.Questions.FromSqlRaw("SELECT * FROM `question` WHERE `id` = " + user.Id).ToList();
                    db.Questions.Add(new QuestionModel
                    {
                        Id_Lession = qr[0].Id_Lession,
                        Id_Part = qr[0].Id_Part,
                        title_question = qr[0].title_question,
                        Question = qr[0].Question,
                        DapanA = qr[0].DapanA,
                        DapanB = qr[0].DapanB,
                        DapanC = qr[0].DapanC,
                        DapanD = qr[0].DapanD,
                        Answer = qr[0].Answer,
                        description = qr[0].description,
                        Sound = qr[0].Sound,
                        Image = qr[0].Image,
                        isActive = 1
                    });
                    int output = db.SaveChanges();
                    dbS.Questions.FromSqlRaw("DELETE FROM `learning1`.`question` WHERE `id` = " + user.Id).ToList();
                        
                    if (output > 0)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception e)
                {

                 
                }


            }
            return Ok("loi");

        }
        [Route("Home/delQuesionSById")]
        [HttpPost]
        public IActionResult delQuesionSById([FromBody] GetID user)
        {
            if (user != null)
            {
                try
                {
                    dbS.Questions.FromSqlRaw("DELETE FROM `learning1`.`question` WHERE `id` = " + user.Id).ToList();
                }
                catch (Exception)
                {

                  
                }

                return Ok("success");
            }
            return Ok("loi");

        }
    }
}
