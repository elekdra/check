
namespace backend.Models
{
 public class FileModel
 {

  public FileModel()
  {

  }

  public FileModel(string fileName, string fileContent, string company, string version, string training, string minVersion, string mode)
  {
   this.FileName = fileName;
   this.FileContent = fileContent;
   this.Company = company;
   this.Version = version;
   this.Training = training;
   this.MinVersion = minVersion;
   this.Mode = mode;
  }

  public override string ToString()
  {
   return $"{FileName}, {FileContent}, {Company}, {Version}, {Training}, {MinVersion}, {Mode}";
  }

  public string FileName { get; set; }
  public string FileContent { get; set; }
  public string Company { get; set; }
  public string Version { get; set; }
  public string Training { get; set; }
  public string MinVersion { get; set; }
  public string Mode { get; set; }
 }
}