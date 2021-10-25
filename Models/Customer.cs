using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prj1081746FinalExam.Models
{
    public class Customer
    {
        [DisplayName("訂單號碼")]
        public Int32 ORDER_ID { get; set; }
        [DisplayName("客戶編號")]
        public string CUS_ID { get; set; }
        [DisplayName("員工編號")]
        public Int32 EMP_ID { get; set; }
        [DisplayName("訂單日期")]
        [DataType(DataType.Date, ErrorMessage = "訂單日期必須為日期格式")]
        public Nullable<System.DateTime> INV_DAT { get; set; }

        [DisplayName("要貨日期")]
        [DataType(DataType.Date, ErrorMessage = "訂單日期必須為日期時間格式")]
        public Nullable<System.DateTime> PAY_Day { get; set; }
        [DisplayName("送貨日期")]
        [DataType(DataType.Date, ErrorMessage = "訂單日期必須為日期時間格式")]
        public Nullable<System.DateTime> DEV_Day { get; set; }
        [DisplayName("送貨方式")]
        public Int32 DEVWAY { get; set; }
        [DisplayName("運費")]
        public Decimal CHARGE { get; set; }
        [DisplayName("收貨人")]
        public string RECEIVER { get; set; }
        [DisplayName("送貨地址")]
        public string ADR { get; set; }
        [DisplayName("送貨城市")]
        public string City { get; set; }
        [DisplayName("送貨行政區")]
        public string District { get; set; }
        [DisplayName("送貨郵遞區號")]
        public string Portocal { get; set; }
        [DisplayName("送貨國家地區")]
        public string Country { get; set; }

        
    }
}