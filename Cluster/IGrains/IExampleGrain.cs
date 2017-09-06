using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace IGrains
{
    public interface IExampleGrain : IGrainWithIntegerCompoundKey
    {
        Task Hello();
    }
}
