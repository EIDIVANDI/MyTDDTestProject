using MyProjectBLL.Repository;
using MyProjectDAL.Context;
using MyProjectDAL.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;

namespace MyProjectBLL.Persons
{
    public class PersonManagement
    {
        private IRepository _Repository;
        public PersonManagement() : this(new EFRepository()) { }
        public PersonManagement(IRepository repository)
        {
            _Repository = repository;
        }

        public Person GetByName(string name)
        {
            Person p = _Repository.GetByName(name);
            return p;
        }

        public IEnumerable<Person> GetAll()
        {
            IEnumerable<Person> p = _Repository.GetAll();
            return p;
        }

        public void CreateThumbnail(string fileName, string filePath, int thumbWi, int thumbHi, bool maintainAspect)
        {
            // do nothing if the original is smaller than the designated thumbnail dimensions
            var originalFile = Path.Combine(filePath, fileName);
            var source = Image.FromFile(originalFile);
            if (source.Width <= thumbWi && source.Height <= thumbHi) return;

            Bitmap thumbnail;
            try
            {
                int wi = thumbWi;
                int hi = thumbHi;

                if (maintainAspect)
                {
                    // maintain the aspect ratio despite the thumbnail size parameters
                    if (source.Width > source.Height)
                    {
                        wi = thumbWi;
                        hi = (int)(source.Height * ((decimal)thumbWi / source.Width));
                    }
                    else
                    {
                        hi = thumbHi;
                        wi = (int)(source.Width * ((decimal)thumbHi / source.Height));
                    }
                }

                thumbnail = new Bitmap(wi, hi);
                using (Graphics g = Graphics.FromImage(thumbnail))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.FillRectangle(Brushes.Transparent, 0, 0, wi, hi);
                    g.DrawImage(source, 0, 0, wi, hi);
                }

                var thumbnailName = Path.Combine(filePath, "thumbnail_" + fileName);
                thumbnail.Save(thumbnailName);
            }
            catch
            {

            }


        }

        public void AddPerson(Person model)
        {
            _Repository.AddPerson(model);
        }
    }
}
