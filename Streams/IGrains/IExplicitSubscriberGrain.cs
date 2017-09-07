using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    /// <summary>
    /// 显示订阅
    /// </summary>
    public interface IExplicitSubscriberGrain: IGrainWithGuidKey
    {
        Task<StreamSubscriptionHandle<string>> SubscribeAsync();

        Task ReceivedMessageAsync(string data);
    }
}
