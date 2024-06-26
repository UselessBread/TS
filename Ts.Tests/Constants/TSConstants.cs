﻿using Common.Constants;
using TS.Data.Contracts.DTO;
using TS.Data.Contracts.Entities;

namespace Ts.Tests.Constants
{
    public static class TSConstants
    {
        #region get methods
        public static Guid FirstUserGuid { get; } = Guid.NewGuid();
        public static Guid SecondUserGuid { get; } = Guid.NewGuid();
        public static Guid ThirdUserGuid { get; } = Guid.NewGuid();

        public static TestsContent UpdSingleOptionContent = new TestsContent
        {
            ImmutableId = Guid.NewGuid(),
            Id = 2,
            CreationDate = DateTime.Now,
            Tasks = new List<TaskDto>
                {
                   new TaskDto
                    {
                        Answers = new List<string>{ "fstUpd","secUpd", "thirdUpd"},
                        Position = 0,
                        RightAnswers = new List<int>{ 2},
                        TaskDescription = "single",
                        Type = Common.Constants.TestTypes.SingleOption
                    }
                },
            Version = 2,
            DeletionDate = null
        };

        public static TestsContent MultipleOptionsContent = new TestsContent
        {
            ImmutableId = Guid.NewGuid(),
            Id = 3,
            CreationDate = DateTime.Now,
            Tasks = new List<TaskDto>
                {
                   new TaskDto
                    {
                        Answers = new List<string>{ "fst2","sec2", "third2"},
                        Position = 0,
                        RightAnswers = new List<int>{ 2,3},
                        TaskDescription = "mult",
                        Type = Common.Constants.TestTypes.MultipleOptions
                    }
                },
            Version = 1,
            DeletionDate = null
        };

        public static TestsContent TextOptionContent = new TestsContent
        {
            ImmutableId = Guid.NewGuid(),
            Id = 4,
            CreationDate = DateTime.Now,
            Tasks = new List<TaskDto>
                {
                   new TaskDto
                    {
                        Answers = new List<string>(),
                        Position = 0,
                        RightAnswers = null,
                        TaskDescription = "textDescr",
                        Type = Common.Constants.TestTypes.Text
                    }
                },
            Version = 1,
            DeletionDate = null
        };

        public static TestsContent OldSingleOptionContent = new TestsContent
        {
            ImmutableId = UpdSingleOptionContent.ImmutableId,
            Id = 1,
            CreationDate = UpdSingleOptionContent.CreationDate,
            Tasks = new List<TaskDto>
                {
                   new TaskDto
                    {
                        Answers = new List<string>{ "fst","sec", "third"},
                        Position = 0,
                        RightAnswers = new List<int>{ 2},
                        TaskDescription = "single",
                        Type = Common.Constants.TestTypes.SingleOption
                    }
                },
            Version = 1,
            DeletionDate = UpdSingleOptionContent.CreationDate.AddHours(-1),
        };

        public static TestsContent TextOptionContentByFirstUser = new TestsContent
        {
            ImmutableId = Guid.NewGuid(),
            Id = 5,
            CreationDate = DateTime.Now,
            Tasks = new List<TaskDto>
                {
                   new TaskDto
                    {
                        Answers = new List<string>(),
                        Position = 0,
                        RightAnswers = null,
                        TaskDescription = "second by first text",
                        Type = Common.Constants.TestTypes.Text
                    }
                },
            Version = 1,
            DeletionDate = null
        };

        public static TestsContent TextOptionContentByFirstUserThird = new TestsContent
        {
            ImmutableId = Guid.NewGuid(),
            Id = 6,
            CreationDate = DateTime.Now,
            Tasks = new List<TaskDto>
                {
                   new TaskDto
                    {
                        Answers = new List<string>(),
                        Position = 0,
                        RightAnswers = null,
                        TaskDescription = "third by first text",
                        Type = Common.Constants.TestTypes.Text
                    }
                },
            Version = 1,
            DeletionDate = null
        };

        public static TestDescriptions FirstContextDescription = new TestDescriptions
        {
            Id = 1,
            Name = "First(sinle) Descr",
            CreationDate = DateTime.Now,
            CrreatedBy = TSConstants.FirstUserGuid,
            DeletionDate = DateTime.Now.AddHours(1),
            ImmutableId = Guid.NewGuid(),
            TestContentId = UpdSingleOptionContent.Id,
            TestContentImmutableId = UpdSingleOptionContent.ImmutableId,
            Version = 1
        };
        public static TestDescriptions FirstUpdContextDescription = new TestDescriptions
        {
            Id = 2,
            Name = "First(single) Descr Upd",
            CreationDate = FirstContextDescription.DeletionDate.Value,
            CrreatedBy = TSConstants.FirstUserGuid,
            DeletionDate = null,
            ImmutableId = FirstContextDescription.ImmutableId,
            TestContentId = UpdSingleOptionContent.Id,
            TestContentImmutableId = UpdSingleOptionContent.ImmutableId,
            Version = 2
        };
        public static TestDescriptions SecondContextDescription = new TestDescriptions
        {
            Id = 3,
            Name = "Second(mult) Descr",
            CreationDate = DateTime.Now,
            CrreatedBy = TSConstants.SecondUserGuid,
            DeletionDate = null,
            ImmutableId = Guid.NewGuid(),
            TestContentId = MultipleOptionsContent.Id,
            TestContentImmutableId = MultipleOptionsContent.ImmutableId,
            Version = 1
        };

        public static TestDescriptions ThirdContextDescription = new TestDescriptions
        {
            Id = 4,
            Name = "third(text) Descr",
            CreationDate = DateTime.Now,
            CrreatedBy = TSConstants.ThirdUserGuid,
            DeletionDate = null,
            ImmutableId = Guid.NewGuid(),
            TestContentId = TextOptionContent.Id,
            TestContentImmutableId = TextOptionContent.ImmutableId,
            Version = 1
        };

        public static TestDescriptions SecondContextDescriptionByFirstUser = new TestDescriptions
        {
            Id = 5,
            Name = "fourth(text) Descr by first user",
            CreationDate = DateTime.Now,
            CrreatedBy = FirstUserGuid,
            DeletionDate = null,
            ImmutableId = Guid.NewGuid(),
            TestContentId = TextOptionContentByFirstUser.Id,
            TestContentImmutableId = TextOptionContentByFirstUser.ImmutableId,
            Version = 1
        };

        public static TestDescriptions ThirdContextDescriptionByFirstUser = new TestDescriptions
        {
            Id = 6,
            Name = "fifth(text) Descr by first user",
            CreationDate = DateTime.Now,
            CrreatedBy = FirstUserGuid,
            DeletionDate = null,
            ImmutableId = Guid.NewGuid(),
            TestContentId = TextOptionContentByFirstUserThird.Id,
            TestContentImmutableId = TextOptionContentByFirstUserThird.ImmutableId,
            Version = 1
        };
        #endregion

        #region create
        public static Guid CreateUserId = Guid.NewGuid();

        public static CreateNewTestDto IdealCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto WrongPositionCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 3,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto HaveRightAnswersInTextCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto HaveAnswersInTextCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{ "te,p"},
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto DoesNotHaveRightAnsersInSingleCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto DoesNotHaveAnsersInSingleCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = null,
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto MultipleRightAnswersSingleCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto OutOfRangeRightAnswersSingleCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{5},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };

        public static CreateNewTestDto DoesNotHaveRightAnsersInMultCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto DoesNotHaveAnsersInMultCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 1,
                        RightAnswers = new List<int>{1,2},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        public static CreateNewTestDto OutOfRangeRightAnsersInMultCreateNewTestDto = new CreateNewTestDto
        {
            TestName = "Created via test",
            Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 0,
                        RightAnswers = new List<int>{1},
                        TaskDescription = "Descr",
                        Type = TestTypes.SingleOption
                    },
                    new TaskDto
                    {
                        Answers = new List<string>{"fst","sec","third"},
                        Position = 1,
                        RightAnswers = new List<int>{1,5},
                        TaskDescription = "Descr",
                        Type = TestTypes.MultipleOptions
                    },
                    new TaskDto
                    {
                        Answers = null,
                        Position = 2,
                        RightAnswers = null,
                        TaskDescription = "Descr",
                        Type = TestTypes.Text
                    }
                }
        };
        #endregion
    }
}
