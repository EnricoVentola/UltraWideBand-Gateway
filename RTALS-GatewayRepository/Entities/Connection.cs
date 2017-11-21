using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTALSGatewayRepository
{
    [Table("WS_Connection")]
        public class Connection
        {
            public string ConnectionID { get; set; }
            public string UserAgent { get; set; }
            public DateTime ConnTimestamp { get; set; }
            public DateTime? DisConnTimestamp { get; set; }
            public bool Connected { get; set; }
        }
}