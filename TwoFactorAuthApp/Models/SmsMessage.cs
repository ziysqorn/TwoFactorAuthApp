namespace TwoFactorAuthApp.Models
{
	public class SmsMessage
	{
		public string OTP { get; set; }
		public string To { get; set; }
		public SmsMessage() { }
		public SmsMessage(string otp, string to) {
			OTP = otp;
			To = to;
		}
	}
}
