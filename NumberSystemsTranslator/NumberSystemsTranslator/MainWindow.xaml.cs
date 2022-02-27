using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Lib;

namespace NumberSystemsTranslator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Variables Variables = new();
        public MainWindow()
        {
            InitializeComponent();
            SetBinding();
        }

        private void SetBinding()
        {
            var bindingFrom = new Binding("NotionFrom")
            {
                Source = Variables
            };
            var bindingTo = new Binding("NotionTo")
            {
                Source = Variables
            };
            var bindingNumber = new Binding("NumberEntire")
            {
                Source = Variables
            };
            var bindingResult = new Binding("Result")
            {
                Source = Variables,
                Mode = BindingMode.TwoWay
            };

            ComboBoxFrom.SetBinding(ComboBox.TextProperty, bindingFrom);
            ComboBoxTo.SetBinding(ComboBox.TextProperty, bindingTo);
            TextBoxNumber.SetBinding(TextBox.TextProperty, bindingNumber);
            LabelResult.SetBinding(ContentProperty, bindingResult);
            label.SetBinding(Label.ContentProperty, bindingResult);

            ButtonCalculate.AddHandler(Button.ClickEvent, new RoutedEventHandler(Variables.ButtonCalculateClick));

            //CommandBinding commandBinding = new CommandBinding();
            //commandBinding.Command = ApplicationCommands.Help;
            //commandBinding.Executed += CommandBinding_Executed;
            //ButtonCalculate.CommandBindings.Add(commandBinding);
        }

        //private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    MessageBox.Show("Справка по приложению");
        //}
    }
}
