namespace TwoFactorAuthApp.Models
{
	public class OTP
	{
		public string inputOTP { get; set; }
		public string targetOTP { get; set; }
		public OTP() { }
		public OTP(string inputOTP, string targetOTP)
		{
			this.inputOTP = inputOTP;
			this.targetOTP = targetOTP;
		}
	}
}
