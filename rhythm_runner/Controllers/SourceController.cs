using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhythm_runner.Controllers
{
    public class SourceController
    {
                                // index,  value  
        private static Dictionary<string, Bitmap> imgs = new Dictionary<string, Bitmap>();

        static public Bitmap getImage(string imageName)
        {
            if (!imgs.ContainsKey(imageName))
                imgs.Add(imageName, new Bitmap(imageName));

            return imgs[imageName];
        }
    }
}
