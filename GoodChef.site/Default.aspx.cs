using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ToolsDll;

namespace GoodChef.site
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            parseHTMLContent pc = new parseHTMLContent();
            pc.loadFromFile();
        }
    }
}
