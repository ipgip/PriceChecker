﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace web.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Розничная")]
        public string PriceType {
            get {
                return ((string)(this["PriceType"]));
            }
            set {
                this["PriceType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://bazby.ru")]
        public string Site {
            get {
                return ((string)(this["Site"]));
            }
            set {
                this["Site"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("BFEBFBFF000206A7")]
        public string License {
            get {
                return ((string)(this["License"]));
            }
            set {
                this["License"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30000")]
        public int Duration {
            get {
                return ((int)(this["Duration"]));
            }
            set {
                this["Duration"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Srvr=\"srvnew\";Ref=\"utrus_beckup\";Usr=\"Admin\";Pwd=\"9780100941\";")]
        public string CS {
            get {
                return ((string)(this["CS"]));
            }
            set {
                this["CS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Srvr=\"srvnew\";Ref=\"utrus_beckup\";Usr=\"Admin\";Pwd=\"9780100941\";")]
        public string Параметр {
            get {
                return ((string)(this["Параметр"]));
            }
            set {
                this["Параметр"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("File=\"C:\\Users\\ipigp\\Documents\\bazby\";Usr=\"Адимн\";Pwd=\"1\";")]
        public string Параметр1 {
            get {
                return ((string)(this["Параметр1"]));
            }
            set {
                this["Параметр1"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Srvr=\"srvnew\";Ref=\"utrus\";Usr=\"Покупатель\";Pwd=\"1\";")]
        public string Параметр2 {
            get {
                return ((string)(this["Параметр2"]));
            }
            set {
                this["Параметр2"] = value;
            }
        }
    }
}