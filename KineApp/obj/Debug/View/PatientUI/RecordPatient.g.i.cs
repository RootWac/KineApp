﻿#pragma checksum "..\..\..\..\View\PatientUI\RecordPatient.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0D310CDC22DE6CB7553649AED430EC9E9559360334C46B36233826A3D561C14E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using KineApp.View.PatientUI;
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


namespace KineApp.View.PatientUI {
    
    
    /// <summary>
    /// RecordPatient
    /// </summary>
    public partial class RecordPatient : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid G_Init;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid G_Create;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_SessionNumber;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Price;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Bilan;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Follow;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button B_Add;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button B_Update;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button B_Finish;
        
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
            System.Uri resourceLocater = new System.Uri("/KineApp;component/view/patientui/recordpatient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
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
            this.G_Init = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 29 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.G_Create = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.TB_SessionNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.TB_Price = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.TB_Bilan = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.TB_Follow = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.B_Add = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
            this.B_Add.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.B_Update = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
            this.B_Update.Click += new System.Windows.RoutedEventHandler(this.B_Update_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.B_Finish = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\View\PatientUI\RecordPatient.xaml"
            this.B_Finish.Click += new System.Windows.RoutedEventHandler(this.B_Finish_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
