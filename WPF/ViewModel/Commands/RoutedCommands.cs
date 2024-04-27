using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand AllToursTab = new RoutedUICommand(
            "Opening All Tours Tab",
            "AllToursTab",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.A, ModifierKeys.Control)
            });
        public static readonly RoutedUICommand MyToursTab = new RoutedUICommand(
            "Opening My Tours Tab",
            "MyToursTab",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.M, ModifierKeys.Control)
            });
        public static readonly RoutedUICommand EndedToursTab = new RoutedUICommand(
            "Opening Ended Tours Tab",
            "EndedToursTab",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Control)
            });
        public static readonly RoutedUICommand RequestedToursTab = new RoutedUICommand(
            "Opening Requested Tours Tab",
            "Requested",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.R, ModifierKeys.Control)
            });
        public static readonly RoutedUICommand VouchersTab = new RoutedUICommand(
            "Opening Vouchers Tab",
            "VouchersTab",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.V, ModifierKeys.Control)
            });        
        public static readonly RoutedUICommand Logout = new RoutedUICommand(
            "Logout",
            "Logout",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.L, ModifierKeys.Control)
            });
        public static readonly RoutedUICommand Notifications = new RoutedUICommand(
            "Opening Notifications",
            "Notifications",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.N, ModifierKeys.Control)
            }); 
        public static readonly RoutedUICommand CountrySearch = new RoutedUICommand(
            "Focusing on country search",
            "CountrySearch",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.C, ModifierKeys.Alt)
            }); 
        public static readonly RoutedUICommand Search = new RoutedUICommand(
            "Clicking search button",
            "Search",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Alt)
            }); 
        public static readonly RoutedUICommand Reset = new RoutedUICommand(
            "Clicking reset button",
            "Reset",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.R, ModifierKeys.Control)
            }); 
    }
}
