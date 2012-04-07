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
using System.Text;

namespace com.wp.helpers
{
    public static class WebClientHelper
    {
        /// <summary>
        /// Performs an HTTP GET request
        /// </summary>
        /// <param name="wc">this webclient</param>
        /// <param name="url">url to GET</param>
        /// <param name="callback">HTTP response callback</param>
        public static void GET(this WebClient wc, string url, Action<object, DownloadStringCompletedEventArgs> callback)
        {
            wc.DownloadStringCompleted += (sender, args) =>
            {
                callback(sender, args);
            };

            if (!wc.IsBusy)
                wc.DownloadStringAsync(new Uri(url));
            else
                throw new InvalidOperationException("WebClient does not support concurrent requests!");
        }

        /// <summary>
        /// Performs an HTTP POST request
        /// </summary>
        /// <param name="wc">this webclient</param>
        /// <param name="url">url to POST to</param>
        /// <param name="content">content to post (querystring with no ? at the beginning)</param>
        /// <param name="contentType">content type, if null or empty it defaults to x-www-form-urlencoded</param>
        /// <param name="callback"></param>
        public static void POST(this WebClient wc, string url, string content, string contentType, Action<object, UploadStringCompletedEventArgs> callback)
        {
            // set content type
            if (string.IsNullOrEmpty(contentType))
                contentType = "x-www-form-urlencoded";

            wc.Headers["Content-Type"] = contentType;
            
            // hook callback from webclient to the input callback parameter
            wc.UploadStringCompleted += (sender, args) =>
            {
                callback(sender, args);    
            };

            // post
            if (!wc.IsBusy)
                wc.UploadStringAsync(new Uri(url), "POST", content);
            else
                throw new InvalidOperationException("WebClient does not support concurrent requests!");
        }
    }
}