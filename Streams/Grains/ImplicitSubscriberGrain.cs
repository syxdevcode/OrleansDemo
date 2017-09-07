﻿using IGrains;
using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    /// <summary>
    /// 隐式订阅
    /// </summary>
    [ImplicitStreamSubscription("GrainImplicitStream")]
    public class ImplicitSubscriberGrain : Grain, IImplicitSubscriberGrain, IAsyncObserver<string>
    {
        protected StreamSubscriptionHandle<string> streamHandle;

        public override async Task OnActivateAsync()
        {
            var streamId = this.GetPrimaryKey();
            var streamProvider = this.GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<string>(streamId, "GrainImplicitStream");
            streamHandle = await stream.SubscribeAsync(OnNextAsync);
        }

        public override async Task OnDeactivateAsync()
        {
            if (streamHandle != null)
                await streamHandle.UnsubscribeAsync();
        }

        public Task OnCompletedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }

        public Task OnNextAsync(string item, StreamSequenceToken token = null)
        {
            Console.WriteLine($"GrainImplicitStream Received message:{item}");
            return Task.CompletedTask;
        }
    }
}
