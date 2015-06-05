using Autofac;
using SlimsteMens.Model;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Repositories;
using SlimsteMens.Service.DataContracts;
using System;

namespace SlimsteMens.Service.Admin
{
    public class AdminQuestionsService
    {
        private readonly IContainer _container;

        public AdminQuestionsService(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            _container = container;
        }

        public bool AddQuestion369(ThreeSixNineQuestionDto threeSixNineQuestionDto)
        {
            try
            {
                var model = threeSixNineQuestionDto.ToModel();
                using (var scope = _container.BeginLifetimeScope().Resolve<IUnitOfWorkScope>())
                {
                    scope.Resolve<IRepository<ThreeSixNineQuestion>>().Insert(model);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddPuzzleQuestion(PuzzleQuestionDto puzzleQuestionDto)
        {
            try
            {
                var model = puzzleQuestionDto.ToModel();
                using (var scope = _container.BeginLifetimeScope().Resolve<IUnitOfWorkScope>())
                {
                    scope.Resolve<IRepository<PuzzleQuestion>>().Insert(model);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddGallery(GalleryDto galleryDto)
        {
            try
            {
                var model = galleryDto.ToModel();
                using (var scope = _container.BeginLifetimeScope().Resolve<IUnitOfWorkScope>())
                {
                    scope.Resolve<IRepository<Gallery>>().Insert(model);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddVideo(VideoDto videoDto)
        {
            try
            {
                var model = videoDto.ToModel();
                using (var scope = _container.BeginLifetimeScope().Resolve<IUnitOfWorkScope>())
                {
                    scope.Resolve<IRepository<VideoQuestion>>().Insert(model);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddFinaleQuestion(FinaleQuestionDto finaleQuestionDto)
        {
            try
            {
                var model = finaleQuestionDto.ToModel();
                using (var scope = _container.BeginLifetimeScope().Resolve<IUnitOfWorkScope>())
                {
                    scope.Resolve<IRepository<FinaleQuestion>>().Insert(model);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
