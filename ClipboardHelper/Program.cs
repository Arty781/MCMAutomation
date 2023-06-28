using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardHelper
{
    public class Program
    {
        static void Main(string[] args)
        {

        }

        public static void SaveToClipboard(string email)
        {
            Clipboard.SetText(email);
        }
    }
}
