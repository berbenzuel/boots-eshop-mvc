namespace BootEshop.Models;

public class DatabaseConfig
{
    public string Name { get; set; }
    public ConnectionConfig Connection { get; set; }
    //server=mysqlstudenti.litv.sssvt.cz;database=yourdatabase;user=yourusername;password=yourpassword;
    
    public string ConnectionString => 
        $"server={this.Connection.Server};database={this.Name};user={this.Connection.UserName};password={this.Connection.Password};";
    
}