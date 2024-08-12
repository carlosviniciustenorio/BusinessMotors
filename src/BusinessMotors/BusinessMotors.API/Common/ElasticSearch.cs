using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessMotors.API.Common
{
    public class ElasticSearch
    {
        public string Endpoint { get; set; }
        public string[] Index { get; set; }
    }
}