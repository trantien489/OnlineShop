using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace OnlineShop.Helper
{
    public class StringHelper
    {
        public static string GetMetaTitle(string input)

        {

            input = input.Trim();

            for (int i = 0x20; i < 0x30; i++)

            {

                input = input.Replace(((char)i).ToString(), " ");

            }

            input = input.Replace(".", "-");

            input = input.Replace(" ", "-");

            input = input.Replace(",", "-");

            input = input.Replace(";", "-");

            input = input.Replace(":", "-");

            input = input.Replace("  ", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string str = input.Normalize(NormalizationForm.FormD);

            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');

            while (str2.IndexOf("?") >= 0)

            {

                str2 = str2.Remove(str2.IndexOf("?"), 1);

            }

            while (str2.Contains("--"))

            {

                str2 = str2.Replace("--", "-").ToLower();

            }

            return str2.ToLower();

        }


        public static string GetstringInvoiceStatus(int number)
        {
            if (number == -1)
            {
                return "Bị Hủy";
            }
            else if (number == 0)
            {
                return "Đang xử lý";
            }
            else if (number == 1)
            {
                return "Đang vận chuyển";
            }
            else if (number == 2)
            {
                return "Đã giao hàng";
            }
            else
            {
                return "";
            }
        }

        public static string GetStringStatus(bool status)
        {
            return status ? "Hoạt động" : "Bị khóa";
        }
    }
}