using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RejseplanenBand
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StationsData stationsData;
        int numberOfVisibleControls = 1;

        public MainPage()
        {
            this.InitializeComponent();
            stationsData = new StationsData();
        }
       
        public void enableDisableButtons()
        {
            switch (numberOfVisibleControls)
            {
                case 1:
                    (this.FindName("RemoveButton") as Button).IsEnabled = false;
                    break;
                case 2:
                    (this.FindName("RemoveButton") as Button).IsEnabled = true;
                    break;
                case 4:
                    (this.FindName("AddButton") as Button).IsEnabled = true;
                    break;
                case 5:
                    (this.FindName("AddButton") as Button).IsEnabled = false;
                    break;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            (this.FindName("SelectedStation" + numberOfVisibleControls) as FrameworkElement).Visibility = Visibility.Collapsed;
            (this.FindName("StationBox" + numberOfVisibleControls) as FrameworkElement).Visibility = Visibility.Collapsed;

            numberOfVisibleControls--;
            this.enableDisableButtons();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            numberOfVisibleControls++;

            (this.FindName("SelectedStation" + numberOfVisibleControls) as FrameworkElement).Visibility = Visibility.Visible;
            (this.FindName("StationBox" + numberOfVisibleControls) as FrameworkElement).Visibility = Visibility.Visible;

            this.enableDisableButtons();
        }

        private void StationsBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void StationsBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void StationsBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.ItemsSource = (sender.Text.Length > 1) ?
                    stationsData.Where(x => x.StartsWith(sender.Text, StringComparison.OrdinalIgnoreCase)) :
                    new string[] { };
            }
        }
    }
}
