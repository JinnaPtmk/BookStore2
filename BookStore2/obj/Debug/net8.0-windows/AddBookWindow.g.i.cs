﻿#pragma checksum "..\..\..\AddBookWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "47C0D823D9CE9DCC3C8444E68B38EDD310651B50"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BookStore2;
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


namespace BookStore2 {
    
    
    /// <summary>
    /// AddBookWindow
    /// </summary>
    public partial class AddBookWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TitleTxt;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IsbnTxt;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescriptionTxt;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PriceTxt;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBtn;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BookStore2;component/addbookwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddBookWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TitleTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.IsbnTxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 11 "..\..\..\AddBookWindow.xaml"
            this.IsbnTxt.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.IsbnTxt_previewtextinput);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\AddBookWindow.xaml"
            this.IsbnTxt.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.IsbnTxt_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DescriptionTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.PriceTxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\..\AddBookWindow.xaml"
            this.PriceTxt.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PriceTxt_previewtextinput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AddBtn = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\AddBookWindow.xaml"
            this.AddBtn.Click += new System.Windows.RoutedEventHandler(this.AddBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\AddBookWindow.xaml"
            this.CancelBtn.Click += new System.Windows.RoutedEventHandler(this.CancelBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

