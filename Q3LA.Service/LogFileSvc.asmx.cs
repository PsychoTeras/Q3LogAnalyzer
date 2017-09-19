using System.IO;
using System.Web.Services;
using Q3LA.Service.Properties;

namespace Q3LA.Service
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class LogFileSvc : WebService
    {
        private static readonly string _logFileName = Settings.Default.LogFilePath;

        [WebMethod]
        public byte[] ReadLogfile()
        {
            try
            {
                FileStream file = File.Open(_logFileName, FileMode.Open, FileAccess.Read);
                using (BinaryReader binReader = new BinaryReader(file))
                {
                    return binReader.ReadBytes((int) binReader.BaseStream.Length);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
