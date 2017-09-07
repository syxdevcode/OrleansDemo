using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    /// <summary>
    /// 隐式订阅
    /// 隐式订阅的订阅者是唯一的，不存在对一个Stream的多次订阅，也不能取消订阅
    /// </summary>
    public interface IImplicitSubscriberGrain : IGrainWithGuidKey
    {
    }
}
