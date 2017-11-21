using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTALSGatewayRepository
{
    [Table("GatewayInit")]
    public class GatewayInit
        {
            [Key]
            public int ID { get; set; }
            public DateTime StartUp { get; set; }
            public string URL { get; set; }          
        }
}