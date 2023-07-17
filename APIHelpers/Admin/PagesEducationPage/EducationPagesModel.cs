using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers.Admin.PagesEducationPage
{
    public class EducationPagesModel
    {
        public class PagesDependencies
        {
            public int PageId { get; set; }
            public List<int> MembershipsIds { get; set; }
        }
    }
}
