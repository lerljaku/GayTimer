using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayerView 
	{
		public PlayerView ()
		{
			InitializeComponent();
	    }

	    private void Button_OnPressed(object sender, EventArgs e)
	    {
	        var button = (Button)sender;

	        button.Opacity = 0.5;
	    }

	    private void Button_OnReleased(object sender, EventArgs e)
	    {
	        var button = (Button)sender;

	        button.Opacity = 0;
	    }
    }
}