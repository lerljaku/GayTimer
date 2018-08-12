using System;
using Xamarin.Forms;

namespace GayTimer.ViewModels
{
    public class MainMasterDetailPageMenuItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Lazy<Page> Page { get; set; }
    }
}