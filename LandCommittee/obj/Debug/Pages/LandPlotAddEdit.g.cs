﻿#pragma checksum "..\..\..\Pages\LandPlotAddEdit.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DD4181D308FC5300707AB614468FD8F06D9820EE6D2028BB49ABE6B522092D64"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using LandCommittee.Pages;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace LandCommittee.Pages {
    
    
    /// <summary>
    /// LandPlotAddEdit
    /// </summary>
    public partial class LandPlotAddEdit : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PhotoImageBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PhotoTextBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ImageSave;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSquare;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBuilt;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNumber;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCost;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAdress;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Category;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Pages\LandPlotAddEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Save;
        
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
            System.Uri resourceLocater = new System.Uri("/LandCommittee;component/pages/landplotaddedit.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\LandPlotAddEdit.xaml"
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
            this.PhotoImageBox = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.PhotoTextBox = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.ImageSave = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Pages\LandPlotAddEdit.xaml"
            this.ImageSave.Click += new System.Windows.RoutedEventHandler(this.ImageSave_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtSquare = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\Pages\LandPlotAddEdit.xaml"
            this.txtSquare.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtSquare_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtBuilt = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\..\Pages\LandPlotAddEdit.xaml"
            this.txtBuilt.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtBuilt_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtCost = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\..\Pages\LandPlotAddEdit.xaml"
            this.txtCost.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtCost_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtAdress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.Category = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.Save = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Pages\LandPlotAddEdit.xaml"
            this.Save.Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

