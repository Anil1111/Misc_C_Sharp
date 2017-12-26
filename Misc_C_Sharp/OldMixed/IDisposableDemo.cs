using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{
    public class IDisposableDemo : IDisposable
    {
        private bool isDisposed = false;
        public void Dispose()
        {
            Console.WriteLine("Disposing the object");
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                if(disposing)
                {
                    //Clean up managed objects by calling their dispose
                }
                //Cleanup unmanaged objects
            }
            isDisposed = false;
        }
        ~IDisposableDemo()
        {
            Console.WriteLine("Destructor called for IDisposable");
            Dispose(false);
        }

    }
}
