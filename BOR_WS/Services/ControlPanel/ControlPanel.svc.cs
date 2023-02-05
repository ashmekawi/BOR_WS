using BOR_WS.DataBase;
using BOR_WS.Modules.ControlPanel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.ControlPanel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ControlPanel" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ControlPanel.svc or ControlPanel.svc.cs at the Solution Explorer and start debugging.
    public class ControlPanel : IControlPanel
    {
        ControlPanelResponse IControlPanel.ControlPanel(int id)
        {
            DBMan db = new DBMan();
            DataSet ds = new DataSet();
            ControlPanelResponse response = new ControlPanelResponse();
            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "SELECT * FROM [dbo].[Request_StatisticMyRequest] (@CustomerID)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", id));
                sqlCommand.Connection = db.databaseConnection;
              
                ds=db.executeSqlCommand(sqlCommand);
                response.RequestsCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                response.BOCount = Convert.ToInt32(ds.Tables[0].Rows[0][3]);
                response.ArggCount = Convert.ToInt32(ds.Tables[0].Rows[0][2]);
                response.LegalCount = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
