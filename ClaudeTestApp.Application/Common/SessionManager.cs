using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudeTestApp.Application.Common
{
    public static class SessionManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetNewSessionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
