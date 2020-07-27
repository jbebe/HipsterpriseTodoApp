using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoClient.Utils
{
    public static class Extensions
    {
        public static void CustomLog(this IJSRuntime runtime, params string[] args)
        {
            runtime.InvokeVoidAsync("vsLog", args);
        }
    }
}
