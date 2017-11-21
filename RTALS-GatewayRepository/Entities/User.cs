using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RTALSGatewayRepository
{
        public class User
        {
            [Key]
            public string UserName { get; set; }
          //  public ICollection<Connection> Connections { get; set; }
        }
    }