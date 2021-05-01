using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Models
{
    public class UserProduct
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
