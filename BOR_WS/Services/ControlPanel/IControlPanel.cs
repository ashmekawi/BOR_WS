using BOR_WS.Modules.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.ControlPanel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IControlPanel" in both code and config file together.
    [ServiceContract]
    public interface IControlPanel
    {
        [OperationContract]
        ControlPanelResponse ControlPanel(int id);
    }
}
