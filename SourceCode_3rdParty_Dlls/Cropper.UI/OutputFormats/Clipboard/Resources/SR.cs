// -----------------------------------------------------------------------------
//  <autogeneratedinfo>
//      This code was generated by:
//        SR Resource Generator custom tool for VS.NET, by Brian Scott
//      Runtime version: 2.0.50727.312
// 
//      It contains classes defined from the contents of the resource file:
//        G:\Current\Cropper\src\Cropper.ClipboardFormat\Resources\SR.resx
// 
//      Generated: Friday, January 26, 2007 8:24 AM
//  </autogeneratedinfo>
// -----------------------------------------------------------------------------
namespace Fusion8.Cropper.ClipboardFormat
{
    using System;
    
    
    internal class SR
    {
        
        public static string BitmapDescription
        {
            get
            {
                return Keys.GetString(Keys.BitmapDescription);
            }
        }
        
        public static string JpgDescription
        {
            get
            {
                return Keys.GetString(Keys.JpgDescription);
            }
        }
        
        public static string PngDescription
        {
            get
            {
                return Keys.GetString(Keys.PngDescription);
            }
        }
        
        public static string ImageQuality(object @int)
        {
            return Keys.GetString(Keys.ImageQuality, new object[] {
                        @int});
        }
        
        private class Keys
        {
            
            static System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Fusion8.Cropper.Resources.SR", typeof(SR).Assembly);
            
            public const string BitmapDescription = "BitmapDescription";
            
            public const string ImageQuality = "ImageQuality";
            
            public const string JpgDescription = "JpgDescription";
            
            public const string PngDescription = "PngDescription";
            
            public static string GetString(string key)
            {
                return resourceManager.GetString(key, Resources.CultureInfo);
            }
            
            public static string GetString(string key, object[] args)
            {
                string msg = resourceManager.GetString(key, Resources.CultureInfo);
                if (msg == null)
                    return "missing reference string for: " + key;
                msg = string.Format(msg, args);
                return msg;
            }
        }
    }
}
