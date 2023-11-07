using System.ComponentModel.DataAnnotations;

namespace TwoFactorAuthApp.Models
{
	public class User
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public User() { }
		public User(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
