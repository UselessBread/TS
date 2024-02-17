using TA.Data.Contracts.Dto;
using TA.Data.Contracts.Entities;

namespace TA.Tests.Constants
{
    public static class DbMockConstants
    {
        public static Guid FirstGroupImmutableId = Guid.NewGuid();
        public static Guid FirstTeacherImmutableId = Guid.NewGuid();
        public static Guid FirstStudentImmutableId = Guid.NewGuid();
        public static Guid SecondGroupImmutableId = Guid.NewGuid();
        public static Guid SecondTeacherImmutableId = Guid.NewGuid();
        public static Guid SecondStudentImmutableId = Guid.NewGuid();

        public static AssignedTests first = new AssignedTests
        {
            AssignedBy = FirstTeacherImmutableId,
            AssignedTime = DateTime.Now,
            CreationDate = DateTime.Now.AddHours(-1),
            GroupImmutableId = FirstGroupImmutableId,
            Id = 1,
            ImmutableId = Guid.NewGuid(),
            TestDescriptionId = 1,
            Version = 1
        };

        public static AssignedTests second = new AssignedTests
        {
            AssignedBy = FirstTeacherImmutableId,
            AssignedTime = DateTime.Now.AddHours(-1),
            CreationDate = DateTime.Now.AddHours(-2),
            GroupImmutableId = FirstGroupImmutableId,
            Id = 2,
            ImmutableId = Guid.NewGuid(),
            TestDescriptionId = 2,
            Version = 1
        };

        public static AssignedTests third = new AssignedTests
        {
            AssignedBy = SecondTeacherImmutableId,
            AssignedTime = DateTime.Now.AddHours(-1),
            CreationDate = DateTime.Now.AddHours(-2),
            GroupImmutableId = SecondGroupImmutableId,
            Id = 3,
            ImmutableId = Guid.NewGuid(),
            TestDescriptionId = 3,
            Version = 1
        };
        public static AssignedTests fourth = new AssignedTests
        {
            AssignedBy = SecondTeacherImmutableId,
            AssignedTime = DateTime.Now,
            CreationDate = DateTime.Now.AddHours(-1),
            StudentImmutableId = FirstStudentImmutableId,
            Id = 4,
            ImmutableId = Guid.NewGuid(),
            TestDescriptionId = 4,
            Version = 1
        };

        public static List<AssignedTests> AssignedTests = new List<AssignedTests> { first, second, third, fourth };

        public static StudentAnswer firstAnswer = new StudentAnswer
        {
            Id = 1,
            Answers = new List<Answer>(),
            AssignedTestImmutableId = first.ImmutableId,
            UserId = FirstStudentImmutableId
        };

        public static List<StudentAnswer> StudentAnswers = new List<StudentAnswer> { firstAnswer };
    }
}
