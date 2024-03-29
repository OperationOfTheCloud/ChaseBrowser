﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyTabs;
using CefSharp;
using CefSharp.WinForms;
using System.Net;
using System.IO;
using ChaseBrowser.Properties;

namespace ChaseBrowser
{
    public partial class Form1 : Form
    {
        
        protected TitleBarTabs parentTabs
        {
            get
            {
                return (ParentForm as TitleBarTabs);
            }
        }
        public Form1()
        {
            InitializeComponent();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(textBox1.Text);
        }

        private void chromiumWebBrowser1_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Back();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Redo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Refresh();
        }

        private void chromiumWebBrowser1_LoadingStateChanged_1(object sender, LoadingStateChangedEventArgs e)
        {
            parentTabs.Invoke(new Action(() =>
            {
                // This code is executed on the UI thread.
                if (e.IsLoading)
                {
                    parentTabs.SelectedTab.Caption = "Loading...";
                }
            }));
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chromiumWebBrowser1.Load(textBox1.Text);
            }
        }

        private void chromiumWebBrowser1_StatusMessage(object sender, StatusMessageEventArgs e)
        {

        }

        private void chromiumWebBrowser1_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            
            parentTabs.Invoke(new Action(() =>
            {
                // This code is executed on the UI thread.
                parentTabs.SelectedTab.Caption = e.Title;
            }));
        }

        private void chromiumWebBrowser1_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            Invoke(new Action(() => textBox1.Text = e.Address));
            {
                Uri uri = new Uri(e.Address);
                {
                    try
                    {
                        WebClient wc = new WebClient();

                        MemoryStream memorystream = new MemoryStream(wc.DownloadData("https://" + uri.Host + "/favicon.ico"));
                        Icon icon = new Icon(memorystream);
                        {

                            memorystream.Seek(0, SeekOrigin.Begin);

                            Invoke(new Action(() =>
                            {
                                Icon = new Icon(memorystream);

                                parentTabs.UpdateThumbnailPreviewIcon(parentTabs.Tabs.Single(t => t.Content == this));
                                parentTabs.RedrawTabs();
                            }));
                        }
                    }
                    catch
                    {
                        
                    }
                }

                Invoke(new Action(() => Parent.Refresh()));
                
            }
        }
    }
}
