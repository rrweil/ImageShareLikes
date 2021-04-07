using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW4._5._21.Data
{
    public class ImageRepository
    {
        private readonly string _connectionString;
        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddImage (Image image)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Images.Add(image);
            context.SaveChanges();
        }

        public List<Image> GetAllImages()
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.ToList();
        }

        public Image GetImage(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public int GetLikes(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id).Likes;
        }

        public void AddLike(int id)
        {
            var 
            using var context = new ImageDbContext(_connectionString);
            
        }
    }
}
