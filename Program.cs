using System;

namespace Discord_Net_Example
{
    class Program
    {
        static void Main(string[] args)
            => new Bot().MainAsync().GetAwaiter().GetResult();
    }
}
