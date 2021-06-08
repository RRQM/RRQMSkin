//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using System;
using System.Text.RegularExpressions;

namespace RRQMSkin
{
    internal static class MetarnetRegex
    {
        /// <summary>
        /// 判断输入的字符串只包含汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsChineseCh(string input)
        {
            Regex regex = new Regex("^[\u4e00-\u9fa5]+$");
            return regex.IsMatch(input);
        }

        ///// <summary>
        ///// 判断输入的字符串只包含汉字
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //internal static bool IsIPv4(string input)
        //{
        //    Regex regex = new Regex(@"(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)");
        //    return regex.IsMatch(input);
        //}

        /// <summary>
        /// 匹配3位或4位区号的电话号码，其中区号可以用小括号括起来，
        /// 也可以不用，区号与本地号间可以用连字号或空格间隔，
        /// 也可以没有间隔
        /// \(0\d{2}\)[- ]?\d{8}|0\d{2}[- ]?\d{8}|\(0\d{3}\)[- ]?\d{7}|0\d{3}[- ]?\d{7}
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsPhone(string input)
        {
            string pattern = "^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7}$|^0\\d{3}[- ]?\\d{7}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsMobilePhone(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^1[3456789]\d{9}$");
        }

        /// <summary>
        /// 验证身份证合理性
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        internal static bool IsIDcard(string input)
        {
            if (input.Length == 18)
            {
                bool check = CheckIDCard18(input);
                return check;
            }
            else if (input.Length == 15)
            {
                bool check = CheckIDCard15(input);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 18位身份证号码验证
        /// </summary>
        private static bool CheckIDCard18(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 16位身份证号码验证
        /// </summary>
        private static bool CheckIDCard15(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;
        }

        /// <summary>
        /// 判断输入的字符串只包含数字
        /// 可以匹配整数和浮点数
        /// ^-?\d+$|^(-?\d+)(\.\d+)?$
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsNumber(string input)
        {
            string pattern = "^-?\\d+$|^(-?\\d+)(\\.\\d+)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 匹配非负整数
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsNotNagtive(string input)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 匹配正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsUint(string input)
        {
            Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串字包含英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsEnglisCh(string input)
        {
            Regex regex = new Regex("^[A-Za-z]+$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个合法的Email地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsEmail(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串是否只包含数字和英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsNumAndEnCh(string input)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个超链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsURL(string input)
        {
            //string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            string pattern = @"^[a-zA-Z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串是否是表示一个IP地址
        /// </summary>
        /// <param name="input">被比较的字符串</param>
        /// <returns>是IP地址则为True</returns>
        internal static bool IsIPv4(string input)
        {
            try
            {
                string[] IPs = input.Split('.');
                Regex regex = new Regex(@"^\d+$");
                for (int i = 0; i < IPs.Length; i++)
                {
                    if (!regex.IsMatch(IPs[i]))
                    {
                        return false;
                    }
                    if (Convert.ToUInt16(IPs[i]) > 255)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// 判断输入的字符串是否是合法的IPV6 地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsIPV6(string input)
        {
            string pattern = "";
            string temp = input;
            string[] strs = temp.Split(':');
            if (strs.Length > 8)
            {
                return false;
            }
            int count = MetarnetRegex.GetStringCount(input, "::");
            if (count > 1)
            {
                return false;
            }
            else if (count == 0)
            {
                pattern = @"^([\da-f]{1,4}:){7}[\da-f]{1,4}$";

                Regex regex = new Regex(pattern);
                return regex.IsMatch(input);
            }
            else
            {
                pattern = @"^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$";
                Regex regex1 = new Regex(pattern);
                return regex1.IsMatch(input);
            }
        }

        /* *******************************************************************
         * 1、通过“:”来分割字符串看得到的字符串数组长度是否小于等于8
         * 2、判断输入的IPV6字符串中是否有“::”。
         * 3、如果没有“::”采用 ^([\da-f]{1,4}:){7}[\da-f]{1,4}$ 来判断
         * 4、如果有“::” ，判断"::"是否止出现一次
         * 5、如果出现一次以上 返回false
         * 6、^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$
         * ******************************************************************/

        /// <summary>
        /// 判断字符串compare 在 input字符串中出现的次数
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="compare">用于比较的字符串</param>
        /// <returns>字符串compare 在 input字符串中出现的次数</returns>
        private static int GetStringCount(string input, string compare)
        {
            int index = input.IndexOf(compare);
            if (index != -1)
            {
                return 1 + GetStringCount(input.Substring(index + compare.Length), compare);
            }
            else
            {
                return 0;
            }
        }
    }
}