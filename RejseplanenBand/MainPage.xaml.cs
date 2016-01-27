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
        const string AddDeleteButtonsGridName = "AddDeleteButtonsGrid";
        const string StationSelectionGroupGridNamePrefix = "StationSelectionGroupGrid";

        const int initialTopMargin = 75;
        const int incrementsTopMarginBetweenStationSelectionGroups = 100;
        const int topMarginStationBox = 35;
        const int marginLeftRightButtons = 60;

        int numberOfControl = 1;
        int topMargin = initialTopMargin;

        StationsData stationsData;

        public MainPage()
        {
            this.InitializeComponent();
            this.createAndAppendStationSelectionGroup(MainGrid);
            this.createAndAppendAddRemoveButtons(MainGrid);
            
            stationsData = new StationsData();
        }

        private void createAndAppendAddRemoveButtons(Grid grid)
        {
            Grid addDeleteButtonsGrid = this.createGrid(AddDeleteButtonsGridName);
            Button[] buttons = this.createAddAndRemoveButtons();
            addDeleteButtonsGrid.Children.Add(buttons[0]);
            addDeleteButtonsGrid.Children.Add(buttons[1]);

            MainGrid.Children.Add(addDeleteButtonsGrid);
        }

        private void createAndAppendStationSelectionGroup(Grid grid)
        {
            Grid stationSelectionGrid = this.createGrid(StationSelectionGroupGridNamePrefix + numberOfControl);
            
            this.createSectionTitle(stationSelectionGrid);
            this.createStationsBox(stationSelectionGrid);

            grid.Children.Add(stationSelectionGrid);

            topMargin += incrementsTopMarginBetweenStationSelectionGroups;
        }

        private Grid createGrid(String gridName)
        {
            Grid grid = new Grid();
            grid.Name = gridName;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Margin = new Thickness(0, topMargin, 0, 0);

            return grid;

        }

        private void createSectionTitle(Grid groupGrid)
        {
            TextBlock sectionTitle = new TextBlock();
            sectionTitle.Text = "Selected station " + numberOfControl;

            sectionTitle.FontFamily = new FontFamily("Century Gothic");
            sectionTitle.FontSize = 12;
            sectionTitle.FontWeight = FontWeights.Bold;
            
            sectionTitle.Padding = new Thickness(5, 10, 5, 10);
            sectionTitle.TextAlignment = TextAlignment.Center;
            sectionTitle.TextWrapping = TextWrapping.Wrap;

            groupGrid.Children.Add(sectionTitle);
        }

        private void createStationsBox(Grid groupGrid)
        {
            AutoSuggestBox stationsBox = new AutoSuggestBox();
            stationsBox.Name = "StationBox" + numberOfControl;
            stationsBox.PlaceholderText = "Name of the station";
            stationsBox.QueryIcon = new SymbolIcon(Symbol.Find);
            stationsBox.HorizontalAlignment = HorizontalAlignment.Center;
            stationsBox.VerticalAlignment = VerticalAlignment.Top;
            stationsBox.Margin = new Thickness(0, topMarginStationBox, 0, 0);
            stationsBox.Width = 300;

            stationsBox.TextChanged += StationsBox_TextChanged;
            stationsBox.SuggestionChosen += StationsBox_SuggestionChosen;
            stationsBox.QuerySubmitted += StationsBox_QuerySubmitted;

            groupGrid.Children.Add(stationsBox);
        }

        private Button[] createAddAndRemoveButtons()
        {
            Button addButton = new Button();
            addButton.HorizontalAlignment = HorizontalAlignment.Center;
            addButton.VerticalAlignment = VerticalAlignment.Top;
            addButton.Margin = new Thickness(marginLeftRightButtons, 0, 0, 0);
            addButton.Name = "AddButton";
            addButton.Content = "Add";
            addButton.Click += AddButton_Click;

            Button deleteButton = new Button();
            deleteButton.HorizontalAlignment = HorizontalAlignment.Center;
            deleteButton.VerticalAlignment = VerticalAlignment.Top;
            deleteButton.Margin = new Thickness(0, 0, marginLeftRightButtons, 0);
            deleteButton.Name = "deleteButton";
            deleteButton.Content = "Delete";
            deleteButton.Click += DeleteButton_Click;

            deleteButton.IsEnabled = numberOfControl > 1;

            return new Button[2] { addButton, deleteButton };
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Grid addDeleteButtonsGrid = this.FindName(AddDeleteButtonsGridName) as Grid;
            Grid stationSelectionGrid = this.FindName(StationSelectionGroupGridNamePrefix + numberOfControl) as Grid;
            addDeleteButtonsGrid.Children.Clear();
            stationSelectionGrid.Children.Clear();

            numberOfControl--;
            topMargin -= incrementsTopMarginBetweenStationSelectionGroups;

            this.createAndAppendAddRemoveButtons(MainGrid);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            numberOfControl++;

            Grid addDeleteButtonsGrid = this.FindName(AddDeleteButtonsGridName) as Grid;
            addDeleteButtonsGrid.Children.Clear();

            this.createAndAppendStationSelectionGroup(MainGrid);
            this.createAndAppendAddRemoveButtons(MainGrid);
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
