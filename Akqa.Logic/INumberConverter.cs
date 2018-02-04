using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akqa.Logic
{
    public interface INumberConverter
    {
        /// <summary>
        /// Converts a number into a words representation using upper case
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <example>ONE HUNDRED FIFTY</example>
        Task<string> Convert(decimal number);
    }
}
