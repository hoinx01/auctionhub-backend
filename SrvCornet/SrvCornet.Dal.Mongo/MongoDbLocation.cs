using System;
using System.Collections.Generic;
using System.Text;

namespace SrvCornet.Dal.Mongo
{
    public class MongoDbLocation
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public bool Ssl { get; set; }
    }
}
