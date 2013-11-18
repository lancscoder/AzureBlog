using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Storage
{
    public interface IStorage
    {
        List<Post> GetAllPosts();
        void SavePost(Post post);
        void DeletePost(Post post);
        string SaveFile(byte[] bytes, string extension);
    }
}
