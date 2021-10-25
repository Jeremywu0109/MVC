using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using prj1081746FinalExam.Models;

namespace prj1081746FinalExam.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        dbFinalExamEntities db = new dbFinalExamEntities(); //引用database命名空間

        String constr = @"Server=140.138.144.66\SQL1422;" +
            "database=1092netdbB;" +
            "uid=1092netdbB;" +
            "pwd=yzuim1092netdbB";

        private void ExecuteCmd(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = constr;
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public ActionResult Index() //第一題
        {
            //將資料取出並且排序
            var customer = db.TableFinalExams1081746.OrderByDescending(TableFinalExams1081718 => TableFinalExams1081718.訂單日期);
            return View(customer);
        }
        public ActionResult Index2()    //第二題
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShowCity(string City)
        {
            //取出資料並且LIST出來
            var customer = db.TableFinalExams1081746
                .OrderBy(TableFinalExams1081746 => TableFinalExams1081746.訂單號碼)
                .ToList()
                .Where(TableFinalExams1081746 => City == TableFinalExams1081746.送貨城市);
            return View(customer);
        }

        public ActionResult Index4(int EMP_ID = 1) //第三題
        {
            ViewBag.EMP_ID = db.TableFinalExams1081746
                .Where(m => m.員工編號 == EMP_ID)
                .FirstOrDefault().員工編號;
            Employee emp = new Employee()
            {
                 employee = db.TableFinalExams1081746.Where(m => m.員工編號 == EMP_ID).ToList()
            };
            return View(emp);
        }

        public ActionResult Delete(int ORDER_ID)
        {
            var order = db.TableFinalExams1081746.Where(m => m.訂單號碼 == ORDER_ID).FirstOrDefault();
            db.TableFinalExams1081746.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index4");
        }

        private Customer GetCustomer(int ORDER_ID)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = constr;
            SqlCommand cmd = new SqlCommand
                ("SELECT * FROM TableFinalExams1081746 WHERE 訂單號碼=@訂單號碼 ", con);
            cmd.Parameters.Add(new SqlParameter
                ("@訂單號碼", SqlDbType.Int)).Value = ORDER_ID;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            Customer cus = new Customer
            {
                ORDER_ID = Int32.Parse(dt.Rows[0]["訂單號碼"].ToString()),
                CUS_ID = dt.Rows[0]["客戶編號"].ToString(),
                EMP_ID= Int32.Parse(dt.Rows[0]["員工編號"].ToString()),
                INV_DAT = DateTime.Parse(dt.Rows[0]["訂單日期"].ToString()),
                PAY_Day = DateTime.Parse(dt.Rows[0]["要貨日期"].ToString()),
                DEV_Day = DateTime.Parse(dt.Rows[0]["送貨日期"].ToString()),
                DEVWAY = Int32.Parse(dt.Rows[0]["送貨方式"].ToString()),
                CHARGE = Decimal.Parse(dt.Rows[0]["運費"].ToString()),
                RECEIVER = dt.Rows[0]["收貨人"].ToString(),
                ADR = dt.Rows[0]["送貨地址"].ToString(),
                City = dt.Rows[0]["送貨城市"].ToString(),
                District = dt.Rows[0]["送貨行政區"].ToString(),
                Portocal = dt.Rows[0]["送貨郵遞區號"].ToString(),
                Country = dt.Rows[0]["送貨國家地區"].ToString()
            };
            return cus;
        }

        public ActionResult Edit(int ORDER_ID)
        {
            /*var customers = db.TableFinalExams1081746
                .Where(m => m.訂單號碼 == ORDER_ID).FirstOrDefault();
            return View(customers);*/
            return View(GetCustomer(ORDER_ID));
        }
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                /*var Temp = db.TableFinalExams1081746
                    .Where(m => m.訂單號碼 == customer.ORDER_ID)
                    .FirstOrDefault();
                //將編輯資料丟入model
                Temp.客戶編號 = customer.CUS_ID;
                Temp.員工編號 = customer.EMP_ID;
                Temp.訂單日期 = customer.INV_DAT;
                Temp.要貨日期 = customer.PAY_Day;
                Temp.送貨日期 = customer.DEV_Day;
                Temp.送貨方式 = customer.DEVWAY;
                Temp.運費 = customer.CHARGE;
                Temp.收貨人 = customer.RECEIVER;
                Temp.送貨地址 = customer.ADR;
                Temp.送貨城市 = customer.City;
                Temp.送貨行政區 = customer.District;
                Temp.送貨郵遞區號 = customer.Portocal;
                Temp.送貨國家地區 = customer.Country;
                db.SaveChanges();
                */

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText =
                    "UPDATE TableFinalExams1081746 SET 客戶編號=@客戶編號, 員工編號= @員工編號, 訂單日期= @訂單日期, " +
                    " 要貨日期=@要貨日期,送貨日期=@送貨日期, 送貨方式 = @送貨方式 ,運費 = @運費," +
                    " 收貨人 =@收貨人, 送貨地址 = @送貨地址, 送貨城市 = @送貨城市, 送貨行政區= @送貨行政區, 送貨郵遞區號 = @送貨郵遞區號, 送貨國家地區=@送貨國家地區 " 
                    + "WHERE 訂單號碼 = @訂單號碼";
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@訂單號碼", SqlDbType.Int)).Value = customer.ORDER_ID;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@客戶編號", SqlDbType.NVarChar)).Value = customer.CUS_ID;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@員工編號", SqlDbType.Int)).Value = customer.EMP_ID;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@訂單日期", SqlDbType.DateTime)).Value = customer.INV_DAT;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@要貨日期", SqlDbType.DateTime)).Value = customer.PAY_Day;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@送貨日期", SqlDbType.DateTime)).Value = customer.DEV_Day;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@送貨方式", SqlDbType.Int)).Value = customer.DEVWAY;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@運費", SqlDbType.Money)).Value = customer.CHARGE;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@收貨人", SqlDbType.NVarChar)).Value = customer.RECEIVER;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@送貨地址", SqlDbType.NVarChar)).Value = customer.ADR;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@送貨城市", SqlDbType.NVarChar)).Value = customer.City;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@送貨行政區", SqlDbType.NVarChar)).Value = customer.District;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@送貨郵遞區號", SqlDbType.NVarChar)).Value = customer.Portocal;
                sqlCommand.Parameters.Add(new SqlParameter
                    ("@送貨國家地區", SqlDbType.NVarChar)).Value = customer.Country;

                ExecuteCmd(sqlCommand);
                /* 
                    [訂單號碼]   INT           NOT NULL,
                    [客戶編號]   NVARCHAR (5)  NULL,
                    [員工編號]   INT           NULL,
                    [訂單日期]   DATETIME      NULL,
                    [要貨日期]   DATETIME      NULL,
                    [送貨日期]   DATETIME      NULL,
                    [送貨方式]   INT           NULL,
                    [運費]     MONEY         NULL,
                    [收貨人]    NVARCHAR (40) NULL,
                    [送貨地址]   NVARCHAR (60) NULL,
                    [送貨城市]   NVARCHAR (15) NULL,
                    [送貨行政區]  NVARCHAR (15) NULL,
                    [送貨郵遞區號] NVARCHAR (10) NULL,
                    [送貨國家地區] NVARCHAR (15) NULL,
                 */
                return RedirectToAction("Index4");
            }
            return View(customer);
        }

        public ActionResult Index5() //第四題
        {
            return View();
        }
        [HttpPost]
        public ActionResult QueryByID(string address)
        {
            var goal = from m in db.TableFinalExams1081746
                    select m;
            goal = goal.Where(m => m.送貨地址.Contains(address)); //如果address被包含在資料裡則輸出
            return View(goal);
        }

        public ActionResult Index6()    //第五題
        {
            return View();
        }
        [HttpPost]
        public ActionResult QueryByID1(int? order, string cus)
        {
            var customer = db.TableFinalExams1081746.OrderBy(TableFinalExams1081746 => TableFinalExams1081746.訂單號碼)
                .Where(TableFinalExams1081746 => order == TableFinalExams1081746.訂單號碼 && cus == TableFinalExams1081746.客戶編號);
            return View(customer);
        }
    }
}