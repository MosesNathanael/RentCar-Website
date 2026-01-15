namespace RentCar_MVC.DTOs
{
    public class LoginResponseDto
    {
        public string UserEmail { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }

        public string UserName {get; set;} = string.Empty;
    }

}