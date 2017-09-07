using IGrains;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class PersonGrain : Grain, IPersonGrain
    {
        public async Task SayHelloAsync()
        {
            string primaryKey = this.GetPrimaryKeyString();
            Console.WriteLine($"{primaryKey} said hello!");
            await Task.CompletedTask;
        }
    }
}
