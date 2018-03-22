using System;
using System.Collections.Generic;
using System.Text;

namespace FaceIdentification
{
    public class Person
    {
        public Guid PersonId { get; set; }

        public string Name { get; set; }

        public string UserData { get; set; }

        public double Confidence { get; set; }
    }
}
