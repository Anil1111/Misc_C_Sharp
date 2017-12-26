using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{
    public class SecurityDemo
    {
        public SecurityDemo()
        {
            var id = WindowsIdentity.GetCurrent();
            Console.WriteLine(id.Name);

            var principal = new WindowsPrincipal(id);
            Console.WriteLine(principal.IsInRole("Builtin\\Admin"));

            var account = new NTAccount(id.Name);
            var sid = account.Translate(typeof(SecurityIdentifier));
            Console.WriteLine(sid.Value);

            foreach (var group in id.Groups)
            {
                Console.WriteLine(group.Translate(typeof(NTAccount)));
            }
        }
    }
}
