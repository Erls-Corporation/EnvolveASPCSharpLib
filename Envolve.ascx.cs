using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnvolveAPI
{
    public partial class EnvolveControl : System.Web.UI.UserControl
    {
        public string APIKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfileHoverHTML { get; set; }
        private List<EnvolveChat> chats = new List<EnvolveChat>();
        public List<EnvolveChat> Chats { get { return chats; } }
        public bool AdminMode { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            // We expect there to be exactly one instance of EnvolveControl on the page.  We cache it in Page.Items so
            // that we can get to it from instances of EnvolveEmbeddedChat later.  And we cache it during Page_Init so 
            // it's reliably available during Page_Load.
            if (this.Page.Items.Contains("envolveControl"))
            {
                throw new Exception("Multiple instances of EnvolveControl are on the page.  You must only include it once.");
            }

            this.Page.Items["envolveControl"] = this;
        }

        // We use Page_PreRender to set the contents of embeddedJavascript to ensure Page_Load for all 
        // EnvolveEmbeddedChatControls has completed.
        protected void Page_PreRender(object sender, EventArgs e)
        {
            EnvolveAPIEncoder encoder = new EnvolveAPIEncoder(this.APIKey);

            this.embeddedJavascript.Text = "envoSn=" + encoder.SiteID + ";\n" +
                "env_commandString=\"" + (this.FirstName == null ? encoder.GetLogoutCommand() :
                encoder.GetLoginCommand(this.FirstName, this.LastName, this.ProfilePicture, this.ProfileHoverHTML, this.AdminMode)) + "\";\n" +
                "envoOptions=" + encoder.GetOptions(chats) + ";\n";
        }

        public static EnvolveControl GetEnvolveControlForPage(Page page)
        {
            return (EnvolveControl)page.Items["envolveControl"];
        }
    }
}
