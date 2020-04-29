using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibbonVk.Models
{
    class PhotoModel
    {
        
        public int PhotoId { set; get; }
        public string ImageUrl { set; get; }

        public PhotoModel(int PhotoId, string ImageUrl)
        {
            this.PhotoId = PhotoId;
            this.ImageUrl = ImageUrl;
        }

    }
}
