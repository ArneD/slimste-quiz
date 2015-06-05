using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Factories
{
    public static class GalleryQuestionFactory
    {
        public static GalleryQuestion CreateGalleryQuestion(byte[] photo, string answer)
        {
            return new GalleryQuestion(photo, answer);
        }

        public static GalleryQuestion CreateGalleryQuestion(byte[] photo, string answer, byte[] photoAnswer)
        {
            var question = CreateGalleryQuestion(photo, answer);
            question.PhotoAnswer = photoAnswer;
            return question;
        }
    }
}
