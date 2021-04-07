using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW4._5._21.Data;

namespace HW4._5._21.Web.Models
{
    public class ViewImageViewModel
    {
        public Image Image { get; set; }
        public bool alreadyLiked { get; set; }
        public List<int> likedImages { get; set; }
    
    }
}
