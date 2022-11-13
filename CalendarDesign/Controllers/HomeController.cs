using CalendarDesign.Models;
using CalendarDesign.Services;
using CalendarDesign.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalendarDesign.Controllers
{
    public class HomeController : Controller
    {
        //宣告MessageBoard資料表的Service物件
        private readonly CalendarDesignDBServices CalenDesignService = new CalendarDesignDBServices();
        //設一個db存放CalendarModel資料(MonthTitle,StartDayOfWeek,EndDay,CalendarContent(List),AddSchedule)
        CalendarModel db = new CalendarModel();

        //設Index畫面為一開始載入之行事曆
        #region 初始行事曆畫面
        public ActionResult Index(string year, string month)
        {
            //設計月曆表格
            var currentDate = DateTime.Now;
            if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(month))
            {
                currentDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
            }
            var date = new DateTime(currentDate.Year, currentDate.Month, 1);        //此處可以增加減少月份，但年份不變，所以無法直接看到2023年1月

            ViewBag.dateyear = date.Year;        //現在年份
            ViewBag.datemonth = date.Month;      //現在月份

            //Model Binding
            var model = new CalendarModel
            {
                //回傳設計月曆條件
                MonthTitle = date.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture).ToUpper(),     //顯示英文月份(標題)
                StartDayOfWeak = (int)date.DayOfWeek,                                                           //回傳每個月第一天
                EndDay = date.AddMonths(1).AddSeconds(-1).Day,                                                   //回傳每個月最後一天

                //回傳行程表
                CalendarContent =  db.CalendarDT.ToList(),                                 
            };

            ViewBag.MonthTitle = model.MonthTitle;
            return View(model);
        }
        #endregion

        //選取日期後之當日行事曆詳細內容
        #region 當日行事曆畫面
        public ActionResult About(string year, string month, string day)
        {
            //設計月曆表格
            var currentDate = DateTime.Now;
            if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(day))      //如果YYYY-MMM-DD都不是空的
            {
                currentDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));        //設currentDate 為輸入之YYYY-MM-DD
            }
            var date = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);      //新增一變數存取現在(年/月/日)

            ViewBag.Detaildateyear = date.Year;        //現在年份
            ViewBag.Detaildatemonth = date.Month;      //現在月份
            ViewBag.Detaildateday = date.Day;          //現在日期

            var data = new List<CalendarDT>();

            //Model Binding
            var model = new CalendarModel
            {
                //回傳設計月曆條件
                MonthTitle = date.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture).ToUpper(),     //顯示英文月份(標題)

                //回傳行程表
                CalendarContent = db.CalendarDT.ToList(),
            };
            ViewBag.DetailTitle = model.MonthTitle;       //輸出月份要選擇這個是因為有更換過月份格式"MMM"
            return View(model);
        }
        #endregion

        //選取日期後之當日行事曆詳細內容2(沒有用)
        #region 選取當日行程(從Date找資料)
        //public ActionResult SelectSchedule(string year, string month, string day)
        //{
        //    ScheduleViewModel Data = new ScheduleViewModel();
        //    //取得頁面所需資料，藉由Service取得
        //    Data.DataList = CalenDesignService.GetDataByDate(year, month, day);
        //    //將資料傳入View中
        //    return View(Data);
        //}
        #endregion


        //新增行程一開始載入畫面
        #region 新增行程
        public ActionResult Create(string year, string month, string day)
        {
            //部分檢視

            return PartialView();
        }
        #endregion

        //設定此Action只接受頁面POST資料傳入
        //使用Bind的Include來定義只接受的欄位，用來避免傳入其他不相干值
        [HttpPost]
        #region 新增行程
        public ActionResult Create([Bind(Include = "Title,Date,Status,Sort,StartTime,EndTime,Article")] CalendarDT Data)
        {
            //使用Service來新增一筆資料
            CalenDesignService.AddSchedule(Data);

            //重新導向頁面至開始畫面
            return RedirectToAction("Index", "Home");
        }
        #endregion

        //修改行事曆頁面要根據傳入編號來決定要修改的行程
        #region 變動行程(從Id找資料)
        public ActionResult Edit(int UID)
        {
            //取得頁面所需資料，藉由Service取得
            CalendarDT Data = CalenDesignService.GetDataById(UID);
            //將資料傳入View中
            return View(Data);
        }
        #endregion

        //修改行程傳入資料時的Action
        [HttpPost]
        #region 變動行程(傳入資料)
        public ActionResult Edit(int UID, [Bind(Include = "Date, Title, Status, Sort, StartTime, EndTime, Article")] CalendarDT UpdateData)
        {
            //將編號設定至修改資料中
            UpdateData.UID = UID;
            //使用Service來修改資料
            CalenDesignService.UpdateSchedule(UpdateData);
            //重新導向至頁面至開始頁面
            return RedirectToAction("Index");
        }
        #endregion


        //刪除頁面根據傳入編號來刪除資料
        #region 刪除行程
        public ActionResult Delete(int UID)
        {
            //使用Service來刪除資料
            CalenDesignService.DeleteSchedule(UID);
            //重新導向至開始頁面
            return RedirectToAction("Index");
        }
        #endregion
    }
}