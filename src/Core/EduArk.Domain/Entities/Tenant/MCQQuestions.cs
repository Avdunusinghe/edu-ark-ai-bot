using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduArk.Domain.Entities.Tenant
{
    public class MCQQuestions : BaseAuditableEntity
    {
        public int AssessmentId { get; set; }
        public int SequenceNo { get; set; }
        public string Text { get; set; }
        public decimal Marks { get; set; }

        public virtual Assessment Assessment { get; set; }
    }
}
