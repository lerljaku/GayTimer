using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GayTimer.MenuItems
{
    public class MainMasterDetailPageMenuItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Lazy<Page> Page { get; set; }
    }
}