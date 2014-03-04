using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnvolveAPI
{
    public partial class EnvolveEmbeddedChat : System.Web.UI.UserControl
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            EnvolveControl envolveControl = EnvolveControl.GetEnvolveControlForPage(Page);
            envolveControl.Chats.Add(
                new EnvolveChat { DivId = chat.ClientID, Tag = this.Tag, Name = this.Name });
            if (!String.IsNullOrEmpty(Style))
            {
                chat.Style.Value = Style;
            }
        }
    }
}
