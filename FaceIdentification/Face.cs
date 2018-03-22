using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceIdentification
{
    public class Face
    {
        public Guid FaceId { get; set; }

        public IList<Person> Candidates { get; set; }

        public Person BestCandidate
        {
            get
            {
                if (this.Candidates.Any())
                {
                    return this.Candidates?.OrderByDescending(c => c.Confidence).First();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
