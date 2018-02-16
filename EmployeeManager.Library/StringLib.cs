using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Library
{
    /// <summary>
    /// Class to treat strings
    /// </summary>
    public static class StringLib
    {
        /// <summary>
        /// Checks if String is Null or Empty
        /// </summary>
        /// <param name="content">string content</param>
        /// <returns>bool</returns>
        public static bool CheckNull(string content)
        {
            if (!string.IsNullOrEmpty(content))
                return true;

            return false;
        }

        /// <summary>
        /// Checks if String is with correct lenght
        /// </summary>
        /// <param name="content"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static bool CheckLenght(string content, int lenght)
        {
            if (content.Length <= lenght)
                return true;

            return false;
        }
    }
}
