﻿using System.Drawing;

namespace ScreenFIRE.Modules.Screenshot.Companion {

    /// <summary> Image measurements and properties </summary>
    class ImageMetrics {

        public Rectangle Rectangle { get; private set; }

        /// <returns>Focus area <see cref="Gdk.Rectangle"/> of the <see cref="Screenshot"/> instance</returns> 
        private Rectangle find_Rectangle(IScreenshotType screenshotType)
            => Rectangle = screenshotType switch {
                IScreenshotType.Custom => Rectangle,
            //IScreenshotType.WindowUnderMouse => ,
            //IScreenshotType.ScreenUnderMouse => ,
            //IScreenshotType.ActiveWindow => , 
            _ => new Screens().AllRectangle
            // ^^ IScreenshotType.All ^^ 
        };



        private ImageMetrics AutoInstance(IScreenshotType screenshotType)
            => new() {
                Rectangle = find_Rectangle(screenshotType)
            };
        //! vv Portal below. Use `ImageMetrics.Instance(screenshotType)`  

        /// <summary> AUTO </summary>
        /// <param name="screenshotType"> Type of the screenshot </param>
        /// <returns> Instance of <see cref="ImageMetrics"/> </returns>
        public static ImageMetrics Instance(IScreenshotType screenshotType)
            => new ImageMetrics().AutoInstance(screenshotType);


        /// <summary> MANUAL </summary>
        /// <param name="rectangle"> Image rectangle </param>
        /// <returns> (Manually set) Instance of <see cref="ImageMetrics"/> </returns>
        public static ImageMetrics Instance(Rectangle rectangle)
            => new() { Rectangle = rectangle };

    }
}