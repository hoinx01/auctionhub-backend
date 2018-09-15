using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SrvCornet.Utils
{
    public class StringUtils
    {
        public static string ComputeSha256Hash(string input)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string QuoteString(string inputString)
        {
            string str = inputString.Trim();
            if (str != "")
            {
                str = str.Replace("'", "''");
            }
            return str;
        }

        public static string AddSlash(string input)
        {
            string str = !string.IsNullOrEmpty(input) ? input.Trim() : "";
            if (str != "")
            {
                str = str.Replace("'", "'").Replace("\"", "\\\"");
            }
            return str;
        }

        public static string RemoveStrHtmlTags(object inputObject)
        {
            if (inputObject == null)
            {
                return string.Empty;
            }
            string input = Convert.ToString(inputObject).Trim();
            if (input != "")
            {
                input = Regex.Replace(input, @"<(.|\n)*?>", string.Empty);
            }
            return input;
        }

        public static string ReplaceSpaceToPlus(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Regex.Replace(input, @"\s+", "+", RegexOptions.IgnoreCase);
            }
            return input;
        }

        public static string ReplaceSpecialCharater(object inputObject)
        {
            if (inputObject == null)
            {
                return string.Empty;
            }
            return Convert.ToString(inputObject).Trim().Trim().Replace(@"\", @"\\").Replace("\"", "&quot;").Replace("“", "&ldquo;").Replace("”", "&rdquo;").Replace("‘", "&lsquo;").Replace("’", "&rsquo;").Replace("'", "&#39;");
        }

        public static string JavaScriptSring(string input)
        {
            input = input.Replace("'", @"\u0027");
            input = input.Replace("\"", @"\u0022");
            return input;
        }

        public static int CountWords(string stringInput)
        {
            if (string.IsNullOrEmpty(stringInput))
            {
                return 0;
            }
            stringInput = RemoveStrHtmlTags(stringInput);
            return Regex.Matches(stringInput, @"[\S]+").Count;
        }

        public static string GetEnumDescription(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute),
                        false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            //Debug.Assert(propertyExpression != null, "propertyExpression != null");
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        public static string SubWordInString(object obj, int maxWord, bool removeHTML = false)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (removeHTML) obj = RemoveStrHtmlTags(obj);

            string input = Regex.Replace(Convert.ToString(obj), @"\s+", " ");


            string[] strArray = Regex.Split(input, " ");
            if (strArray.Length <= maxWord)
            {
                return input;
            }
            input = string.Empty;
            for (int i = 0; i < maxWord; i++)
            {
                input = input + strArray[i] + " ";
            }
            return string.Concat(input.Trim(), "...");
        }

        public static string SubWordInDotString(object obj, int maxWord, string extensionEnd = " ...")
        {
            if (obj == null)
            {
                return string.Empty;
            }
            string input = Regex.Replace(Convert.ToString(obj), @"\s+", " ");
            string[] strArray = Regex.Split(input, " ");
            if (strArray.Length <= maxWord)
            {
                return input;
            }
            input = string.Empty;
            for (int i = 0; i < maxWord; i++)
            {
                input = input + strArray[i] + " ";
            }
            return (input.Trim() + extensionEnd);
        }

        public static string FormatNumber(string sNumber, string sperator = ".")
        {
            int num = 3;
            int num2 = 0;
            for (int i = 1; i <= (sNumber.Length / 3); i++)
            {
                if ((num + num2) < sNumber.Length)
                {
                    sNumber = sNumber.Insert((sNumber.Length - num) - num2, sperator);
                }
                num += 3;
                num2++;
            }
            return sNumber;
        }

        public static string FormatNumberWithComma(string sNumber)
        {
            int num = 3;
            int num2 = 0;
            for (int i = 1; i <= (sNumber.Length / 3); i++)
            {
                if ((num + num2) < sNumber.Length)
                {
                    sNumber = sNumber.Insert((sNumber.Length - num) - num2, ",");
                }
                num += 3;
                num2++;
            }
            return sNumber;
        }

        public static bool IsValidWord(string input, char character)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            string[] arr = input.Split(character);
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Length > 30)
                {
                    return false;
                }
            }
            return true;
        }

        public static string GetMetaDescription(string format, params object[] args)
        {
            if (String.IsNullOrEmpty(format)) return String.Empty;

            string strDes = format;

            if (!String.IsNullOrEmpty(format) && args != null && args.Length > 0)
            {
                strDes = String.Format(strDes, args);
            }

            return strDes;
        }
        public static bool Equals(string text1, string text2)
        {
            if (text1 == null && text2 == null)
                return true;
            if (text1 == null && text2 != null)
                return false;
            if (text1 != null && text2 == null)
                return false;
            return text1.Equals(text2);
        }
    }
}
