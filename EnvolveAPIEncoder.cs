using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace EnvolveAPI
{
    public struct EnvolveChat
    {
        public string Tag;
        public string Name;
        public string DivId;

        public string AsJSON()
        {
            return String.Format("{{ tag : '{0}', name : '{1}', divID : '{2}' }}",
                HttpUtility.HtmlEncode(Tag), HttpUtility.HtmlEncode(Name), HttpUtility.HtmlEncode(DivId));
        }
    }

    public class EnvolveAPIEncoder
    {
        private const string API_VERSION = "0.2";
        private string secretKey;

        public int SiteID { protected set; get; }

        public EnvolveAPIEncoder(String apiKey)
        {
            //parse the API key
            String[] pieces = apiKey.Split('-');
            int siteID;
            if (pieces.Length != 2 || !int.TryParse(pieces[0], out siteID))
            {
                throw new Exception("Invalid Envolve API Key");
            }

            this.SiteID = siteID;
            this.secretKey = pieces[1];
        }
		 
        public String GetLoginCommand(String firstName, String lastName, String picture, string profileHoverHTML, bool isAdmin)
	    {
		    if(firstName == null)
		    {
		    	throw new Exception("You must provide at least a first name. If you are providing a username, use it for the first name and set the last name to null");
	        }
		
		    String commandString = "v=" + API_VERSION + ",c=login,fn=" + this.base64Encode(firstName);
		    if(lastName != null)
		    {
			    commandString += ",ln=" + this.base64Encode(lastName);
		    }
		    if(picture != null)
		    {
			    commandString += ",pic=" + this.base64Encode(picture);
		    }
            if (profileHoverHTML != null)
            {
                commandString += ",prof=" + this.base64Encode(profileHoverHTML);
            }
		    if(isAdmin)
		    {
			    commandString += ",admin=t";
		    }
		    return this.signCommand(commandString);
	    }

	    public String GetLogoutCommand()
	    {
		    return this.signCommand("c=logout");
	    }

        public string GetOptions(List<EnvolveChat> chats)
        {
            return string.Format("{{ groups : [], chats : [{0}] }}", string.Join(", ", chats.Select(chat => chat.AsJSON())));
        }
	
	    private String signCommand(String command)
	    {
            //convert to a unix "epoch" time format.
            String c = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000) + ";" + command;

            String hash = this.calculateHMAC_SHA1Hash(c).ToLower();

		    return hash + ";" + c;
	    }
	
	    private string calculateHMAC_SHA1Hash(string text)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(this.secretKey);
            HMACSHA1 cryptoService = new HMACSHA1(keyBytes);
            byte[] rawHash = cryptoService.ComputeHash(bytes);
            string hash = ByteToString(rawHash);

            return hash;
        }

        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        private string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = System.Text.Encoding.UTF8.GetBytes(data);    
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch(Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }
    }
}
