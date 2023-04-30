using BOR_WS.EXTFUN;
using BOR_WS.Modules;
using BOR_WS.Modules.AddRequest;
using BOR_WS.Modules.BackEnd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.BackEnd
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BackEnd" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BackEnd.svc or BackEnd.svc.cs at the Solution Explorer and start debugging.
    public class BackEnd : IBackEnd
    {
        CRA00Context db = new CRA00Context();
        CRRB_ServiceContext CRRB = new CRRB_ServiceContext();
        public LoginResponse Login(LoginRequest request)
        {
            string str = "SELECT FULL_NAME as UserName ,[NUMBER0] as UserID FROM [CRA00].[dbo].[OPERATOR] where NAME=N'" + request.UserName + "' and Password= hashbytes('SHA1',N'" + request.Password + "')  and status = 'enable' and officecode = 200";
            LoginResponse response = db.Database.SqlQuery<LoginResponse>(str).FirstOrDefault();
            return response;
        }
        public int RequestInProgress(RequestInProgressRequest request)
        {
            string str = "sp_CRRB_RequestAudit_add " + request.RequestID + "," + request.InProgress + "," + request.UserID + "";
            CRRB.Database.ExecuteSqlCommand(str);
            int x = CRRB.Database.SqlQuery<int>("SELECT [InProgressID] FROM [CRRB_Service].[dbo].[CRRB_Request] where id=" + request.RequestID).FirstOrDefault();
            if (x == request.InProgress)
            {
                return 1;

            }
            else
            {
                return 0;
            }
        }
        public int AddEntity(AddEntityRequest request)
        {
            string str = "exec [dbo].[sp_CRRB_AddEntity_New]  " + request.RequestID + "  ," + request.UserID + "";
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CRRB_ServiceContext"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            try
            {
                con.Open();
                da.Fill(ds);
                con.Close();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }

            return Convert.ToInt32(ds.Tables[ds.Tables.Count].Rows[0]["RecID"]);

        }
        public List<BackEndRequestsReponse> Requests(int Office, int UserID, int InProgress)
        {
            List<BackEndRequestsReponse> response = new List<BackEndRequestsReponse>();
            response = CRRB.Database.SqlQuery<BackEndRequestsReponse>("SELECT * FROM [dbo].[Request_GetAtInProgress]" +
                " ( " + Office + " ," + UserID + "  ," + InProgress + ")").ToList();
            return response;

        }
        public List<Decision> AvailableDecision(int RequestID, int DecisionMaker)
        {
            List<Decision> Decisions = new List<Decision>();
            string str = "SELECT* FROM[dbo].[Request_AvailableDecision] (" + RequestID + "," + DecisionMaker + ")";
            Decisions = CRRB.Database.SqlQuery<Decision>(str).ToList();
            return Decisions;
        }
        public List<Request> GetBackEndRequest(int RequestID, string Nid, string Phon, string UCR)
        {
            List<Request> requests = new List<Request>();
            string str = @"SELECT* FROM[dbo].[Request_GetCustomerRequest]" +
               " (" + RequestID + ",'" + Nid + "','" + Phon + "','" + UCR + "')";
            requests = CRRB.Database.SqlQuery<Request>(str).ToList();
            return requests;
        }
        public Office_GetRequestInfResponse GetBackEndRequestData(int RequestID)
        {
            Office_GetRequestInfResponse response = new Office_GetRequestInfResponse();
            response.RequestInfo = CRRB.Database.SqlQuery<Office_GetRequestInfo>("SELECT * FROM [dbo].[Office_GetRequestInfo] (" + RequestID + ")").ToList();
            response.Files = CRRB.Database.SqlQuery<RequestFile>("SELECT          CRRB_RequestAttachment.Seq" +
" , CRRB_RequestAttachment.img, DocType.Adesc, FExtType.Adesc AS FExtType" +
" FROM            CRRB_RequestAttachment INNER JOIN" +
                         " DocType ON CRRB_RequestAttachment.DocTypeID = DocType.ID INNER JOIN" +
                         " FExtType ON CRRB_RequestAttachment.FExtType = FExtType.ID" +

                        " where CRRB_RequestAttachment.RequestID = " + RequestID).ToList();
            response.Decisions = CRRB.Database.SqlQuery<Decision>("SELECT* FROM[dbo].[Request_AvailableDecision] (" + RequestID + "," + 3 + ")").ToList();
            response.UCR = CRRB.Database.SqlQuery<string>("select cast(UCR as nvarchar) from [CRRB_Request] where id=" + RequestID).FirstOrDefault();
            response.InProgress = CRRB.Database.SqlQuery<int>("select [InProgressID] from [CRRB_Request] where id=" + RequestID).FirstOrDefault();
            response.InProgressDesc = CRRB.Database.SqlQuery<string>("select [Adesc] from [InProgress] where id=" + response.InProgress).FirstOrDefault();
            response.RequestType = CRRB.Database.SqlQuery<string>("SELECT        RequestType.Adesc FROM            RequestType INNER JOIN CRRB_Request ON RequestType.ID = CRRB_Request.RequestTypeID where CRRB_Request.ID=" + RequestID).FirstOrDefault();
            return response;

        }
        public Book GetBOIBook(string BOIID, string UCR, int UserID)
        {
            int exists = CRRB.Database.SqlQuery<int>("SELECT [dbo].[BOIExists] (" + BOIID + "," + UCR + ")").FirstOrDefault();
            Book book = new Book();
            book.Header = new Header();
            book.Footer = new Footer();
            if (exists == 1)
            {
                book.ResponseCode = 200;
                book.ResponeMSG = "Done";
            }
            else
            {
                book.ResponseCode = 201;
                book.ResponeMSG = "هذه المنشأة غير موجودة";
            }
            string str = "SELECT * FROM[dbo].[CRRB_GetBOI_Book] (" + BOIID + ", " + UCR + ")";
            book.BookRow = CRRB.Database.SqlQuery<BookRow>(str).ToList();
            if (BOIID == "0")
            {
                BOIID = Convert.ToString(CRRB.Database.SqlQuery<Decimal>("SELECT cast(BOIID as numeric(18, 0))as BOIID FROM [dbo].[CRRB_GetBOI_ByUCR] (" + UCR + ")").FirstOrDefault());
            }
            string UserName = db.Database.SqlQuery<string>("SELECT [FULL_NAME] FROM [CRA00].[dbo].[OPERATOR] where NUMBER0 =" + UserID).FirstOrDefault();
            book.Footer.FooterRow = CRRB.Database.SqlQuery<FooterRow>("SELECT * FROM CRRB.[dbo].[Pivot_GetBOI_Footer] (" + BOIID + "," + UserID + ",'" + UserName + "')").ToList();
            book.Header.HeaderRow = CRRB.Database.SqlQuery<HeaderRow>("SELECT * FROM CRRB.[dbo].[Pivot_GetBOI_Header] (" + BOIID + "," + UserID + ",'" + UserName + "')").ToList();
            string QRText = book.Header.HeaderRow.Where(c => c.Lbl == "رقم الوثيقة").Select(a => a.ItemValue).FirstOrDefault();
            QRGenerator.QRGeneratorClient qRGenerator = new QRGenerator.QRGeneratorClient();
            book.Header.QRCode=qRGenerator.GenerateQRCode(QRText);
          // System.IO.File.WriteAllBytes(@"c:\Reports\QR.jpg", Convert.FromBase64String(book.Header.QRCode));

            book.Header.QREXT = "jpg";
            return book;

        }
        public Result Payment(int RequestID, decimal TotalFees, int ReceiptNum, int ReceiptGroup, DateTime ReceiptDate, int UserID)
        {
            Result result = new Result();
            int date = int.Parse(ReceiptDate.ToString("yyyyMMdd"));
            string str = "EXECUTE[dbo].[SP_CRRB_RequestPayment_add] " +
            RequestID + "," + TotalFees + "," + ReceiptNum + "," + ReceiptGroup + "," + date + "," + UserID;
            result = CRRB.Database.SqlQuery<Result>(str).FirstOrDefault();
            return result;
        }
    }
}
