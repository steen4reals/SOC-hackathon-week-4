using System.Data;
using Npgsql;

public class BaseRepository {

    private NpgsqlConnection SqlConnection(){
        var stringBuilder = new NpgsqlConnectionStringBuilder {
            Host = "ec2-54-170-123-247.eu-west-1.compute.amazonaws.com",
            Database = "dftnn6u8o6v7rr",
            Username = "nsuirvgpxhlmsl",
            Password = "01a6b5616e688bcb4d9b133517823b6c0d4218e5891e29d96cf444fe95278b46",
            Port = 5432,
            SslMode = Npgsql.SslMode.Require, 
            TrustServerCertificate = true
        };
        return new NpgsqlConnection(stringBuilder.ConnectionString);
    }

    public IDbConnection CreateConnection(){
        var connection = SqlConnection();
        connection.Open();
        return connection;
    }
}