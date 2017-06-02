using System.ServiceProcess;
using System.Web.Http;

namespace MonitorService
{
    public class StatusController: ApiController
    {
        public static bool CheckStatus(ServiceController controller)
        {
            try
            {
                return controller.Status == ServiceControllerStatus.Running;
            }
            catch
            {
                return false;
            }
        }
    }
}
