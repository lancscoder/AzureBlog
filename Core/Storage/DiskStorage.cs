using Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;

namespace Core.Storage
{
    public class DiskStorage : IStorage
    {
        private readonly StorageHelper _storageHelper;
        private static string _folder = HostingEnvironment.MapPath("~/posts/");

        public DiskStorage()
        {
            _storageHelper = new StorageHelper();
        }

        public List<Entities.Post> GetAllPosts()
        {
            if (!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }

            var list = new List<Post>();

            foreach (string file in Directory.GetFiles(_folder, "*.xml", SearchOption.TopDirectoryOnly))
            {
                XElement doc = XElement.Load(file);

                var post = _storageHelper.GetPostFromXml(doc, file);

                list.Add(post);
            }

            // TODO : Move this out???
            if (list.Count > 0)
            {
                list.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
                HttpRuntime.Cache.Insert("posts", list);
            }

            return list;
        }

        public void SavePost(Entities.Post post)
        {
            string fileName = Path.Combine(_folder, post.ID + ".xml");

            var doc = _storageHelper.GetPostXml(post);

            if (!File.Exists(fileName)) // New post
            {
                _storageHelper.RefreshCache(post);
            }

            doc.Save(fileName);
        }

        public void DeletePost(Entities.Post post)
        {
            string file = Path.Combine(_folder, post.ID + ".xml");
            File.Delete(file);

            _storageHelper.RemoveFromCache(post);
        }

        public string SaveFile(byte[] bytes, string extension)
        {
            string relative = "~/posts/files/" + Guid.NewGuid() + "." + extension.Trim('.');
            string file = HostingEnvironment.MapPath(relative);

            File.WriteAllBytes(file, bytes);

            //var cruncher = new ImageCruncher.Cruncher();
            //cruncher.CrunchImages(file);

            return VirtualPathUtility.ToAbsolute(relative);
        }
    }
}
