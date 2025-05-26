using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F21Party.Controllers
{
    internal static class Function
    {
        public static bool HasWriteAccess(string pageName)
        {
            if (!Program.PublicArrWriteAccessPages.Contains(pageName))
            {
                MessageBox.Show("You don't have 'Write' Access!");
                return false;
            }
            return true;
        }
    }
}
