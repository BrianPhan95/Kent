using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Libary.Enums
{
    public enum QuestionSectionType
    {
        ListenEnterText = 1,
        ListenWithBoxQuestion = 2,
        ListenSelectAnswer = 3,
        ReadingWithBoxAnswer = 4,
        ReadingTrueFalse = 5
    }

    public enum QuestionTemplateType
    {
        Listening = 1,
        Reading = 2
    }
}
