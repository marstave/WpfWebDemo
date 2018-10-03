using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfWebDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebServer ws;
        ObjectForScriptingHelper helper;

        public MainWindow()
        {
            InitializeComponent();
            helper = new ObjectForScriptingHelper(this);
            wb1.ObjectForScripting = helper;
            ws = new WebServer(SendResponse, "http://localhost:9000/");
            ws.Run();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            wb1.Navigate("http://localhost:9000/");

        }

        public string SendResponse(HttpListenerRequest request)
        {
            return @"<!doctype html>
                    <head>
                        <meta charset='utf-8'>
                        <meta http-equiv='X-UA-Compatible' content='IE=edge, charset=UTF-8' >  
                        <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css'>
                        <script src = 'https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script> 
                        <script src = 'https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js' ></script >
                        <script src='https://ajax.googleapis.com/ajax/libs/jquery-ui-1.7.2.custom.min.js'></script> 
                        
                        <script src='https://unpkg.com/babel-standalone@6/babel.min.js'></script>
                        <script src='https://cdnjs.cloudflare.com/ajax/libs/babel-polyfill/6.7.4/polyfill.min.js'></script>

                        <script type='text/babel'>
                            function test(){
                               alert('ok fra func virkelig')
                            }
                        </script>
                        <style>
                            body {
                                margin:10px;
                                font-family:segoe ui; 
                            } 
                        </style>  
                    </head>
                    <body>
                       <h2>Just a demo</h2>
                       Tester og tester 
                       <button onclick='test()'>test</button>
                    </body>
                    </html> 
                    ";
        }

    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class ObjectForScriptingHelper
    {
        MainWindow mw;
        public ObjectForScriptingHelper(MainWindow w)
        {
            this.mw = w;
        }
    }
}
