﻿#pragma checksum "..\..\..\..\Views\AccessSystem\LoginWindown.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2F971908E78F97313CD4B14305FBABC50815CFB32549FC134EEEA23042523C4E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Core;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.DXBinding;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using kakashi.Views;


namespace kakashi.Views {
    
    
    /// <summary>
    /// LoginWindown
    /// </summary>
    public partial class LoginWindown : DevExpress.Xpf.Core.ThemedWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\Views\AccessSystem\LoginWindown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox UserComboBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\AccessSystem\LoginWindown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\AccessSystem\LoginWindown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button YesButton;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Views\AccessSystem\LoginWindown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NoButton;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\Views\AccessSystem\LoginWindown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PassSet;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/kakashi;component/views/accesssystem/loginwindown.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\AccessSystem\LoginWindown.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.PasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.YesButton = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\..\Views\AccessSystem\LoginWindown.xaml"
            this.YesButton.Click += new System.Windows.RoutedEventHandler(this.YesBtnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.NoButton = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.PassSet = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

