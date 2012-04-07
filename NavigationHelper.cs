using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace com.wp..helpers
{
    public class NavigationHelper
    {
        public static void Navigate(string uri)
        {
            PhoneApplicationFrame root = Application.Current.RootVisual as PhoneApplicationFrame;
            if (root != null)
            {
                root.Navigate(new Uri(uri, UriKind.Relative));
            }
        }

        /// <summary>
        /// Purges the Root Frame off all Navigational Journal Entries. This
        /// has the effect of causing you app to exit if the back button is
        /// pressed after this method is called.
        /// </summary>
        /// <param name="rootFrame"></param>
        public static void PurgeNavigationalBackstack(PhoneApplicationFrame rootFrame)
        {
            var purgeList = new System.Collections.Generic.List<System.Windows.Navigation.JournalEntry>();

            foreach (var entry in rootFrame.BackStack)
                purgeList.Add(entry);

            foreach (var entry in purgeList)
            {
                if (rootFrame.CanGoBack)
                    rootFrame.RemoveBackEntry();
            }
        }
    }
}