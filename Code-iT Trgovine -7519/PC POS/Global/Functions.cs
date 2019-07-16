using System;
using System.Data;

namespace PCPOS.Global
{
    public static class Functions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDateParam()
        {
            DataTable DTpocetno = Database.GetPocetno();
            return DTpocetno.Rows.Count > 0 ? $"{DTpocetno.Rows[0]["datum"].ToString()}" : $"{DateTime.Now.Year}-01-01 00:00:00";
        }
    }
}
