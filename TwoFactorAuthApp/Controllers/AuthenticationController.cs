using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using TwoFactorAuthApp.Models;
using static System.Net.WebRequestMethods;

namespace TwoFactorAuthApp.Controllers
{
	public class AuthenticationController : Controller
	{
		User IniUser = new User("haha1207", "hihi1232003");
		string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "OTP.txt");
		public IActionResult LoginForm()
		{
			return View("Views/Authentication/LoginForm.cshtml");
		}
		[HttpPost]
		public IActionResult SendSMS([FromBody]SmsMessage model)
		{
			string accountSid = "AC41dd883e2436ec35c8071b870f6ddb96";
			string authToken = "5bb9f593f6b09d5b080a8b77b4f01be8";

			TwilioClient.Init(accountSid, authToken);

			var result = MessageResource.Create(
				body: model.OTP,
				from: new Twilio.Types.PhoneNumber("+447576006174"),
				to: new Twilio.Types.PhoneNumber(model.To)
			);
			Console.WriteLine(result.Sid);

			return Ok("Success sending OTP");
		}
		[HttpPost]
		public IActionResult Login(User user)
		{
			if (user.Username == IniUser.Username && user.Password == IniUser.Password)
			{
				Random randomOTP = new Random();
				string OTP = randomOTP.Next(1000, 9999).ToString();
				SmsMessage smsMessage = new SmsMessage(OTP, "+84814371165");
				SendSMS(smsMessage);
				System.IO.File.WriteAllText(path, OTP);
				return View("/Views/Authentication/VerifyOTP.cshtml");
			}
			else
			{
				ViewBag.Message = "Incorrect Username or Password";
			}
			return View("LoginForm");
		}
		[HttpPost]
		public IActionResult VerifyOTP(string OTP)
		{
			string Line= System.IO.File.ReadAllText(path);
			string targetOTP = Line.ToString();
			if (OTP==targetOTP)
			{
				return Redirect("/Authentication/HomePage");
			}
			return View("VerifyOTP");
		}
		public IActionResult HomePage()
		{
			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}