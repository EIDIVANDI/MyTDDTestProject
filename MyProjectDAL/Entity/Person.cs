using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProjectDAL.Entity
{
    public class Person
    {
        public string PersonName { get; set; }

        public string PersonEmail { get; set; }

        public virtual Person Parent { get; set; }

    }
}
