using System.Collections.Generic;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;

namespace backend.Controllers
{
 [ApiController]
 [Route("api/[controller]")]

 public class TaskController : ControllerBase
 {
  IWebHostEnvironment environment;
  public TaskController(IWebHostEnvironment environment)
  {
   this.environment = environment;
  }
  [HttpGet]
  [Route("authenticate")]
  public bool UserAuthenticate(string credentials)
  {
   var userDetails = credentials.Split("|");

   var currentDirectory = System.IO.Directory.GetCurrentDirectory();
   string jsonPath = currentDirectory + @"\wwwroot\Credentials\Credentials.json";
   var userCredentials = System.IO.File.ReadAllText(jsonPath);
   object jsonObject = JsonConvert.DeserializeObject(userCredentials);
   var list = JsonConvert.DeserializeObject<List<userCredentials>>(userCredentials);
   bool Flag = false;
   foreach (var item in list)
   {
    if (item.userName == userDetails[0] && item.passWord == userDetails[1])
    {
     Flag = true;
    }
   }
   if (Flag)
   {
    return true;
   }
   else
   {
    return false;
   }
  }

  [HttpPut]
  [Route("filesave")]
  public IActionResult PutFileNames([FromBody] FileModel model)
  {
   string webRootPath = environment.WebRootPath;
   string filesPath = Path.Combine(webRootPath, "files");
   string fileNamesave = $"{Path.GetFileNameWithoutExtension(model.FileName)}_{Guid.NewGuid().ToString()}{Path.GetExtension(model.FileName)}";
   string path = filesPath + "\\" + fileNamesave;
   String[] cnts = model.FileContent.Split("base64,");
   byte[] data = Convert.FromBase64String(cnts[1]);
   using (System.IO.FileStream stream = System.IO.File.Create(path))
   {
    stream.Write(data, 0, data.Length);
   }
   if (model.Mode == "upload")
   {
    string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
    MySqlConnection connection = new MySqlConnection(cs);
    connection.Open();
    IDictionary<string, string> companyMap = GetCompanyNameToIdMap(connection);
    IDictionary<string, string> trainingMap = GetTrainingNameToIdMap(connection);
    var trainingindexCommand = $"select training_index from DOCUMENT_MANAGEMENT.trainingdetails_header where training_id ={trainingMap[model.Training.ToUpper()]} and company_id = {companyMap[model.Company.ToUpper()]} and version={model.Version}";
    Console.WriteLine(trainingindexCommand);
    int trainingindex = -1;
    Console.WriteLine(path);
    using (var getTrainingIndexCmd = new MySqlCommand(trainingindexCommand, connection))
    {
     using (MySqlDataReader reader = getTrainingIndexCmd.ExecuteReader())
     {
      while (reader.Read())
      {
       trainingindex = reader.GetInt32(0);
      }
     }
    }
    if (trainingindex >= 0)
    {
     var uploadatindexCommand = @" INSERT INTO DOCUMENT_MANAGEMENT.trainingdetails_data(`Training_index`,`Filepath`,`minimum_version`,`file_name`) values(" + trainingindex + @",""" + path + @""",""" + model.MinVersion + @""",""" + model.FileName + @""")";
     Console.WriteLine(uploadatindexCommand);
     using var insertTrainingData = new MySqlCommand(uploadatindexCommand, connection);
     insertTrainingData.ExecuteNonQuery();
    }
    else
    {
        Console.WriteLine(trainingindex);
     var uploadHeader = @" INSERT INTO DOCUMENT_MANAGEMENT.trainingdetails_header(`Company_ID`,`Version`,`Training_ID`) values(" + companyMap[model.Company.ToUpper()] + @",""" + model.Version + @"""," + trainingMap[model.Training.ToUpper()] + @")";
    Console.WriteLine(uploadHeader);
     var indexReadBack = -1;
     using var insertTrainingHeaderCommand = new MySqlCommand(uploadHeader, connection);
    insertTrainingHeaderCommand.ExecuteNonQuery();
    var getIndex=$"select training_index from DOCUMENT_MANAGEMENT.trainingdetails_header where training_id ={trainingMap[model.Training.ToUpper()]} and company_id = {companyMap[model.Company.ToUpper()]} and version={model.Version}";
   


using (var getIndexCmd = new MySqlCommand(getIndex, connection))
    {
     using (MySqlDataReader reader = getIndexCmd.ExecuteReader())
     {
      while (reader.Read())
      {
       indexReadBack = reader.GetInt32(0);
       Console.WriteLine(indexReadBack);
      }
     }
    }



    //  using (MySqlDataReader reader = insertTrainingHeaderCommand.ExecuteReader())
    //  {
    //   while (reader.Read())
    //   {
    //    indexReadBack = reader.GetInt32(0);
    //    Console.WriteLine(indexReadBack);
    //   }
    //  }
     var uploadatindexCommand = @" INSERT INTO DOCUMENT_MANAGEMENT.trainingdetails_data(`Training_index`,`Filepath`,`minimum_version`,`file_name`) values(" + indexReadBack + @",""" + path + @""",""" + model.MinVersion + @""",""" + model.FileName + @""")";
     using var insertTrainingDataCmd = new MySqlCommand(uploadatindexCommand, connection);
     insertTrainingDataCmd.ExecuteNonQuery();
    }
    connection.Close();
   }
   else
   {
    //edit file path and minimum version
    Console.WriteLine("edit");
    string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
    MySqlConnection con = new MySqlConnection(cs);
    con.Open();
    int companyId = 0;
    int trainingId = 0;
    var companyCommand = "select company_id from DOCUMENT_MANAGEMENT.company where company_name = " + @"""" + model.Company + @"""";
    var trainingCommand = "select training_id from DOCUMENT_MANAGEMENT.training where training_name = " + @"""" + model.Training + @"""";
    using var cmd = new MySqlCommand(companyCommand, con);
    using MySqlDataReader rdr1 = cmd.ExecuteReader();
    while (rdr1.Read())
    {
     companyId = rdr1.GetInt32(0);
     Console.WriteLine(companyId);
    }
    con.Close();
    con.Open();
    using var cmd2 = new MySqlCommand(trainingCommand, con);
    using MySqlDataReader rdr2 = cmd2.ExecuteReader();
    while (rdr2.Read())
    {
     trainingId = rdr2.GetInt32(0);
     Console.WriteLine(trainingId);
    }
    con.Close();
    con.Open();
    var trainingindexCommand = @"select training_index from DOCUMENT_MANAGEMENT.trainingdetails_header where training_id =" + trainingId + " and company_id =" + companyId + " and version=" + model.Version;
    Console.WriteLine(trainingindexCommand);
    int trainingindex = 0;
    using var cmd3 = new MySqlCommand(trainingindexCommand, con);
    using MySqlDataReader rdr3 = cmd3.ExecuteReader();
    if (rdr3.HasRows)
    {
     while (rdr3.Read())
     {
      trainingindex = rdr3.GetInt32(0);
      Console.WriteLine(trainingindex);
     }
     con.Close();
     con.Open();
     var uploadatindexCommand = @"UPDATE DOCUMENT_MANAGEMENT.trainingdetails_data SET Filepath=""" + path + @"""" + @" , minimum_version=""" + model.MinVersion + @""" where Training_index=" + trainingindex + @" and file_name=""" + model.FileName + @"""";
     Console.WriteLine(uploadatindexCommand);
     using var cmd4 = new MySqlCommand(uploadatindexCommand, con);
     Console.WriteLine("abcd");
     // using MySqlDataReader rdr4 = cmd4.ExecuteReader();
     // SqlCommand cmd = new SqlCommand(str, con);
     cmd4.ExecuteNonQuery();

    }
   }
   return Ok();
  }



  [HttpGet()]
  [Route("filedelete")]
  public String GetFileDelete(string file)
  {
   Console.WriteLine(file);
   string[] fileProperties = file.Split("|");
   string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
   MySqlConnection connection = new MySqlConnection(cs);
   connection.Open();
   IDictionary<string, string> companyMap = GetCompanyNameToIdMap(connection);
   IDictionary<string, string> trainingMap = GetTrainingNameToIdMap(connection);
   string getIndexToDeleteSmt = "SELECT trainingdetails_header.Training_index  FROM DOCUMENT_MANAGEMENT.trainingdetails_header WHERE trainingdetails_header.Company_ID=" + companyMap[fileProperties[2]] + " and  trainingdetails_header.Version=" + fileProperties[1] + " and trainingdetails_header.Training_ID=" + trainingMap[fileProperties[3]];
   var deletingIndex = -1;
   using (var getIndexToDeleteCmd = new MySqlCommand(getIndexToDeleteSmt, connection))
   {
    using (MySqlDataReader reader = getIndexToDeleteCmd.ExecuteReader())
    {
     while (reader.Read())
     {
      deletingIndex = reader.GetInt32(0);
     }
    }
   }
   string deleterowCommand = "DELETE FROM DOCUMENT_MANAGEMENT.trainingdetails_data where Training_index=" + deletingIndex + " and file_name=" + @"'" + fileProperties[0] + @"'";
   using (var deleteCommand = new MySqlCommand(deleterowCommand, connection))
   {
    deleteCommand.ExecuteNonQuery();
   }
   connection.Close();
   return "File data deleted successfully";
  }

  [HttpGet]
  [Route("getdefaults")]
  public IList<FileModel> GetDefaults(string initialize)
  {
   string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
   MySqlConnection connection = new MySqlConnection(cs);
   connection.Open();
   IList<FileModel> fileModels = GetAllFileModels(connection);
   IDictionary<string, string> companyMap = GetCompanyIdToNameMap(connection);
   IDictionary<string, string> trainingMap = GetTrainingIdToNameMap(connection);
   foreach (var model in fileModels)
   {
    model.Company = companyMap[model.Company];
    model.Training = trainingMap[model.Training];
   }
   connection.Close();
   return fileModels;
  }

  private IDictionary<string, string> GetCompanyIdToNameMap(MySqlConnection connection)
  {
   IDictionary<string, string> companyMap = new Dictionary<string, string>();
   using (MySqlCommand readAllCompaniesCommand = connection.CreateCommand())
   {
    readAllCompaniesCommand.CommandText = "select * from DOCUMENT_MANAGEMENT.company";
    using (var reader = readAllCompaniesCommand.ExecuteReader())
    {
     while (reader.Read())
     {
      companyMap.Add(reader.GetInt32(0).ToString(), reader.GetString(1).ToString());
     }
    }
   }
   return companyMap;
  }

  private IDictionary<string, string> GetTrainingIdToNameMap(MySqlConnection connection)
  {
   IDictionary<string, string> trainingMap = new Dictionary<string, string>();
   using (MySqlCommand readAllTrainingCommand = connection.CreateCommand())
   {
    readAllTrainingCommand.CommandText = "select * from DOCUMENT_MANAGEMENT.training";
    using (var reader = readAllTrainingCommand.ExecuteReader())
    {
     while (reader.Read())
     {
      trainingMap.Add(reader.GetInt32(0).ToString(), reader.GetString(1).ToString());
     }
    }
   }
   return trainingMap;
  }

  private IDictionary<string, string> GetCompanyNameToIdMap(MySqlConnection connection)
  {
   IDictionary<string, string> companyMap = new Dictionary<string, string>();
   using (MySqlCommand readAllCompaniesCommand = connection.CreateCommand())
   {
    readAllCompaniesCommand.CommandText = "select * from DOCUMENT_MANAGEMENT.company";
    using (var reader = readAllCompaniesCommand.ExecuteReader())
    {
     while (reader.Read())
     {
      string id = reader.GetInt32(0).ToString();
      string name = reader.GetString(1).ToString();
      companyMap.Add(name, id);
     }
    }
   }
   return companyMap;
  }

  private IDictionary<string, string> GetTrainingNameToIdMap(MySqlConnection connection)
  {
   IDictionary<string, string> trainingMap = new Dictionary<string, string>();
   using (MySqlCommand readAllTrainingCommand = connection.CreateCommand())
   {
    readAllTrainingCommand.CommandText = "select * from DOCUMENT_MANAGEMENT.training";
    using (var reader = readAllTrainingCommand.ExecuteReader())
    {
     while (reader.Read())
     {
      string id = reader.GetInt32(0).ToString();
      string name = reader.GetString(1).ToString();
      trainingMap.Add(name, id);
     }
    }
   }
   return trainingMap;
  }

  private IList<FileModel> GetAllFileModels(MySqlConnection connection)
  {
   IList<FileModel> fileModels = new List<FileModel>();
   // Get all file models.
   using (MySqlCommand readAllFileModelsCommand = connection.CreateCommand())
   {
    readAllFileModelsCommand.CommandText = "select * from DOCUMENT_MANAGEMENT.trainingdetails_header INNER JOIN DOCUMENT_MANAGEMENT.trainingdetails_data where trainingdetails_header.Training_index=trainingdetails_data.Training_index";
    using (var reader = readAllFileModelsCommand.ExecuteReader())
    {
     if (reader.HasRows)
     {
      while (reader.Read())
      {
       FileModel item = new FileModel();
       item.FileName = reader.GetString(8);
       item.Company = reader.GetInt32(1).ToString();
       item.Version = reader.GetString(2);
       item.Training = reader.GetInt32(3).ToString();
       item.FileContent = reader.GetString(6);
       item.MinVersion = reader.GetString(7);
       item.Mode = "";
       fileModels.Add(item);
       Console.WriteLine(item.FileContent);
      }
     }
    }
   }
   return fileModels;
  }
 }
}