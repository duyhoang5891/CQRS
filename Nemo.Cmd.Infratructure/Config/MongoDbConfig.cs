using System;
namespace Nemo.Cmd.Infratructure.Config
{
    public class MongoDbConfig
    {
        public string ConnectionString { get; set; }
        public string DataBase { get; set; }
        public string Collection { get; set; }
    }
}

