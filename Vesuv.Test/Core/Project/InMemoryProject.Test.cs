using Xunit;

namespace Vesuv.Core.Project
{
    public class InMemoryProjectTest
    {

        [Fact(DisplayName = "Create a project that doesn't exist.")]
        public void CreateMissingProject()
        {
            // Prepare Test
            var uniquePathCounter = 0;
            var tempPath = Path.GetTempPath();
            var projectPath = Path.Combine(tempPath, $"MissingProject ({uniquePathCounter})");
            while (Directory.Exists(projectPath)) {
                ++uniquePathCounter;
                projectPath = Path.Combine(tempPath, $"MissingProject ({uniquePathCounter})");
            }
            var projectDirectory = new DirectoryInfo(projectPath);

            // Prepare actual object
            IProject project = InMemoryProject.OpenProject(projectDirectory);

            // Prepare expected properties
            var expectedReadonlyFlag = true;
            var expectedMissingFlag = true;
            var expectedModifiedFlag = false;
            var expectedProjectName = $"MissingProject ({uniquePathCounter})";
            var expectedEngineVersion = new Version(0, 0, 0);

            // Do the tests
            Assert.Equal(expectedReadonlyFlag, project.IsReadonly);
            Assert.Equal(expectedMissingFlag, project.IsMissing);
            Assert.Equal(expectedModifiedFlag, project.IsModified);
            Assert.Equal(expectedProjectName, project.Name);
            Assert.Equal(expectedEngineVersion, project.EngineVersion);
        }

        [Fact(DisplayName = "Create a in-memory-project")]
        public void CreateNewInMemoryProject()
        {
            // Prepare actual object
            IProject project = InMemoryProject.CreateProject("Test Project");

            // Prepare expected properties
            var expectedReadonlyFlag = false;
            var expectedMissingFlag = false;
            var expectedModifiedFlag = false;
            var expectedProjectName = $"Test Project";
            var expectedEngineVersion = Common.CurrentEngineVersion;

            // Do the tests
            Assert.Equal(expectedReadonlyFlag, project.IsReadonly);
            Assert.Equal(expectedMissingFlag, project.IsMissing);
            Assert.Equal(expectedModifiedFlag, project.IsModified);
            Assert.Equal(expectedProjectName, project.Name);
            Assert.Equal(expectedEngineVersion, project.EngineVersion);
        }
    }
}
