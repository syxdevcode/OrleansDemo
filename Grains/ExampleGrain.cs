using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class ExampleGrain : Grain, IGrains.IExampleGrain
    {
        public Task Hello()
        {
            string keyExtension;

            var primaryKey = this.GetPrimaryKey(out keyExtension);

            Console.WriteLine("Hello from " + keyExtension);

            return TaskDone.Done;
        }
    }
}
