using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.FeatureRepository
{
    public class ForumRepository : IForumRepository
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";
        private readonly Serializer<Forum> _serializer;
        private List<Forum> _forums;

        public Subject ForumSubject { get; set; }
        public Forum Add(Forum forum)
        {
            forum.Id = NextId();
            _forums = _serializer.FromCSV(FilePath);
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            ForumSubject.NotifyObservers();
            return forum;
        }

        public void Delete(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forum found = _forums.Find(a => a.Id == forum.Id);
            _forums.Remove(found);
            _serializer.ToCSV(FilePath, _forums);
            ForumSubject.NotifyObservers();
        }

        public List<Forum> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Forum GetById(int id)
        {
            foreach (Forum forum in _forums)
            {
                if (id == forum.Id) return forum;
            }

            return null;
        }

        public int NextId()
        {
            _forums = _serializer.FromCSV(FilePath);
            if (_forums.Count < 1)
                return 1;

            return _forums.Max(a => a.Id) + 1;
        }

        public Forum Update(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forum current = _forums.Find(a => a.Id == forum.Id);
            int index = _forums.IndexOf(current);
            _forums.Remove(current);
            _forums.Insert(index, forum);
            _serializer.ToCSV(FilePath, _forums);
            ForumSubject.NotifyObservers();
            return forum;
        }
    }
}
