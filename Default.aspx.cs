using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnvolveDemo
{
    public partial class DefaultWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Perform sign on operations and then provide profile details (name, picture, etc.) to the Envolve Control.
            this.Envolve.FirstName = "Happy";
            this.Envolve.LastName = "Go Lucky";
            this.Envolve.AdminMode = true;
            this.Envolve.ProfileHoverHTML = "<div style='color: red;'>I love cats!</div>";
            this.Envolve.ProfilePicture = "http://d.envolve.com/anon_user_pic.png";
        }
    }
}
