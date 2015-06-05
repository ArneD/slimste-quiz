using System.Collections.Generic;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Factories
{
    public static class ListQuestionFactory
    {
        public static ListQuestion CreateListQuestion(string listName, string question, ICollection<string> answers)
        {
            return new ListQuestion(listName, question, answers);
        }
    }
}
