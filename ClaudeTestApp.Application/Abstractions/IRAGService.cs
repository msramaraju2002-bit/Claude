using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudeTestApp.Application.Abstractions
{
    public interface IRAGService
    {
        Task<string> AskAsync(string question);
    }
}
