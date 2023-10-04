using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace RandomPasscode.Controllers
{
    public class Passcode : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            generateCode();
            HttpContext.Session.SetInt32("countPasscode", 1);
            return View();
        }

        [HttpPost]
        [Route("generatecode")]
        public IActionResult Getcode()
        {
            int? count = HttpContext.Session.GetInt32("countPasscode");
            if (count != null)
            {
                count++;
                generateCode();
                HttpContext.Session.SetInt32("countPasscode", (int)count);
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private void generateCode()
        {
            int codeLength = 14;
            Random rand = new Random();
            const string validCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] charactersCode = new char[codeLength];

            for (int i = 0; i < codeLength; i++)
            {
                int randomInt = rand.Next(validCharacters.Length);
                charactersCode[i] = validCharacters[randomInt];
            }
            HttpContext.Session.SetString("Passcode", new string(charactersCode));
        }
    }
}
