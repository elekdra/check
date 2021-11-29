
namespace backend.Models
{
 public class userCredentials
 {
  public userCredentials(string username, string password)
  {
   this.userName = username;
   this.passWord = password;

  }
  public string userName { get; set; }
  public string passWord { get; set; }
 }
}