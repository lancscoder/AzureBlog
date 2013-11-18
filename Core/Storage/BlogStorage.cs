using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Storage
{
    public class BlogStorage : IStorage
    {
        private readonly StorageHelper _storageHelper;

        public BlogStorage()
        {
            _storageHelper = new StorageHelper();
        }

        public List<Entities.Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public void SavePost(Entities.Post post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(Entities.Post post)
        {
            throw new NotImplementedException();
        }

        public string SaveFile(byte[] bytes, string extension)
        {
            throw new NotImplementedException();
        }
    }
}
