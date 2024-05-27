using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookingApp.Repository.FeatureRepository
{
    public class ForumCommentRepository : IForumCommentRepository
    {
        private const string FilePath = "../../../Resources/Data/forum_comments.csv";
        private readonly Serializer<ForumComment> _serializer;
        private List<ForumComment> _comments;

        public Subject ForumCommentSubject { get; set; }
        
        public ForumCommentRepository()
        {
            _serializer = new Serializer<ForumComment>();
            _comments = new List<ForumComment>();
            ForumCommentSubject = new Subject();    
        }
        public ForumComment Add(ForumComment comment)
        {
            comment.Id = NextId();
            _comments = _serializer.FromCSV(FilePath);
            _comments.Add(comment);
            _serializer.ToCSV(FilePath, _comments);
            ForumCommentSubject.NotifyObservers();
            return comment;
        }

        public void Delete(ForumComment comment)
        {
            _comments = _serializer.FromCSV(FilePath);
            ForumComment found = _comments.Find(a => a.Id == comment.Id);
            _comments.Remove(found);
            _serializer.ToCSV(FilePath, _comments);
            ForumCommentSubject.NotifyObservers();
        }

        public List<ForumComment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ForumComment GetById(int id)
        {
            foreach (ForumComment comment in _comments)
            {
                if (id == comment.Id) return comment;
            }

            return null;
        }

        public int NextId()
        {
            _comments = _serializer.FromCSV(FilePath);
            if (_comments.Count < 1)
                return 1;

            return _comments.Max(a => a.Id) + 1;
        }

        public ForumComment Update(ForumComment comment)
        {
            _comments = _serializer.FromCSV(FilePath);
            ForumComment current = _comments.Find(a => a.Id == comment.Id);
            int index = _comments.IndexOf(current);
            _comments.Remove(current);
            _comments.Insert(index, comment);
            _serializer.ToCSV(FilePath, _comments);
            ForumCommentSubject.NotifyObservers();
            return comment;
        }
    }
}
