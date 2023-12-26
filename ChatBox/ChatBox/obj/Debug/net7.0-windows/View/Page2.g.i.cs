﻿#pragma checksum "..\..\..\..\View\Page2.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "226EF271523BDD8E939E547C4A3A6B120818F409"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ChatBox.View;
using ChatBox.View.Components;
using ChatBox.ViewModel;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ChatBox.View {
    
    
    /// <summary>
    /// Page2
    /// </summary>
    public partial class Page2 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 113 "..\..\..\..\View\Page2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ChatScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\View\Page2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ChatPanel;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\..\View\Page2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMessage;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\..\View\Page2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image selectedImage;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\..\View\Page2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button InputImage;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\..\View\Page2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.14.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ChatBox;component/view/page2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Page2.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.14.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ChatScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            
            #line 114 "..\..\..\..\View\Page2.xaml"
            this.ChatScrollViewer.Loaded += new System.Windows.RoutedEventHandler(this.ChatScrollViewer_Loaded);
            
            #line default
            #line hidden
            
            #line 115 "..\..\..\..\View\Page2.xaml"
            this.ChatScrollViewer.ScrollChanged += new System.Windows.Controls.ScrollChangedEventHandler(this.ChatScrollViewer_ScrollChanged);
            
            #line default
            #line hidden
            
            #line 116 "..\..\..\..\View\Page2.xaml"
            this.ChatScrollViewer.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.ChatScrollViewer_PreviewMouseWheel);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ChatPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.txtMessage = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.selectedImage = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.InputImage = ((System.Windows.Controls.Button)(target));
            
            #line 144 "..\..\..\..\View\Page2.xaml"
            this.InputImage.Click += new System.Windows.RoutedEventHandler(this.InputImage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 153 "..\..\..\..\View\Page2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MicButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.sendButton = ((System.Windows.Controls.Button)(target));
            
            #line 164 "..\..\..\..\View\Page2.xaml"
            this.sendButton.Click += new System.Windows.RoutedEventHandler(this.SendButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

