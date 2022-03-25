using Xunit;

namespace Vesuv.Core.IO
{
    public class ProjectTest
    {
        [Theory(DisplayName = "Try open a non-existing project")]
        [InlineData(@"S:\Work\Vesuv\No project in this folder")]
        public async Task OpenNonExistingProject(string resRootPath)
        {
            // Prepare actual object
            var project = await Project.OpenProject(resRootPath);

            // Prepare expected properties
            var expectedProjectName = $"{new DirectoryInfo(resRootPath).Name} (Missing Project)";

            // Do the tests
            Assert.NotNull(project);
            Assert.Equal(expectedProjectName, project.ProjectFile.ProjectName);
        }

        [Theory(DisplayName = "Create new in-memory project")]
        [InlineData("TestProject 01")]
        public void CreateNewInMemoryProject(string projectName)
        {
            // Prepare actual object
            var project = Project.CreateNewProject(projectName);

            // Do the tests
            Assert.NotNull(project);
        }
    }
}
