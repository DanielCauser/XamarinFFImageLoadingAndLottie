using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFFImageLoadingAndLottie.Models
{
    public static class StaticResources
    {
        public static string MonkeyList => "";
    }
    public class Monkey
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public int Population { get; set; }
    }
}
