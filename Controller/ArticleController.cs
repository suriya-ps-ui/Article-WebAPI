using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Controllers{
    [ApiController]
    public class ArticleController:ControllerBase{
        private  ArticleService articleService;
        private UserService userService;
        private IConfiguration configuration;
        public ArticleController(ArticleService articleService,UserService userService,IConfiguration configuration){
            this.articleService=articleService??throw new ArgumentNullException(nameof(articleService));
            this.userService=userService;
            this.configuration=configuration;
        }

        [HttpPost("/Login")]
        public IActionResult Login([FromBody]Login login){
            var user=userService.GetUser(login.userName,login.userPassword);
            if(user==null){
                return Unauthorized("Wrong Username or Password");
            }
            var claims=new[]{
                new Claim(ClaimTypes.NameIdentifier,user.userId.ToString()),
                new Claim(ClaimTypes.Name,user.userName)
            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]??""));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token=new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:creds
            );
            return Ok(new{token=new JwtSecurityTokenHandler().WriteToken(token)});
        }

        [HttpGet("/Get")]
        [Authorize]
        public IActionResult GetArticles(){
            var articles=articleService.GetArticleRecords();
            return Ok(articles);
        }
        [HttpPost("/Post")]
        [Authorize]
        public IActionResult PostArticle([FromBody] Article article){
            articleService.PostOrUpdateRecords(article);
            return CreatedAtAction("GetArticles",new {id=article.articleId},article);
        }
        // [HttpPost("/POST")]
        // [Authorize]
        // public IActionResult PostArticle([FromBody] Article article){
        //     articleService.PostArticleRecords(article);
        //     return CreatedAtAction("GetArticles",new { id=article.articleId },article);
        // }
        // [HttpPut("/PUT")]
        // [Authorize]
        // public IActionResult UpdateArticle([FromQuery] int articleId,[FromBody] string articleTitle){
        //     articleService.UpdateRecords(articleId, articleTitle);
        //     return NoContent();
        // }
        [HttpDelete("/Delete")]
        [Authorize]
        public IActionResult DeleteArticle([FromQuery] int articleId){
            articleService.DeleteArticleRecords(articleId);
            return NoContent();
        }
    }
}