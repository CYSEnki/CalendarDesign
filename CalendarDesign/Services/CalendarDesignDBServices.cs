using CalendarDesign.Models;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace CalendarDesign.Services
{
    public class CalendarDesignDBServices
    {

        //建立與資料庫的連線字串
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["CalendarModel"].ConnectionString;

        //建立與資料庫的連線
        private readonly SqlConnection conn = new SqlConnection(cnstr);


        //藉由日期取得當日陣列資料的方法(沒有用)
        #region 查詢當日陣列資料
        //public List<CalendarDT> GetDataByDate(string year, string month, string day)
        //{
        //    List<CalendarDT> DataList = new List<CalendarDT>();
        //    string Date = year + "-" + month + "-" + day;
        //    DateTime NewDate = Convert.ToDateTime(Date);
        //    Sql語法

        //    string sql = $@"SELECT * FROM  CalendarDT WHERE Date = {NewDate}; ";
        //    確保程式不會因執行錯誤而整個中斷
        //    try
        //    {
        //        開啟資料庫連線D
        //        conn.Open();
        //        執行Sql指令
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        取得Sql資料
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())        //獲得下一筆資料直到沒有資料
        //        {
        //            CalendarDT Data = new CalendarDT();
        //            Data.UID = Convert.ToInt32(dr["UID"]);
        //            Data.Date = Convert.ToDateTime(dr["Date"]);
        //            Data.Title = dr["Title"].ToString();
        //            Data.Status = dr["Status"].ToString();
        //            Data.Sort = dr["Sort"].ToString();
        //            Data.StartTime = Convert.ToDateTime(dr["StartTime"]);
        //            Data.EndTime = Convert.ToDateTime(dr["EndTime"]);
        //            Data.Article = dr["Article"].ToString();
        //            DataList.Add(Data);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        丟出錯誤
        //        throw new Exception(e.Message.ToString());
        //    }
        //    finally
        //    {
        //        關閉資料庫連線
        //        conn.Close();
        //    }
        //    return DataList;
        //}
        #endregion


        ////新增資料方法
        #region 新增行程
        public void AddSchedule(CalendarDT NewData)
        {
            DateTime NewDate = Convert.ToDateTime(NewData.Date);
            DateTime NewStart = Convert.ToDateTime(NewData.StartTime);
            DateTime NewEnd = Convert.ToDateTime(NewData.EndTime) ;
            string date = NewDate.ToString("yyyy-MM-dd");
            string start = NewDate.ToString("yyyy-MM-dd") + " " + NewStart.ToString("HH:mm:ss");
            string end = NewDate.ToString("yyyy-MM-dd") + " " +  NewEnd.ToString("HH:mm:ss");

            //Sql新增語法
            //設定新增時間為現在
            string nowtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string sql = $@"INSERT INTO CalendarDT(Title, Date, Status, Sort, StartTime, EndTime, Article) VALUES ('{NewData.Title}', '{date}', '{NewData.Status}', '{NewData.Sort}', '{start}', '{end}', '{NewData.Article}')";
            //確保程式不會因執行錯誤而整個中斷

            try
            {
                //開啟資料庫連線D
                conn.Open();
                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
        }
        #endregion

        //藉由編號取得單筆資料的方法
        #region 查詢一筆資料
        public CalendarDT  GetDataById(int UID)
        {
            CalendarDT Data = new CalendarDT();
            //Sql語法
            string sql = $@"SELECT * FROM  CalendarDT WHERE UID = {UID}; ";
            //確保程式不會因執行錯誤而整個中斷
            try
            {
                //開啟資料庫連線D
                conn.Open();
                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);
                //取得Sql資料
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.UID = Convert.ToInt32(dr["UID"]);
                Data.Date = Convert.ToDateTime(dr["Date"]);
                Data.Title = dr["Title"].ToString();
                Data.Status = dr["Status"].ToString();
                Data.Sort = dr["Sort"].ToString();
                Data.StartTime = Convert.ToDateTime(dr["StartTime"]);
                Data.EndTime = Convert.ToDateTime(dr["EndTime"]);
                Data.Article = dr["Article"].ToString();
            }
            catch (Exception e)
            {
                //查無資料
                Data = null;
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
            return Data;
        }
        #endregion


        //修改行程方法
        #region 修改行程
        public void UpdateSchedule(CalendarDT UpdateData)
        {
            DateTime NewDate = Convert.ToDateTime(UpdateData.Date);
            DateTime NewStart = Convert.ToDateTime(UpdateData.StartTime);
            DateTime NewEnd = Convert.ToDateTime(UpdateData.EndTime);
            string date = NewDate.ToString("yyyy-MM-dd HH:mm:ss");
            string start = NewStart.ToString("yyyy-MM-dd HH:mm:ss");
            string end = NewEnd.ToString("yyyy-MM-dd HH:mm:ss");

            //Sql修改語法
            string sql = $@"UPDATE CalendarDT SET Title = '{UpdateData.Title}',Date = '{date}', Status = '{UpdateData.Status}', Sort = '{UpdateData.Sort}', Article = '{UpdateData.Article}', StartTime = '{start}', EndTime = '{end}' WHERE UID = {UpdateData.UID}; ";
            //確保程式不會因執行錯誤而整個中斷

            try
            {
                //開啟資料庫連線D
                conn.Open();
                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
        }
        #endregion


        //刪除行程方法
        #region 刪除資料
        public void DeleteSchedule(int UID)
        {
            //Sql刪除語法
            //根據Id取得要刪除的資料
            string sql = $@"DELETE FROM CalendarDT WHERE UID = {UID}; ";
            //確保程式不會因執行錯誤而整個中斷

            try
            {
                //開啟資料庫連線D
                conn.Open();
                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
        }
        #endregion


    }
}