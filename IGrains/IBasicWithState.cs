using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IBasicWithState:Orleans.IGrainWithStringKey
    {
        Task SayHello(string str);

        Task<List<string>> GetHistory();
    }

    /// <summary>
    /// 保存状态
    /// </summary>
    public class BasicState
    {
        public List<string> historyStr = new List<string>();
    }
}
